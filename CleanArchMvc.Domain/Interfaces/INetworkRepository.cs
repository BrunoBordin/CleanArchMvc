using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface INetworkRepository
    {
        Task<IEnumerable<Network>> GetNetworksAsync();
        Task<Network> GetByIdAsync(int? id);
        Task<Network> GetByCNPJAsync(string cnpj);
        Task<Network> CreateAsync(Network network);
        Task<Network> UpdateAsync(Network network);
        Task<Network> RemoveAsync(Network network);
        Task<IEnumerable<Network>> GetActiveNetworksAsync();
        Task<IEnumerable<Network>> GetNetworksByLoyaltyCardAsync(int loyaltyCardId);
    }
}