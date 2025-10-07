using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface INetworkService
    {
        Task<IEnumerable<NetworkDTO>> GetNetworksAsync();
        Task<NetworkDTO> GetByIdAsync(int? id);
        Task<NetworkDTO> GetByCNPJAsync(string cnpj);
        Task<NetworkDTO> CreateAsync(NetworkDTO networkDto);
        Task<NetworkDTO> UpdateAsync(NetworkDTO networkDto);
        Task<NetworkDTO> RemoveAsync(int? id);
        Task<IEnumerable<NetworkDTO>> GetActiveNetworksAsync();
        Task<NetworkDTO> ActivateAsync(int id);
        Task<NetworkDTO> DeactivateAsync(int id);
    }
}