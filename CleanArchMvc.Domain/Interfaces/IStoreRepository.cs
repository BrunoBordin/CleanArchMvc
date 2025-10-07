using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetStoresAsync();
        Task<Store> GetByIdAsync(int? id);
        Task<Store> GetByCNPJAsync(string cnpj);
        Task<Store> CreateAsync(Store store);
        Task<Store> UpdateAsync(Store store);
        Task<Store> RemoveAsync(Store store);
        Task<IEnumerable<Store>> GetActiveStoresAsync();
        Task<IEnumerable<Store>> GetStoresByLoyaltyCardAsync(int loyaltyCardId);
    }
}