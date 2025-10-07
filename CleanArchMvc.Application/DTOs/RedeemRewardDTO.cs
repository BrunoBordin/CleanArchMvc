using CleanArchMvc.Domain.Enums;

namespace CleanArchMvc.Application.DTOs
{
    public class RedeemRewardDTO
    {
        public int CustomerLoyaltyCardId { get; set; }
        public int CompanyId { get; set; }
        public RewardType RewardType { get; set; }
        public int? ProductId { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? CashbackValue { get; set; }
        public string? Notes { get; set; }
    }
}