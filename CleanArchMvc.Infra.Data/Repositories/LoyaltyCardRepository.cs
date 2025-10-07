using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class LoyaltyCardRepository : ILoyaltyCardRepository
    {
        ApplicationDbContext _loyaltyCardContext;
        public LoyaltyCardRepository(ApplicationDbContext context)
        {
            _loyaltyCardContext = context;
        }

        public async Task<LoyaltyCard> CreateAsync(LoyaltyCard loyaltyCard)
        {
            _loyaltyCardContext.Add(loyaltyCard);
            await _loyaltyCardContext.SaveChangesAsync();
            return loyaltyCard;
        }

        public async Task<LoyaltyCard> GetByIdAsync(int? id)
        {
            return await _loyaltyCardContext.LoyaltyCards.FindAsync(id);
        }

        public async Task<LoyaltyCard> GetByIdWithDetailsAsync(int? id)
        {
            return await _loyaltyCardContext.LoyaltyCards
                .Include(x => x.ParticipatingNetworks)
                    .ThenInclude(x => x.Network)
                .Include(x => x.ParticipatingCompanies)
                    .ThenInclude(x => x.Company)
                .Include(x => x.EligibleProducts)
                    .ThenInclude(x => x.Product)
                .Include(x => x.EligibleCategories)
                    .ThenInclude(x => x.Category)
                .Include(x => x.RewardProducts)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<LoyaltyCard>> GetLoyaltyCardsAsync()
        {
            return await _loyaltyCardContext.LoyaltyCards
                .Include(x => x.ParticipatingNetworks)
                    .ThenInclude(x => x.Network)
                .Include(x => x.ParticipatingCompanies)
                    .ThenInclude(x => x.Company)
                .ToListAsync();
        }

        public async Task<LoyaltyCard> RemoveAsync(LoyaltyCard loyaltyCard)
        {
            _loyaltyCardContext.Remove(loyaltyCard);
            await _loyaltyCardContext.SaveChangesAsync();
            return loyaltyCard;
        }

        public async Task<LoyaltyCard> UpdateAsync(LoyaltyCard loyaltyCard)
        {
            _loyaltyCardContext.Update(loyaltyCard);
            await _loyaltyCardContext.SaveChangesAsync();
            return loyaltyCard;
        }

        public async Task<IEnumerable<LoyaltyCard>> GetActiveLoyaltyCardsAsync()
        {
            return await _loyaltyCardContext.LoyaltyCards
                .Where(x => x.Status == Domain.Enums.LoyaltyCardStatus.Active)
                .Include(x => x.ParticipatingNetworks)
                    .ThenInclude(x => x.Network)
                .Include(x => x.ParticipatingCompanies)
                    .ThenInclude(x => x.Company)
                .ToListAsync();
        }

        public async Task<IEnumerable<LoyaltyCard>> GetLoyaltyCardsByScopeAsync(int scope)
        {
            return await _loyaltyCardContext.LoyaltyCards
                .Where(x => (int)x.Scope == scope)
                .Include(x => x.ParticipatingNetworks)
                    .ThenInclude(x => x.Network)
                .Include(x => x.ParticipatingCompanies)
                    .ThenInclude(x => x.Company)
                .ToListAsync();
        }

        public async Task<LoyaltyCard> GetActiveLoyaltyCardByCompanyAsync(int companyId)
        {
            return await _loyaltyCardContext.LoyaltyCards
                .Where(x => x.Status == Domain.Enums.LoyaltyCardStatus.Active &&
                           x.Scope == Domain.Enums.LoyaltyCardScope.SingleStore &&
                           x.ParticipatingCompanies.Any(pc => pc.CompanyId == companyId))
                .Include(x => x.ParticipatingCompanies)
                    .ThenInclude(x => x.Company)
                .FirstOrDefaultAsync();
        }

        public async Task<LoyaltyCard> GetActiveLoyaltyCardByNetworkAsync()
        {
            return await _loyaltyCardContext.LoyaltyCards
                .Where(x => x.Status == Domain.Enums.LoyaltyCardStatus.Active &&
                           x.Scope == Domain.Enums.LoyaltyCardScope.Network)
                .Include(x => x.ParticipatingNetworks)
                    .ThenInclude(x => x.Network)
                .Include(x => x.ParticipatingCompanies)
                    .ThenInclude(x => x.Company)
                .FirstOrDefaultAsync();
        }
    }
}