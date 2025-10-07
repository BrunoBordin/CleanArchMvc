using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class LoyaltyCardRedemptionRepository : ILoyaltyCardRedemptionRepository
    {
        ApplicationDbContext _redemptionContext;
        public LoyaltyCardRedemptionRepository(ApplicationDbContext context)
        {
            _redemptionContext = context;
        }

        public async Task<LoyaltyCardRedemption> CreateAsync(LoyaltyCardRedemption redemption)
        {
            _redemptionContext.Add(redemption);
            await _redemptionContext.SaveChangesAsync();
            return redemption;
        }

        public async Task<LoyaltyCardRedemption> GetByIdAsync(int? id)
        {
            return await _redemptionContext.LoyaltyCardRedemptions
                .Include(x => x.CustomerLoyaltyCard)
                    .ThenInclude(x => x.Customer)
                .Include(x => x.CustomerLoyaltyCard)
                    .ThenInclude(x => x.LoyaltyCard)
                .Include(x => x.Company)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<LoyaltyCardRedemption>> GetRedemptionsAsync()
        {
            return await _redemptionContext.LoyaltyCardRedemptions
                .Include(x => x.CustomerLoyaltyCard)
                    .ThenInclude(x => x.Customer)
                .Include(x => x.CustomerLoyaltyCard)
                    .ThenInclude(x => x.LoyaltyCard)
                .Include(x => x.Company)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<LoyaltyCardRedemption> RemoveAsync(LoyaltyCardRedemption redemption)
        {
            _redemptionContext.Remove(redemption);
            await _redemptionContext.SaveChangesAsync();
            return redemption;
        }

        public async Task<LoyaltyCardRedemption> UpdateAsync(LoyaltyCardRedemption redemption)
        {
            _redemptionContext.Update(redemption);
            await _redemptionContext.SaveChangesAsync();
            return redemption;
        }

        public async Task<IEnumerable<LoyaltyCardRedemption>> GetByCustomerLoyaltyCardAsync(int customerLoyaltyCardId)
        {
            return await _redemptionContext.LoyaltyCardRedemptions
                .Where(x => x.CustomerLoyaltyCardId == customerLoyaltyCardId)
                .Include(x => x.CustomerLoyaltyCard)
                    .ThenInclude(x => x.Customer)
                .Include(x => x.CustomerLoyaltyCard)
                    .ThenInclude(x => x.LoyaltyCard)
                .Include(x => x.Company)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<LoyaltyCardRedemption>> GetByCompanyAsync(int companyId)
        {
            return await _redemptionContext.LoyaltyCardRedemptions
                .Where(x => x.CompanyId == companyId)
                .Include(x => x.CustomerLoyaltyCard)
                    .ThenInclude(x => x.Customer)
                .Include(x => x.CustomerLoyaltyCard)
                    .ThenInclude(x => x.LoyaltyCard)
                .Include(x => x.Company)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<LoyaltyCardRedemption>> GetUnusedRedemptionsAsync()
        {
            return await _redemptionContext.LoyaltyCardRedemptions
                .Where(x => !x.IsUsed)
                .Include(x => x.CustomerLoyaltyCard)
                    .ThenInclude(x => x.Customer)
                .Include(x => x.CustomerLoyaltyCard)
                    .ThenInclude(x => x.LoyaltyCard)
                .Include(x => x.Company)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<LoyaltyCardRedemption>> GetExpiredRedemptionsAsync()
        {
            var now = DateTime.UtcNow;
            return await _redemptionContext.LoyaltyCardRedemptions
                .Where(x => x.BenefitExpirationDate.HasValue && x.BenefitExpirationDate.Value < now)
                .Include(x => x.CustomerLoyaltyCard)
                    .ThenInclude(x => x.Customer)
                .Include(x => x.CustomerLoyaltyCard)
                    .ThenInclude(x => x.LoyaltyCard)
                .Include(x => x.Company)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<LoyaltyCardRedemption>> GetByCustomerAsync(int customerId)
        {
            return await _redemptionContext.LoyaltyCardRedemptions
                .Where(x => x.CustomerLoyaltyCard.CustomerId == customerId)
                .Include(x => x.CustomerLoyaltyCard)
                    .ThenInclude(x => x.Customer)
                .Include(x => x.CustomerLoyaltyCard)
                    .ThenInclude(x => x.LoyaltyCard)
                .Include(x => x.Company)
                .Include(x => x.Product)
                .ToListAsync();
        }
    }
}