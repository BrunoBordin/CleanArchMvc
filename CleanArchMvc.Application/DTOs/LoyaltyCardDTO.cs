using CleanArchMvc.Domain.Enums;

namespace CleanArchMvc.Application.DTOs
{
    public class LoyaltyCardDTO
    {
        public int Id { get; set; }
        public string PublicName { get; set; }
        public LoyaltyCardStatus Status { get; set; }
        public int StampGoal { get; set; }
        public decimal ValuePerStamp { get; set; }
        public EligibilityType EligibilityType { get; set; }
        public bool AllowMultipleStampsPerPurchase { get; set; }
        public LoyaltyCardScope Scope { get; set; }
        public RewardType RewardType { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? CashbackValue { get; set; }
        public int? CardValidityDays { get; set; }
        public int? BenefitValidityDays { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<NetworkDTO> ParticipatingNetworks { get; set; } = new List<NetworkDTO>();
        public List<CompanyDTO> ParticipatingCompanies { get; set; } = new List<CompanyDTO>();
        public List<ProductDTO> EligibleProducts { get; set; } = new List<ProductDTO>();
        public List<CategoryDTO> EligibleCategories { get; set; } = new List<CategoryDTO>();
        public List<ProductDTO> RewardProducts { get; set; } = new List<ProductDTO>();
    }
}