using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface ICustomerLoyaltyCardService
    {
        Task<IEnumerable<CustomerLoyaltyCardDTO>> GetCustomerLoyaltyCardsAsync();
        Task<CustomerLoyaltyCardDTO> GetByIdAsync(int? id);
        Task<CustomerLoyaltyCardDTO> GetByCustomerAndLoyaltyCardAsync(int customerId, int loyaltyCardId, int companyId);
        Task<CustomerLoyaltyCardDTO> CreateAsync(int customerId, int loyaltyCardId, int companyId);
        Task<CustomerLoyaltyCardDTO> AddStampAsync(AddStampDTO addStampDto);
        Task<CustomerLoyaltyCardDTO> ResetAsync(int id);
        Task<IEnumerable<CustomerLoyaltyCardDTO>> GetByCustomerAsync(int customerId);
        Task<IEnumerable<CustomerLoyaltyCardDTO>> GetByLoyaltyCardAsync(int loyaltyCardId);
        Task<IEnumerable<CustomerLoyaltyCardDTO>> GetByCompanyAsync(int companyId);
        Task<IEnumerable<CustomerLoyaltyCardDTO>> GetCompletedCardsAsync();
        Task<IEnumerable<CustomerLoyaltyCardDTO>> GetExpiredCardsAsync();
        Task<bool> CustomerHasActiveCardAsync(int customerId, int loyaltyCardId, int companyId);
    }
}