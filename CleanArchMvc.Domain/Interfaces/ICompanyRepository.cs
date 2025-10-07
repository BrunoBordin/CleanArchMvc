using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company> GetByIdAsync(int? id);
        Task<Company> GetByCNPJAsync(string cnpj);
        Task<Company> CreateAsync(Company company);
        Task<Company> UpdateAsync(Company company);
        Task<Company> RemoveAsync(Company company);
        Task<IEnumerable<Company>> GetActiveCompaniesAsync();
        Task<IEnumerable<Company>> GetCompaniesByLoyaltyCardAsync(int loyaltyCardId);
        Task<IEnumerable<Company>> GetCompaniesByNetworkAsync(int networkId);
    }
}