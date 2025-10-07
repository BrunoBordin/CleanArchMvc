namespace CleanArchMvc.Application.DTOs
{
    public class AddStampDTO
    {
        public int CustomerId { get; set; }
        public int LoyaltyCardId { get; set; }
        public int StoreId { get; set; }
        public int StampCount { get; set; } = 1;
        public decimal PurchaseValue { get; set; }
        public List<int> ProductIds { get; set; } = new List<int>();
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}