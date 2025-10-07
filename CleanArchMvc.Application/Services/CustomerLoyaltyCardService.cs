using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Domain.Enums;

namespace CleanArchMvc.Application.Services
{
    public class CustomerLoyaltyCardService : ICustomerLoyaltyCardService
    {
        private ICustomerLoyaltyCardRepository _customerLoyaltyCardRepository;
        private ILoyaltyCardRepository _loyaltyCardRepository;
        private ICustomerRepository _customerRepository;
        private IStoreRepository _storeRepository;
        private readonly IMapper _mapper;

        public CustomerLoyaltyCardService(
            ICustomerLoyaltyCardRepository customerLoyaltyCardRepository,
            ILoyaltyCardRepository loyaltyCardRepository,
            ICustomerRepository customerRepository,
            IStoreRepository storeRepository,
            IMapper mapper)
        {
            _customerLoyaltyCardRepository = customerLoyaltyCardRepository;
            _loyaltyCardRepository = loyaltyCardRepository;
            _customerRepository = customerRepository;
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        public async Task<CustomerLoyaltyCardDTO> CreateAsync(int customerId, int loyaltyCardId, int storeId)
        {
            // Verify if customer already has an active card for this loyalty card and store
            var existingCard = await _customerLoyaltyCardRepository.GetByCustomerAndLoyaltyCardAsync(customerId, loyaltyCardId, storeId);
            if (existingCard != null && !existingCard.IsCompleted && !existingCard.IsExpired())
            {
                throw new InvalidOperationException("Customer already has an active loyalty card for this store.");
            }

            // Verify if loyalty card is active
            var loyaltyCard = await _loyaltyCardRepository.GetByIdAsync(loyaltyCardId);
            if (loyaltyCard == null || loyaltyCard.Status != LoyaltyCardStatus.Active)
            {
                throw new InvalidOperationException("Loyalty card is not active or does not exist.");
            }

            // Verify if store is active and participates in the loyalty card
            var store = await _storeRepository.GetByIdAsync(storeId);
            if (store == null || !store.IsActive)
            {
                throw new InvalidOperationException("Store is not active or does not exist.");
            }

            // For single store scope, verify if store participates
            if (loyaltyCard.Scope == LoyaltyCardScope.SingleStore)
            {
                var participatingStores = await _loyaltyCardRepository.GetLoyaltyCardsByScopeAsync((int)LoyaltyCardScope.SingleStore);
                var singleStoreCard = participatingStores.FirstOrDefault();
                if (singleStoreCard == null || !singleStoreCard.ParticipatingStores.Any(ps => ps.StoreId == storeId))
                {
                    throw new InvalidOperationException("Store does not participate in this loyalty card.");
                }
            }

            var customerLoyaltyCardEntity = new CustomerLoyaltyCard(customerId, loyaltyCardId, storeId);
            var customerLoyaltyCardCreated = await _customerLoyaltyCardRepository.CreateAsync(customerLoyaltyCardEntity);
            
            return _mapper.Map<CustomerLoyaltyCardDTO>(customerLoyaltyCardCreated);
        }

        public async Task<CustomerLoyaltyCardDTO> AddStampAsync(AddStampDTO addStampDto)
        {
            var customerLoyaltyCard = await _customerLoyaltyCardRepository.GetByCustomerAndLoyaltyCardAsync(
                addStampDto.CustomerId, addStampDto.LoyaltyCardId, addStampDto.StoreId);

            if (customerLoyaltyCard == null)
            {
                throw new InvalidOperationException("Customer loyalty card not found.");
            }

            if (customerLoyaltyCard.IsCompleted)
            {
                throw new InvalidOperationException("Cannot add stamps to a completed loyalty card.");
            }

            if (customerLoyaltyCard.IsExpired())
            {
                throw new InvalidOperationException("Cannot add stamps to an expired loyalty card.");
            }

            // Get loyalty card details for validation
            var loyaltyCard = await _loyaltyCardRepository.GetByIdWithDetailsAsync(addStampDto.LoyaltyCardId);
            if (loyaltyCard == null)
            {
                throw new InvalidOperationException("Loyalty card not found.");
            }

            // Validate eligibility based on purchase
            if (!IsPurchaseEligible(addStampDto, loyaltyCard))
            {
                throw new InvalidOperationException("Purchase is not eligible for this loyalty card.");
            }

            // Calculate stamp count based on purchase value and loyalty card settings
            int stampCount = CalculateStampCount(addStampDto, loyaltyCard);

            if (stampCount > 0)
            {
                if (addStampDto.StampCount == 1)
                {
                    customerLoyaltyCard.AddStamp();
                }
                else
                {
                    customerLoyaltyCard.AddMultipleStamps(stampCount);
                }

                var customerLoyaltyCardUpdated = await _customerLoyaltyCardRepository.UpdateAsync(customerLoyaltyCard);
                return _mapper.Map<CustomerLoyaltyCardDTO>(customerLoyaltyCardUpdated);
            }

            return _mapper.Map<CustomerLoyaltyCardDTO>(customerLoyaltyCard);
        }

        public async Task<CustomerLoyaltyCardDTO> GetByIdAsync(int? id)
        {
            var customerLoyaltyCardEntity = await _customerLoyaltyCardRepository.GetByIdAsync(id);
            return _mapper.Map<CustomerLoyaltyCardDTO>(customerLoyaltyCardEntity);
        }

        public async Task<CustomerLoyaltyCardDTO> GetByCustomerAndLoyaltyCardAsync(int customerId, int loyaltyCardId, int storeId)
        {
            var customerLoyaltyCardEntity = await _customerLoyaltyCardRepository.GetByCustomerAndLoyaltyCardAsync(customerId, loyaltyCardId, storeId);
            return _mapper.Map<CustomerLoyaltyCardDTO>(customerLoyaltyCardEntity);
        }

        public async Task<IEnumerable<CustomerLoyaltyCardDTO>> GetCustomerLoyaltyCardsAsync()
        {
            var customerLoyaltyCardEntity = await _customerLoyaltyCardRepository.GetCustomerLoyaltyCardsAsync();
            return _mapper.Map<IEnumerable<CustomerLoyaltyCardDTO>>(customerLoyaltyCardEntity);
        }

        public async Task<CustomerLoyaltyCardDTO> ResetAsync(int id)
        {
            var customerLoyaltyCardEntity = await _customerLoyaltyCardRepository.GetByIdAsync(id);
            customerLoyaltyCardEntity.Reset();
            var customerLoyaltyCardUpdated = await _customerLoyaltyCardRepository.UpdateAsync(customerLoyaltyCardEntity);
            return _mapper.Map<CustomerLoyaltyCardDTO>(customerLoyaltyCardUpdated);
        }

        public async Task<IEnumerable<CustomerLoyaltyCardDTO>> GetByCustomerAsync(int customerId)
        {
            var customerLoyaltyCardEntity = await _customerLoyaltyCardRepository.GetByCustomerAsync(customerId);
            return _mapper.Map<IEnumerable<CustomerLoyaltyCardDTO>>(customerLoyaltyCardEntity);
        }

        public async Task<IEnumerable<CustomerLoyaltyCardDTO>> GetByLoyaltyCardAsync(int loyaltyCardId)
        {
            var customerLoyaltyCardEntity = await _customerLoyaltyCardRepository.GetByLoyaltyCardAsync(loyaltyCardId);
            return _mapper.Map<IEnumerable<CustomerLoyaltyCardDTO>>(customerLoyaltyCardEntity);
        }

        public async Task<IEnumerable<CustomerLoyaltyCardDTO>> GetByStoreAsync(int storeId)
        {
            var customerLoyaltyCardEntity = await _customerLoyaltyCardRepository.GetByStoreAsync(storeId);
            return _mapper.Map<IEnumerable<CustomerLoyaltyCardDTO>>(customerLoyaltyCardEntity);
        }

        public async Task<IEnumerable<CustomerLoyaltyCardDTO>> GetCompletedCardsAsync()
        {
            var customerLoyaltyCardEntity = await _customerLoyaltyCardRepository.GetCompletedCardsAsync();
            return _mapper.Map<IEnumerable<CustomerLoyaltyCardDTO>>(customerLoyaltyCardEntity);
        }

        public async Task<IEnumerable<CustomerLoyaltyCardDTO>> GetExpiredCardsAsync()
        {
            var customerLoyaltyCardEntity = await _customerLoyaltyCardRepository.GetExpiredCardsAsync();
            return _mapper.Map<IEnumerable<CustomerLoyaltyCardDTO>>(customerLoyaltyCardEntity);
        }

        public async Task<bool> CustomerHasActiveCardAsync(int customerId, int loyaltyCardId, int storeId)
        {
            return await _customerLoyaltyCardRepository.CustomerHasActiveCardAsync(customerId, loyaltyCardId, storeId);
        }

        private bool IsPurchaseEligible(AddStampDTO addStampDto, LoyaltyCard loyaltyCard)
        {
            switch (loyaltyCard.EligibilityType)
            {
                case EligibilityType.AnyPurchase:
                    return true;
                case EligibilityType.SpecificProducts:
                    return addStampDto.ProductIds.Any(pid => 
                        loyaltyCard.EligibleProducts.Any(ep => ep.ProductId == pid));
                case EligibilityType.SpecificCategories:
                    return addStampDto.CategoryIds.Any(cid => 
                        loyaltyCard.EligibleCategories.Any(ec => ec.CategoryId == cid));
                default:
                    return false;
            }
        }

        private int CalculateStampCount(AddStampDTO addStampDto, LoyaltyCard loyaltyCard)
        {
            if (!loyaltyCard.AllowMultipleStampsPerPurchase)
            {
                return 1;
            }

            // Calculate based on purchase value
            return (int)Math.Floor(addStampDto.PurchaseValue / loyaltyCard.ValuePerStamp);
        }
    }
}