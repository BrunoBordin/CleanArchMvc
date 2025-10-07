using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetCustomersAsync();
        Task<CustomerDTO> GetByIdAsync(int? id);
        Task<CustomerDTO> GetByEmailAsync(string email);
        Task<CustomerDTO> GetByDocumentAsync(string document);
        Task<CustomerDTO> CreateAsync(CustomerDTO customerDto);
        Task<CustomerDTO> UpdateAsync(CustomerDTO customerDto);
        Task<CustomerDTO> RemoveAsync(int? id);
        Task<IEnumerable<CustomerDTO>> GetActiveCustomersAsync();
        Task<CustomerDTO> ActivateAsync(int id);
        Task<CustomerDTO> DeactivateAsync(int id);
    }
}