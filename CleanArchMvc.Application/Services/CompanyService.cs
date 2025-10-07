using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<CompanyDTO> CreateAsync(CompanyDTO companyDto)
        {
            var companyEntity = new Company(
                companyDto.Name,
                companyDto.CNPJ,
                companyDto.Address,
                companyDto.City,
                companyDto.State,
                companyDto.ZipCode,
                companyDto.Phone,
                companyDto.Email,
                companyDto.NetworkId
            );

            var companyCreated = await _companyRepository.CreateAsync(companyEntity);
            return _mapper.Map<CompanyDTO>(companyCreated);
        }

        public async Task<CompanyDTO> GetByIdAsync(int? id)
        {
            var companyEntity = await _companyRepository.GetByIdAsync(id);
            return _mapper.Map<CompanyDTO>(companyEntity);
        }

        public async Task<CompanyDTO> GetByCNPJAsync(string cnpj)
        {
            var companyEntity = await _companyRepository.GetByCNPJAsync(cnpj);
            return _mapper.Map<CompanyDTO>(companyEntity);
        }

        public async Task<IEnumerable<CompanyDTO>> GetCompaniesAsync()
        {
            var companyEntity = await _companyRepository.GetCompaniesAsync();
            return _mapper.Map<IEnumerable<CompanyDTO>>(companyEntity);
        }

        public async Task<CompanyDTO> RemoveAsync(int? id)
        {
            var companyEntity = _companyRepository.GetByIdAsync(id).Result;
            var companyRemoved = await _companyRepository.RemoveAsync(companyEntity);
            return _mapper.Map<CompanyDTO>(companyRemoved);
        }

        public async Task<CompanyDTO> UpdateAsync(CompanyDTO companyDto)
        {
            var companyEntity = await _companyRepository.GetByIdAsync(companyDto.Id);
            companyEntity.Update(
                companyDto.Name,
                companyDto.Address,
                companyDto.City,
                companyDto.State,
                companyDto.ZipCode,
                companyDto.Phone,
                companyDto.Email
            );

            var companyUpdated = await _companyRepository.UpdateAsync(companyEntity);
            return _mapper.Map<CompanyDTO>(companyUpdated);
        }

        public async Task<IEnumerable<CompanyDTO>> GetActiveCompaniesAsync()
        {
            var companyEntity = await _companyRepository.GetActiveCompaniesAsync();
            return _mapper.Map<IEnumerable<CompanyDTO>>(companyEntity);
        }

        public async Task<IEnumerable<CompanyDTO>> GetCompaniesByNetworkAsync(int networkId)
        {
            var companyEntity = await _companyRepository.GetCompaniesByNetworkAsync(networkId);
            return _mapper.Map<IEnumerable<CompanyDTO>>(companyEntity);
        }

        public async Task<CompanyDTO> ActivateAsync(int id)
        {
            var companyEntity = await _companyRepository.GetByIdAsync(id);
            companyEntity.Activate();
            var companyUpdated = await _companyRepository.UpdateAsync(companyEntity);
            return _mapper.Map<CompanyDTO>(companyUpdated);
        }

        public async Task<CompanyDTO> DeactivateAsync(int id)
        {
            var companyEntity = await _companyRepository.GetByIdAsync(id);
            companyEntity.Deactivate();
            var companyUpdated = await _companyRepository.UpdateAsync(companyEntity);
            return _mapper.Map<CompanyDTO>(companyUpdated);
        }
    }
}