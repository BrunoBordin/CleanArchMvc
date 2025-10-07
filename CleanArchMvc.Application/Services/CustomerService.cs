using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDTO> CreateAsync(CustomerDTO customerDto)
        {
            var customerEntity = new Customer(
                customerDto.Name,
                customerDto.Email,
                customerDto.Phone,
                customerDto.Document,
                customerDto.BirthDate
            );

            var customerCreated = await _customerRepository.CreateAsync(customerEntity);
            return _mapper.Map<CustomerDTO>(customerCreated);
        }

        public async Task<CustomerDTO> GetByIdAsync(int? id)
        {
            var customerEntity = await _customerRepository.GetByIdAsync(id);
            return _mapper.Map<CustomerDTO>(customerEntity);
        }

        public async Task<CustomerDTO> GetByEmailAsync(string email)
        {
            var customerEntity = await _customerRepository.GetByEmailAsync(email);
            return _mapper.Map<CustomerDTO>(customerEntity);
        }

        public async Task<CustomerDTO> GetByDocumentAsync(string document)
        {
            var customerEntity = await _customerRepository.GetByDocumentAsync(document);
            return _mapper.Map<CustomerDTO>(customerEntity);
        }

        public async Task<IEnumerable<CustomerDTO>> GetCustomersAsync()
        {
            var customerEntity = await _customerRepository.GetCustomersAsync();
            return _mapper.Map<IEnumerable<CustomerDTO>>(customerEntity);
        }

        public async Task<CustomerDTO> RemoveAsync(int? id)
        {
            var customerEntity = _customerRepository.GetByIdAsync(id).Result;
            var customerRemoved = await _customerRepository.RemoveAsync(customerEntity);
            return _mapper.Map<CustomerDTO>(customerRemoved);
        }

        public async Task<CustomerDTO> UpdateAsync(CustomerDTO customerDto)
        {
            var customerEntity = await _customerRepository.GetByIdAsync(customerDto.Id);
            customerEntity.Update(
                customerDto.Name,
                customerDto.Email,
                customerDto.Phone,
                customerDto.Document,
                customerDto.BirthDate
            );

            var customerUpdated = await _customerRepository.UpdateAsync(customerEntity);
            return _mapper.Map<CustomerDTO>(customerUpdated);
        }

        public async Task<IEnumerable<CustomerDTO>> GetActiveCustomersAsync()
        {
            var customerEntity = await _customerRepository.GetActiveCustomersAsync();
            return _mapper.Map<IEnumerable<CustomerDTO>>(customerEntity);
        }

        public async Task<CustomerDTO> ActivateAsync(int id)
        {
            var customerEntity = await _customerRepository.GetByIdAsync(id);
            customerEntity.Activate();
            var customerUpdated = await _customerRepository.UpdateAsync(customerEntity);
            return _mapper.Map<CustomerDTO>(customerUpdated);
        }

        public async Task<CustomerDTO> DeactivateAsync(int id)
        {
            var customerEntity = await _customerRepository.GetByIdAsync(id);
            customerEntity.Deactivate();
            var customerUpdated = await _customerRepository.UpdateAsync(customerEntity);
            return _mapper.Map<CustomerDTO>(customerUpdated);
        }
    }
}