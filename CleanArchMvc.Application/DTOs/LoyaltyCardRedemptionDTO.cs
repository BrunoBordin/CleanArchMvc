using CleanArchMvc.Domain.Enums;

namespace CleanArchMvc.Application.DTOs
{
    public class LoyaltyCardRedemptionDTO
    {
        public int Id { get; set; }
        public int CustomerLoyaltyCardId { get; set; }
        public int CompanyId { get; set; }
        public int? ProductId { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? CashbackValue { get; set; }
        public RewardType RewardType { get; set; }
        public DateTime RedeemedAt { get; set; }
        public DateTime? BenefitExpirationDate { get; set; }
        public bool IsUsed { get; set; }
        public DateTime? UsedAt { get; set; }
        public string? Notes { get; set; }
        public CustomerLoyaltyCardDTO CustomerLoyaltyCard { get; set; }
        public CompanyDTO Company { get; set; }
        public ProductDTO? Product { get; set; }
        public bool IsExpired { get; set; }
    }
}