using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        ApplicationDbContext _storeContext;
        public StoreRepository(ApplicationDbContext context)
        {
            _storeContext = context;
        }

        public async Task<Store> CreateAsync(Store store)
        {
            _storeContext.Add(store);
            await _storeContext.SaveChangesAsync();
            return store;
        }

        public async Task<Store> GetByIdAsync(int? id)
        {
            return await _storeContext.Stores.FindAsync(id);
        }

        public async Task<Store> GetByCNPJAsync(string cnpj)
        {
            return await _storeContext.Stores
                .FirstOrDefaultAsync(x => x.CNPJ == cnpj);
        }

        public async Task<IEnumerable<Store>> GetStoresAsync()
        {
            return await _storeContext.Stores
                .Include(x => x.LoyaltyCards)
                    .ThenInclude(x => x.LoyaltyCard)
                .ToListAsync();
        }

        public async Task<Store> RemoveAsync(Store store)
        {
            _storeContext.Remove(store);
            await _storeContext.SaveChangesAsync();
            return store;
        }

        public async Task<Store> UpdateAsync(Store store)
        {
            _storeContext.Update(store);
            await _storeContext.SaveChangesAsync();
            return store;
        }

        public async Task<IEnumerable<Store>> GetActiveStoresAsync()
        {
            return await _storeContext.Stores
                .Where(x => x.IsActive)
                .Include(x => x.LoyaltyCards)
                    .ThenInclude(x => x.LoyaltyCard)
                .ToListAsync();
        }

        public async Task<IEnumerable<Store>> GetStoresByLoyaltyCardAsync(int loyaltyCardId)
        {
            return await _storeContext.Stores
                .Where(x => x.LoyaltyCards.Any(lc => lc.LoyaltyCardId == loyaltyCardId))
                .Include(x => x.LoyaltyCards)
                    .ThenInclude(x => x.LoyaltyCard)
                .ToListAsync();
        }
    }
}