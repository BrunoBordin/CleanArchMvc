using CleanArchMvc.Domain.Enums;

namespace CleanArchMvc.Application.DTOs
{
    public class UpdateLoyaltyCardDTO
    {
        public int Id { get; set; }
        public string PublicName { get; set; }
        public int StampGoal { get; set; }
        public decimal ValuePerStamp { get; set; }
        public EligibilityType EligibilityType { get; set; }
        public bool AllowMultipleStampsPerPurchase { get; set; }
        public RewardType RewardType { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? CashbackValue { get; set; }
        public int? CardValidityDays { get; set; }
        public int? BenefitValidityDays { get; set; }
        public List<int> ParticipatingStoreIds { get; set; } = new List<int>();
        public List<int> EligibleProductIds { get; set; } = new List<int>();
        public List<int> EligibleCategoryIds { get; set; } = new List<int>();
        public List<int> RewardProductIds { get; set; } = new List<int>();
    }
}