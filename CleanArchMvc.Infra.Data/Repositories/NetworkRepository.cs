using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class NetworkRepository : INetworkRepository
    {
        ApplicationDbContext _networkContext;
        public NetworkRepository(ApplicationDbContext context)
        {
            _networkContext = context;
        }

        public async Task<Network> CreateAsync(Network network)
        {
            _networkContext.Add(network);
            await _networkContext.SaveChangesAsync();
            return network;
        }

        public async Task<Network> GetByIdAsync(int? id)
        {
            return await _networkContext.Networks.FindAsync(id);
        }

        public async Task<Network> GetByCNPJAsync(string cnpj)
        {
            return await _networkContext.Networks
                .FirstOrDefaultAsync(x => x.CNPJ == cnpj);
        }

        public async Task<IEnumerable<Network>> GetNetworksAsync()
        {
            return await _networkContext.Networks
                .Include(x => x.Companies)
                .Include(x => x.LoyaltyCards)
                    .ThenInclude(x => x.LoyaltyCard)
                .ToListAsync();
        }

        public async Task<Network> RemoveAsync(Network network)
        {
            _networkContext.Remove(network);
            await _networkContext.SaveChangesAsync();
            return network;
        }

        public async Task<Network> UpdateAsync(Network network)
        {
            _networkContext.Update(network);
            await _networkContext.SaveChangesAsync();
            return network;
        }

        public async Task<IEnumerable<Network>> GetActiveNetworksAsync()
        {
            return await _networkContext.Networks
                .Where(x => x.IsActive)
                .Include(x => x.Companies)
                .Include(x => x.LoyaltyCards)
                    .ThenInclude(x => x.LoyaltyCard)
                .ToListAsync();
        }

        public async Task<IEnumerable<Network>> GetNetworksByLoyaltyCardAsync(int loyaltyCardId)
        {
            return await _networkContext.Networks
                .Where(x => x.LoyaltyCards.Any(lc => lc.LoyaltyCardId == loyaltyCardId))
                .Include(x => x.Companies)
                .Include(x => x.LoyaltyCards)
                    .ThenInclude(x => x.LoyaltyCard)
                .ToListAsync();
        }
    }
}