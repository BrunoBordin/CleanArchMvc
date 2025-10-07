using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class CustomerLoyaltyCardRepository : ICustomerLoyaltyCardRepository
    {
        ApplicationDbContext _customerLoyaltyCardContext;
        public CustomerLoyaltyCardRepository(ApplicationDbContext context)
        {
            _customerLoyaltyCardContext = context;
        }

        public async Task<CustomerLoyaltyCard> CreateAsync(CustomerLoyaltyCard customerLoyaltyCard)
        {
            _customerLoyaltyCardContext.Add(customerLoyaltyCard);
            await _customerLoyaltyCardContext.SaveChangesAsync();
            return customerLoyaltyCard;
        }

        public async Task<CustomerLoyaltyCard> GetByIdAsync(int? id)
        {
            return await _customerLoyaltyCardContext.CustomerLoyaltyCards
                .Include(x => x.Customer)
                .Include(x => x.LoyaltyCard)
                .Include(x => x.Store)
                .Include(x => x.Redemptions)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CustomerLoyaltyCard> GetByCustomerAndLoyaltyCardAsync(int customerId, int loyaltyCardId, int storeId)
        {
            return await _customerLoyaltyCardContext.CustomerLoyaltyCards
                .Include(x => x.Customer)
                .Include(x => x.LoyaltyCard)
                .Include(x => x.Store)
                .Include(x => x.Redemptions)
                .FirstOrDefaultAsync(x => x.CustomerId == customerId && 
                                        x.LoyaltyCardId == loyaltyCardId && 
                                        x.StoreId == storeId);
        }

        public async Task<IEnumerable<CustomerLoyaltyCard>> GetCustomerLoyaltyCardsAsync()
        {
            return await _customerLoyaltyCardContext.CustomerLoyaltyCards
                .Include(x => x.Customer)
                .Include(x => x.LoyaltyCard)
                .Include(x => x.Store)
                .Include(x => x.Redemptions)
                .ToListAsync();
        }

        public async Task<CustomerLoyaltyCard> RemoveAsync(CustomerLoyaltyCard customerLoyaltyCard)
        {
            _customerLoyaltyCardContext.Remove(customerLoyaltyCard);
            await _customerLoyaltyCardContext.SaveChangesAsync();
            return customerLoyaltyCard;
        }

        public async Task<CustomerLoyaltyCard> UpdateAsync(CustomerLoyaltyCard customerLoyaltyCard)
        {
            _customerLoyaltyCardContext.Update(customerLoyaltyCard);
            await _customerLoyaltyCardContext.SaveChangesAsync();
            return customerLoyaltyCard;
        }

        public async Task<IEnumerable<CustomerLoyaltyCard>> GetByCustomerAsync(int customerId)
        {
            return await _customerLoyaltyCardContext.CustomerLoyaltyCards
                .Where(x => x.CustomerId == customerId)
                .Include(x => x.Customer)
                .Include(x => x.LoyaltyCard)
                .Include(x => x.Store)
                .Include(x => x.Redemptions)
                .ToListAsync();
        }

        public async Task<IEnumerable<CustomerLoyaltyCard>> GetByLoyaltyCardAsync(int loyaltyCardId)
        {
            return await _customerLoyaltyCardContext.CustomerLoyaltyCards
                .Where(x => x.LoyaltyCardId == loyaltyCardId)
                .Include(x => x.Customer)
                .Include(x => x.LoyaltyCard)
                .Include(x => x.Store)
                .Include(x => x.Redemptions)
                .ToListAsync();
        }

        public async Task<IEnumerable<CustomerLoyaltyCard>> GetByStoreAsync(int storeId)
        {
            return await _customerLoyaltyCardContext.CustomerLoyaltyCards
                .Where(x => x.StoreId == storeId)
                .Include(x => x.Customer)
                .Include(x => x.LoyaltyCard)
                .Include(x => x.Store)
                .Include(x => x.Redemptions)
                .ToListAsync();
        }

        public async Task<IEnumerable<CustomerLoyaltyCard>> GetCompletedCardsAsync()
        {
            return await _customerLoyaltyCardContext.CustomerLoyaltyCards
                .Where(x => x.IsCompleted)
                .Include(x => x.Customer)
                .Include(x => x.LoyaltyCard)
                .Include(x => x.Store)
                .Include(x => x.Redemptions)
                .ToListAsync();
        }

        public async Task<IEnumerable<CustomerLoyaltyCard>> GetExpiredCardsAsync()
        {
            var now = DateTime.UtcNow;
            return await _customerLoyaltyCardContext.CustomerLoyaltyCards
                .Where(x => x.ExpirationDate.HasValue && x.ExpirationDate.Value < now)
                .Include(x => x.Customer)
                .Include(x => x.LoyaltyCard)
                .Include(x => x.Store)
                .Include(x => x.Redemptions)
                .ToListAsync();
        }

        public async Task<bool> CustomerHasActiveCardAsync(int customerId, int loyaltyCardId, int storeId)
        {
            return await _customerLoyaltyCardContext.CustomerLoyaltyCards
                .AnyAsync(x => x.CustomerId == customerId && 
                             x.LoyaltyCardId == loyaltyCardId && 
                             x.StoreId == storeId &&
                             !x.IsCompleted &&
                             (!x.ExpirationDate.HasValue || x.ExpirationDate.Value > DateTime.UtcNow));
        }
    }
}