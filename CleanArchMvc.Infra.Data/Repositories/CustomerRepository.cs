using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        ApplicationDbContext _customerContext;
        public CustomerRepository(ApplicationDbContext context)
        {
            _customerContext = context;
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            _customerContext.Add(customer);
            await _customerContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> GetByIdAsync(int? id)
        {
            return await _customerContext.Customers.FindAsync(id);
        }

        public async Task<Customer> GetByEmailAsync(string email)
        {
            return await _customerContext.Customers
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Customer> GetByDocumentAsync(string document)
        {
            return await _customerContext.Customers
                .FirstOrDefaultAsync(x => x.Document == document);
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _customerContext.Customers
                .Include(x => x.LoyaltyCards)
                    .ThenInclude(x => x.LoyaltyCard)
                .ToListAsync();
        }

        public async Task<Customer> RemoveAsync(Customer customer)
        {
            _customerContext.Remove(customer);
            await _customerContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            _customerContext.Update(customer);
            await _customerContext.SaveChangesAsync();
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetActiveCustomersAsync()
        {
            return await _customerContext.Customers
                .Where(x => x.IsActive)
                .Include(x => x.LoyaltyCards)
                    .ThenInclude(x => x.LoyaltyCard)
                .ToListAsync();
        }
    }
}