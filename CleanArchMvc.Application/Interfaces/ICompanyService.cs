using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDTO>> GetCompaniesAsync();
        Task<CompanyDTO> GetByIdAsync(int? id);
        Task<CompanyDTO> GetByCNPJAsync(string cnpj);
        Task<CompanyDTO> CreateAsync(CompanyDTO companyDto);
        Task<CompanyDTO> UpdateAsync(CompanyDTO companyDto);
        Task<CompanyDTO> RemoveAsync(int? id);
        Task<IEnumerable<CompanyDTO>> GetActiveCompaniesAsync();
        Task<IEnumerable<CompanyDTO>> GetCompaniesByNetworkAsync(int networkId);
        Task<CompanyDTO> ActivateAsync(int id);
        Task<CompanyDTO> DeactivateAsync(int id);
    }
}