using CleanArchMvc.Domain.Enums;

namespace CleanArchMvc.Application.DTOs
{
    public class CustomerLoyaltyCardDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int LoyaltyCardId { get; set; }
        public int StoreId { get; set; }
        public int CurrentStamps { get; set; }
        public DateTime? FirstStampDate { get; set; }
        public DateTime? LastStampDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public CustomerDTO Customer { get; set; }
        public LoyaltyCardDTO LoyaltyCard { get; set; }
        public StoreDTO Store { get; set; }
        public List<LoyaltyCardRedemptionDTO> Redemptions { get; set; } = new List<LoyaltyCardRedemptionDTO>();
        public int RemainingStamps { get; set; }
        public bool IsExpired { get; set; }
    }
}