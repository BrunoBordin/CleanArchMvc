using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class NetworkService : INetworkService
    {
        private INetworkRepository _networkRepository;
        private readonly IMapper _mapper;

        public NetworkService(INetworkRepository networkRepository, IMapper mapper)
        {
            _networkRepository = networkRepository;
            _mapper = mapper;
        }

        public async Task<NetworkDTO> CreateAsync(NetworkDTO networkDto)
        {
            var networkEntity = new Network(
                networkDto.Name,
                networkDto.Description,
                networkDto.CNPJ,
                networkDto.Address,
                networkDto.City,
                networkDto.State,
                networkDto.ZipCode,
                networkDto.Phone,
                networkDto.Email
            );

            var networkCreated = await _networkRepository.CreateAsync(networkEntity);
            return _mapper.Map<NetworkDTO>(networkCreated);
        }

        public async Task<NetworkDTO> GetByIdAsync(int? id)
        {
            var networkEntity = await _networkRepository.GetByIdAsync(id);
            return _mapper.Map<NetworkDTO>(networkEntity);
        }

        public async Task<NetworkDTO> GetByCNPJAsync(string cnpj)
        {
            var networkEntity = await _networkRepository.GetByCNPJAsync(cnpj);
            return _mapper.Map<NetworkDTO>(networkEntity);
        }

        public async Task<IEnumerable<NetworkDTO>> GetNetworksAsync()
        {
            var networkEntity = await _networkRepository.GetNetworksAsync();
            return _mapper.Map<IEnumerable<NetworkDTO>>(networkEntity);
        }

        public async Task<NetworkDTO> RemoveAsync(int? id)
        {
            var networkEntity = _networkRepository.GetByIdAsync(id).Result;
            var networkRemoved = await _networkRepository.RemoveAsync(networkEntity);
            return _mapper.Map<NetworkDTO>(networkRemoved);
        }

        public async Task<NetworkDTO> UpdateAsync(NetworkDTO networkDto)
        {
            var networkEntity = await _networkRepository.GetByIdAsync(networkDto.Id);
            networkEntity.Update(
                networkDto.Name,
                networkDto.Description,
                networkDto.Address,
                networkDto.City,
                networkDto.State,
                networkDto.ZipCode,
                networkDto.Phone,
                networkDto.Email
            );

            var networkUpdated = await _networkRepository.UpdateAsync(networkEntity);
            return _mapper.Map<NetworkDTO>(networkUpdated);
        }

        public async Task<IEnumerable<NetworkDTO>> GetActiveNetworksAsync()
        {
            var networkEntity = await _networkRepository.GetActiveNetworksAsync();
            return _mapper.Map<IEnumerable<NetworkDTO>>(networkEntity);
        }

        public async Task<NetworkDTO> ActivateAsync(int id)
        {
            var networkEntity = await _networkRepository.GetByIdAsync(id);
            networkEntity.Activate();
            var networkUpdated = await _networkRepository.UpdateAsync(networkEntity);
            return _mapper.Map<NetworkDTO>(networkUpdated);
        }

        public async Task<NetworkDTO> DeactivateAsync(int id)
        {
            var networkEntity = await _networkRepository.GetByIdAsync(id);
            networkEntity.Deactivate();
            var networkUpdated = await _networkRepository.UpdateAsync(networkEntity);
            return _mapper.Map<NetworkDTO>(networkUpdated);
        }
    }
}