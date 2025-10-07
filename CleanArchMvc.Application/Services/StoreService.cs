using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class StoreService : IStoreService
    {
        private IStoreRepository _storeRepository;
        private readonly IMapper _mapper;

        public StoreService(IStoreRepository storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        public async Task<StoreDTO> CreateAsync(StoreDTO storeDto)
        {
            var storeEntity = new Store(
                storeDto.Name,
                storeDto.CNPJ,
                storeDto.Address,
                storeDto.City,
                storeDto.State,
                storeDto.ZipCode,
                storeDto.Phone,
                storeDto.Email
            );

            var storeCreated = await _storeRepository.CreateAsync(storeEntity);
            return _mapper.Map<StoreDTO>(storeCreated);
        }

        public async Task<StoreDTO> GetByIdAsync(int? id)
        {
            var storeEntity = await _storeRepository.GetByIdAsync(id);
            return _mapper.Map<StoreDTO>(storeEntity);
        }

        public async Task<StoreDTO> GetByCNPJAsync(string cnpj)
        {
            var storeEntity = await _storeRepository.GetByCNPJAsync(cnpj);
            return _mapper.Map<StoreDTO>(storeEntity);
        }

        public async Task<IEnumerable<StoreDTO>> GetStoresAsync()
        {
            var storeEntity = await _storeRepository.GetStoresAsync();
            return _mapper.Map<IEnumerable<StoreDTO>>(storeEntity);
        }

        public async Task<StoreDTO> RemoveAsync(int? id)
        {
            var storeEntity = _storeRepository.GetByIdAsync(id).Result;
            var storeRemoved = await _storeRepository.RemoveAsync(storeEntity);
            return _mapper.Map<StoreDTO>(storeRemoved);
        }

        public async Task<StoreDTO> UpdateAsync(StoreDTO storeDto)
        {
            var storeEntity = await _storeRepository.GetByIdAsync(storeDto.Id);
            storeEntity.Update(
                storeDto.Name,
                storeDto.Address,
                storeDto.City,
                storeDto.State,
                storeDto.ZipCode,
                storeDto.Phone,
                storeDto.Email
            );

            var storeUpdated = await _storeRepository.UpdateAsync(storeEntity);
            return _mapper.Map<StoreDTO>(storeUpdated);
        }

        public async Task<IEnumerable<StoreDTO>> GetActiveStoresAsync()
        {
            var storeEntity = await _storeRepository.GetActiveStoresAsync();
            return _mapper.Map<IEnumerable<StoreDTO>>(storeEntity);
        }

        public async Task<StoreDTO> ActivateAsync(int id)
        {
            var storeEntity = await _storeRepository.GetByIdAsync(id);
            storeEntity.Activate();
            var storeUpdated = await _storeRepository.UpdateAsync(storeEntity);
            return _mapper.Map<StoreDTO>(storeUpdated);
        }

        public async Task<StoreDTO> DeactivateAsync(int id)
        {
            var storeEntity = await _storeRepository.GetByIdAsync(id);
            storeEntity.Deactivate();
            var storeUpdated = await _storeRepository.UpdateAsync(storeEntity);
            return _mapper.Map<StoreDTO>(storeUpdated);
        }
    }
}