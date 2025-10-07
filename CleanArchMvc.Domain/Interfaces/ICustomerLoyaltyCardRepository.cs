using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface ICustomerLoyaltyCardRepository
    {
        Task<IEnumerable<CustomerLoyaltyCard>> GetCustomerLoyaltyCardsAsync();
        Task<CustomerLoyaltyCard> GetByIdAsync(int? id);
        Task<CustomerLoyaltyCard> GetByCustomerAndLoyaltyCardAsync(int customerId, int loyaltyCardId, int companyId);
        Task<CustomerLoyaltyCard> CreateAsync(CustomerLoyaltyCard customerLoyaltyCard);
        Task<CustomerLoyaltyCard> UpdateAsync(CustomerLoyaltyCard customerLoyaltyCard);
        Task<CustomerLoyaltyCard> RemoveAsync(CustomerLoyaltyCard customerLoyaltyCard);
        Task<IEnumerable<CustomerLoyaltyCard>> GetByCustomerAsync(int customerId);
        Task<IEnumerable<CustomerLoyaltyCard>> GetByLoyaltyCardAsync(int loyaltyCardId);
        Task<IEnumerable<CustomerLoyaltyCard>> GetByCompanyAsync(int companyId);
        Task<IEnumerable<CustomerLoyaltyCard>> GetCompletedCardsAsync();
        Task<IEnumerable<CustomerLoyaltyCard>> GetExpiredCardsAsync();
        Task<bool> CustomerHasActiveCardAsync(int customerId, int loyaltyCardId, int companyId);
    }
}