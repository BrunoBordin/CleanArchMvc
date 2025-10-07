using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface ILoyaltyCardRepository
    {
        Task<IEnumerable<LoyaltyCard>> GetLoyaltyCardsAsync();
        Task<LoyaltyCard> GetByIdAsync(int? id);
        Task<LoyaltyCard> GetByIdWithDetailsAsync(int? id);
        Task<LoyaltyCard> CreateAsync(LoyaltyCard loyaltyCard);
        Task<LoyaltyCard> UpdateAsync(LoyaltyCard loyaltyCard);
        Task<LoyaltyCard> RemoveAsync(LoyaltyCard loyaltyCard);
        Task<IEnumerable<LoyaltyCard>> GetActiveLoyaltyCardsAsync();
        Task<IEnumerable<LoyaltyCard>> GetLoyaltyCardsByScopeAsync(int scope);
        Task<LoyaltyCard> GetActiveLoyaltyCardByStoreAsync(int storeId);
        Task<LoyaltyCard> GetActiveLoyaltyCardByNetworkAsync();
    }
}