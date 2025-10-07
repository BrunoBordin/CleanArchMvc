using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreDTO>> GetStoresAsync();
        Task<StoreDTO> GetByIdAsync(int? id);
        Task<StoreDTO> GetByCNPJAsync(string cnpj);
        Task<StoreDTO> CreateAsync(StoreDTO storeDto);
        Task<StoreDTO> UpdateAsync(StoreDTO storeDto);
        Task<StoreDTO> RemoveAsync(int? id);
        Task<IEnumerable<StoreDTO>> GetActiveStoresAsync();
        Task<StoreDTO> ActivateAsync(int id);
        Task<StoreDTO> DeactivateAsync(int id);
    }
}