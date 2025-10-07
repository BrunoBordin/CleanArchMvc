using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Domain.Enums;

namespace CleanArchMvc.Application.Services
{
    public class LoyaltyCardRedemptionService : ILoyaltyCardRedemptionService
    {
        private ILoyaltyCardRedemptionRepository _redemptionRepository;
        private ICustomerLoyaltyCardRepository _customerLoyaltyCardRepository;
        private readonly IMapper _mapper;

        public LoyaltyCardRedemptionService(
            ILoyaltyCardRedemptionRepository redemptionRepository,
            ICustomerLoyaltyCardRepository customerLoyaltyCardRepository,
            IMapper mapper)
        {
            _redemptionRepository = redemptionRepository;
            _customerLoyaltyCardRepository = customerLoyaltyCardRepository;
            _mapper = mapper;
        }

        public async Task<LoyaltyCardRedemptionDTO> CreateAsync(RedeemRewardDTO redeemRewardDto)
        {
            // Verify if customer loyalty card exists and is completed
            var customerLoyaltyCard = await _customerLoyaltyCardRepository.GetByIdAsync(redeemRewardDto.CustomerLoyaltyCardId);
            if (customerLoyaltyCard == null)
            {
                throw new InvalidOperationException("Customer loyalty card not found.");
            }

            if (!customerLoyaltyCard.IsCompleted)
            {
                throw new InvalidOperationException("Loyalty card is not completed yet.");
            }

            if (customerLoyaltyCard.IsExpired())
            {
                throw new InvalidOperationException("Loyalty card has expired.");
            }

            // Get loyalty card details to validate reward type
            var loyaltyCard = customerLoyaltyCard.LoyaltyCard;
            if (loyaltyCard == null)
            {
                throw new InvalidOperationException("Loyalty card not found.");
            }

            // Validate reward type matches loyalty card configuration
            if (redeemRewardDto.RewardType != loyaltyCard.RewardType)
            {
                throw new InvalidOperationException("Reward type does not match loyalty card configuration.");
            }

            // Validate reward values
            ValidateRewardValues(redeemRewardDto, loyaltyCard);

            // Calculate benefit expiration date
            DateTime? benefitExpirationDate = null;
            if (loyaltyCard.BenefitValidityDays.HasValue)
            {
                benefitExpirationDate = DateTime.UtcNow.AddDays(loyaltyCard.BenefitValidityDays.Value);
            }

            var redemptionEntity = new LoyaltyCardRedemption(
                redeemRewardDto.CustomerLoyaltyCardId,
                redeemRewardDto.StoreId,
                redeemRewardDto.RewardType,
                redeemRewardDto.ProductId,
                redeemRewardDto.DiscountValue,
                redeemRewardDto.CashbackValue,
                benefitExpirationDate,
                redeemRewardDto.Notes
            );

            var redemptionCreated = await _redemptionRepository.CreateAsync(redemptionEntity);

            // Reset the customer loyalty card after redemption
            customerLoyaltyCard.Reset();
            await _customerLoyaltyCardRepository.UpdateAsync(customerLoyaltyCard);

            return _mapper.Map<LoyaltyCardRedemptionDTO>(redemptionCreated);
        }

        public async Task<LoyaltyCardRedemptionDTO> GetByIdAsync(int? id)
        {
            var redemptionEntity = await _redemptionRepository.GetByIdAsync(id);
            return _mapper.Map<LoyaltyCardRedemptionDTO>(redemptionEntity);
        }

        public async Task<IEnumerable<LoyaltyCardRedemptionDTO>> GetRedemptionsAsync()
        {
            var redemptionEntity = await _redemptionRepository.GetRedemptionsAsync();
            return _mapper.Map<IEnumerable<LoyaltyCardRedemptionDTO>>(redemptionEntity);
        }

        public async Task<LoyaltyCardRedemptionDTO> MarkAsUsedAsync(int id, string? notes = null)
        {
            var redemptionEntity = await _redemptionRepository.GetByIdAsync(id);
            if (redemptionEntity == null)
            {
                throw new InvalidOperationException("Redemption not found.");
            }

            redemptionEntity.MarkAsUsed(notes);
            var redemptionUpdated = await _redemptionRepository.UpdateAsync(redemptionEntity);
            return _mapper.Map<LoyaltyCardRedemptionDTO>(redemptionUpdated);
        }

        public async Task<IEnumerable<LoyaltyCardRedemptionDTO>> GetByCustomerLoyaltyCardAsync(int customerLoyaltyCardId)
        {
            var redemptionEntity = await _redemptionRepository.GetByCustomerLoyaltyCardAsync(customerLoyaltyCardId);
            return _mapper.Map<IEnumerable<LoyaltyCardRedemptionDTO>>(redemptionEntity);
        }

        public async Task<IEnumerable<LoyaltyCardRedemptionDTO>> GetByStoreAsync(int storeId)
        {
            var redemptionEntity = await _redemptionRepository.GetByStoreAsync(storeId);
            return _mapper.Map<IEnumerable<LoyaltyCardRedemptionDTO>>(redemptionEntity);
        }

        public async Task<IEnumerable<LoyaltyCardRedemptionDTO>> GetUnusedRedemptionsAsync()
        {
            var redemptionEntity = await _redemptionRepository.GetUnusedRedemptionsAsync();
            return _mapper.Map<IEnumerable<LoyaltyCardRedemptionDTO>>(redemptionEntity);
        }

        public async Task<IEnumerable<LoyaltyCardRedemptionDTO>> GetExpiredRedemptionsAsync()
        {
            var redemptionEntity = await _redemptionRepository.GetExpiredRedemptionsAsync();
            return _mapper.Map<IEnumerable<LoyaltyCardRedemptionDTO>>(redemptionEntity);
        }

        public async Task<IEnumerable<LoyaltyCardRedemptionDTO>> GetByCustomerAsync(int customerId)
        {
            var redemptionEntity = await _redemptionRepository.GetByCustomerAsync(customerId);
            return _mapper.Map<IEnumerable<LoyaltyCardRedemptionDTO>>(redemptionEntity);
        }

        private void ValidateRewardValues(RedeemRewardDTO redeemRewardDto, LoyaltyCard loyaltyCard)
        {
            switch (redeemRewardDto.RewardType)
            {
                case RewardType.Product:
                    if (!redeemRewardDto.ProductId.HasValue)
                    {
                        throw new InvalidOperationException("Product ID is required for product reward.");
                    }
                    break;
                case RewardType.Discount:
                    if (!redeemRewardDto.DiscountValue.HasValue || redeemRewardDto.DiscountValue <= 0)
                    {
                        throw new InvalidOperationException("Valid discount value is required for discount reward.");
                    }
                    break;
                case RewardType.Cashback:
                    if (!redeemRewardDto.CashbackValue.HasValue || redeemRewardDto.CashbackValue <= 0)
                    {
                        throw new InvalidOperationException("Valid cashback value is required for cashback reward.");
                    }
                    break;
            }
        }
    }
}