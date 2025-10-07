using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        ApplicationDbContext _companyContext;
        public CompanyRepository(ApplicationDbContext context)
        {
            _companyContext = context;
        }

        public async Task<Company> CreateAsync(Company company)
        {
            _companyContext.Add(company);
            await _companyContext.SaveChangesAsync();
            return company;
        }

        public async Task<Company> GetByIdAsync(int? id)
        {
            return await _companyContext.Companies
                .Include(x => x.Network)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Company> GetByCNPJAsync(string cnpj)
        {
            return await _companyContext.Companies
                .Include(x => x.Network)
                .FirstOrDefaultAsync(x => x.CNPJ == cnpj);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _companyContext.Companies
                .Include(x => x.Network)
                .Include(x => x.LoyaltyCards)
                    .ThenInclude(x => x.LoyaltyCard)
                .ToListAsync();
        }

        public async Task<Company> RemoveAsync(Company company)
        {
            _companyContext.Remove(company);
            await _companyContext.SaveChangesAsync();
            return company;
        }

        public async Task<Company> UpdateAsync(Company company)
        {
            _companyContext.Update(company);
            await _companyContext.SaveChangesAsync();
            return company;
        }

        public async Task<IEnumerable<Company>> GetActiveCompaniesAsync()
        {
            return await _companyContext.Companies
                .Where(x => x.IsActive)
                .Include(x => x.Network)
                .Include(x => x.LoyaltyCards)
                    .ThenInclude(x => x.LoyaltyCard)
                .ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesByLoyaltyCardAsync(int loyaltyCardId)
        {
            return await _companyContext.Companies
                .Where(x => x.LoyaltyCards.Any(lc => lc.LoyaltyCardId == loyaltyCardId))
                .Include(x => x.Network)
                .Include(x => x.LoyaltyCards)
                    .ThenInclude(x => x.LoyaltyCard)
                .ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesByNetworkAsync(int networkId)
        {
            return await _companyContext.Companies
                .Where(x => x.NetworkId == networkId)
                .Include(x => x.Network)
                .Include(x => x.LoyaltyCards)
                    .ThenInclude(x => x.LoyaltyCard)
                .ToListAsync();
        }
    }
}