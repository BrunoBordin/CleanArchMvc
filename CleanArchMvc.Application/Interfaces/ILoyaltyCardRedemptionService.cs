using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface ILoyaltyCardRedemptionService
    {
        Task<IEnumerable<LoyaltyCardRedemptionDTO>> GetRedemptionsAsync();
        Task<LoyaltyCardRedemptionDTO> GetByIdAsync(int? id);
        Task<LoyaltyCardRedemptionDTO> CreateAsync(RedeemRewardDTO redeemRewardDto);
        Task<LoyaltyCardRedemptionDTO> MarkAsUsedAsync(int id, string? notes = null);
        Task<IEnumerable<LoyaltyCardRedemptionDTO>> GetByCustomerLoyaltyCardAsync(int customerLoyaltyCardId);
        Task<IEnumerable<LoyaltyCardRedemptionDTO>> GetByCompanyAsync(int companyId);
        Task<IEnumerable<LoyaltyCardRedemptionDTO>> GetUnusedRedemptionsAsync();
        Task<IEnumerable<LoyaltyCardRedemptionDTO>> GetExpiredRedemptionsAsync();
        Task<IEnumerable<LoyaltyCardRedemptionDTO>> GetByCustomerAsync(int customerId);
    }
}