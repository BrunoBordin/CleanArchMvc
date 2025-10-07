using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface ILoyaltyCardService
    {
        Task<IEnumerable<LoyaltyCardDTO>> GetLoyaltyCardsAsync();
        Task<LoyaltyCardDTO> GetByIdAsync(int? id);
        Task<LoyaltyCardDTO> CreateAsync(CreateLoyaltyCardDTO loyaltyCardDto);
        Task<LoyaltyCardDTO> UpdateAsync(UpdateLoyaltyCardDTO loyaltyCardDto);
        Task<LoyaltyCardDTO> RemoveAsync(int? id);
        Task<LoyaltyCardDTO> UpdateStatusAsync(int id, int status);
        Task<IEnumerable<LoyaltyCardDTO>> GetActiveLoyaltyCardsAsync();
        Task<LoyaltyCardDTO> GetActiveLoyaltyCardByCompanyAsync(int companyId);
        Task<LoyaltyCardDTO> GetActiveLoyaltyCardByNetworkAsync();
    }
}