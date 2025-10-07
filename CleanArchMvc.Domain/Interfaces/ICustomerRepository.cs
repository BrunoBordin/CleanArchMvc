using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> GetByIdAsync(int? id);
        Task<Customer> GetByEmailAsync(string email);
        Task<Customer> GetByDocumentAsync(string document);
        Task<Customer> CreateAsync(Customer customer);
        Task<Customer> UpdateAsync(Customer customer);
        Task<Customer> RemoveAsync(Customer customer);
        Task<IEnumerable<Customer>> GetActiveCustomersAsync();
    }
}