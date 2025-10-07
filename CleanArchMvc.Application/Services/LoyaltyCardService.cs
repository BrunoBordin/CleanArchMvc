using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Domain.Enums;

namespace CleanArchMvc.Application.Services
{
    public class LoyaltyCardService : ILoyaltyCardService
    {
        private ILoyaltyCardRepository _loyaltyCardRepository;
        private IStoreRepository _storeRepository;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public LoyaltyCardService(ILoyaltyCardRepository loyaltyCardRepository, 
            IStoreRepository storeRepository, 
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _loyaltyCardRepository = loyaltyCardRepository;
            _storeRepository = storeRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<LoyaltyCardDTO> CreateAsync(CreateLoyaltyCardDTO loyaltyCardDto)
        {
            var loyaltyCardEntity = new LoyaltyCard(
                loyaltyCardDto.PublicName,
                loyaltyCardDto.StampGoal,
                loyaltyCardDto.ValuePerStamp,
                loyaltyCardDto.EligibilityType,
                loyaltyCardDto.AllowMultipleStampsPerPurchase,
                loyaltyCardDto.Scope,
                loyaltyCardDto.RewardType,
                loyaltyCardDto.DiscountValue,
                loyaltyCardDto.CashbackValue,
                loyaltyCardDto.CardValidityDays,
                loyaltyCardDto.BenefitValidityDays
            );

            var loyaltyCardCreated = await _loyaltyCardRepository.CreateAsync(loyaltyCardEntity);

            // Add participating stores
            foreach (var storeId in loyaltyCardDto.ParticipatingStoreIds)
            {
                var store = await _storeRepository.GetByIdAsync(storeId);
                if (store != null)
                {
                    var loyaltyCardStore = new LoyaltyCardStore(loyaltyCardCreated.Id, storeId);
                    // Note: You'll need to add this to the repository or handle it differently
                }
            }

            // Add eligible products
            foreach (var productId in loyaltyCardDto.EligibleProductIds)
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product != null)
                {
                    var eligibleProduct = new LoyaltyCardEligibleProduct(loyaltyCardCreated.Id, productId);
                    // Note: You'll need to add this to the repository or handle it differently
                }
            }

            // Add eligible categories
            foreach (var categoryId in loyaltyCardDto.EligibleCategoryIds)
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category != null)
                {
                    var eligibleCategory = new LoyaltyCardEligibleCategory(loyaltyCardCreated.Id, categoryId);
                    // Note: You'll need to add this to the repository or handle it differently
                }
            }

            // Add reward products
            foreach (var productId in loyaltyCardDto.RewardProductIds)
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product != null)
                {
                    var rewardProduct = new LoyaltyCardRewardProduct(loyaltyCardCreated.Id, productId);
                    // Note: You'll need to add this to the repository or handle it differently
                }
            }

            return _mapper.Map<LoyaltyCardDTO>(loyaltyCardCreated);
        }

        public async Task<LoyaltyCardDTO> GetByIdAsync(int? id)
        {
            var loyaltyCardEntity = await _loyaltyCardRepository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<LoyaltyCardDTO>(loyaltyCardEntity);
        }

        public async Task<IEnumerable<LoyaltyCardDTO>> GetLoyaltyCardsAsync()
        {
            var loyaltyCardEntity = await _loyaltyCardRepository.GetLoyaltyCardsAsync();
            return _mapper.Map<IEnumerable<LoyaltyCardDTO>>(loyaltyCardEntity);
        }

        public async Task<LoyaltyCardDTO> RemoveAsync(int? id)
        {
            var loyaltyCardEntity = _loyaltyCardRepository.GetByIdAsync(id).Result;
            var loyaltyCardRemoved = await _loyaltyCardRepository.RemoveAsync(loyaltyCardEntity);
            return _mapper.Map<LoyaltyCardDTO>(loyaltyCardRemoved);
        }

        public async Task<LoyaltyCardDTO> UpdateAsync(UpdateLoyaltyCardDTO loyaltyCardDto)
        {
            var loyaltyCardEntity = await _loyaltyCardRepository.GetByIdAsync(loyaltyCardDto.Id);
            
            loyaltyCardEntity.UpdateConfiguration(
                loyaltyCardDto.PublicName,
                loyaltyCardDto.StampGoal,
                loyaltyCardDto.ValuePerStamp,
                loyaltyCardDto.EligibilityType,
                loyaltyCardDto.AllowMultipleStampsPerPurchase,
                loyaltyCardDto.RewardType,
                loyaltyCardDto.DiscountValue,
                loyaltyCardDto.CashbackValue,
                loyaltyCardDto.CardValidityDays,
                loyaltyCardDto.BenefitValidityDays
            );

            var loyaltyCardUpdated = await _loyaltyCardRepository.UpdateAsync(loyaltyCardEntity);
            return _mapper.Map<LoyaltyCardDTO>(loyaltyCardUpdated);
        }

        public async Task<LoyaltyCardDTO> UpdateStatusAsync(int id, int status)
        {
            var loyaltyCardEntity = await _loyaltyCardRepository.GetByIdAsync(id);
            loyaltyCardEntity.UpdateStatus((LoyaltyCardStatus)status);
            var loyaltyCardUpdated = await _loyaltyCardRepository.UpdateAsync(loyaltyCardEntity);
            return _mapper.Map<LoyaltyCardDTO>(loyaltyCardUpdated);
        }

        public async Task<IEnumerable<LoyaltyCardDTO>> GetActiveLoyaltyCardsAsync()
        {
            var loyaltyCardEntity = await _loyaltyCardRepository.GetActiveLoyaltyCardsAsync();
            return _mapper.Map<IEnumerable<LoyaltyCardDTO>>(loyaltyCardEntity);
        }

        public async Task<LoyaltyCardDTO> GetActiveLoyaltyCardByStoreAsync(int storeId)
        {
            var loyaltyCardEntity = await _loyaltyCardRepository.GetActiveLoyaltyCardByStoreAsync(storeId);
            return _mapper.Map<LoyaltyCardDTO>(loyaltyCardEntity);
        }

        public async Task<LoyaltyCardDTO> GetActiveLoyaltyCardByNetworkAsync()
        {
            var loyaltyCardEntity = await _loyaltyCardRepository.GetActiveLoyaltyCardByNetworkAsync();
            return _mapper.Map<LoyaltyCardDTO>(loyaltyCardEntity);
        }
    }
}