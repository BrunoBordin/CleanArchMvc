namespace CleanArchMvc.Domain.Entities
{
    public sealed class LoyaltyCardEligibleProduct : Entity
    {
        public int LoyaltyCardId { get; private set; }
        public int ProductId { get; private set; }
        public DateTime AddedAt { get; private set; }

        // Navigation properties
        public LoyaltyCard LoyaltyCard { get; private set; }
        public Product Product { get; private set; }

        public LoyaltyCardEligibleProduct(int loyaltyCardId, int productId)
        {
            DomainExceptionValidation.When(loyaltyCardId <= 0, "Invalid loyalty card ID.");
            DomainExceptionValidation.When(productId <= 0, "Invalid product ID.");

            LoyaltyCardId = loyaltyCardId;
            ProductId = productId;
            AddedAt = DateTime.UtcNow;
        }

        public LoyaltyCardEligibleProduct(int id, int loyaltyCardId, int productId, DateTime addedAt)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            DomainExceptionValidation.When(loyaltyCardId <= 0, "Invalid loyalty card ID.");
            DomainExceptionValidation.When(productId <= 0, "Invalid product ID.");

            Id = id;
            LoyaltyCardId = loyaltyCardId;
            ProductId = productId;
            AddedAt = addedAt;
        }
    }
}