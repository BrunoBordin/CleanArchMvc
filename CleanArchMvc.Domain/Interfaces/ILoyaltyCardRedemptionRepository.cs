using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface ILoyaltyCardRedemptionRepository
    {
        Task<IEnumerable<LoyaltyCardRedemption>> GetRedemptionsAsync();
        Task<LoyaltyCardRedemption> GetByIdAsync(int? id);
        Task<LoyaltyCardRedemption> CreateAsync(LoyaltyCardRedemption redemption);
        Task<LoyaltyCardRedemption> UpdateAsync(LoyaltyCardRedemption redemption);
        Task<LoyaltyCardRedemption> RemoveAsync(LoyaltyCardRedemption redemption);
        Task<IEnumerable<LoyaltyCardRedemption>> GetByCustomerLoyaltyCardAsync(int customerLoyaltyCardId);
        Task<IEnumerable<LoyaltyCardRedemption>> GetByStoreAsync(int storeId);
        Task<IEnumerable<LoyaltyCardRedemption>> GetUnusedRedemptionsAsync();
        Task<IEnumerable<LoyaltyCardRedemption>> GetExpiredRedemptionsAsync();
        Task<IEnumerable<LoyaltyCardRedemption>> GetByCustomerAsync(int customerId);
    }
}