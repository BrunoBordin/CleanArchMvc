namespace CleanArchMvc.Domain.Entities
{
    public sealed class LoyaltyCardEligibleCategory : Entity
    {
        public int LoyaltyCardId { get; private set; }
        public int CategoryId { get; private set; }
        public DateTime AddedAt { get; private set; }

        // Navigation properties
        public LoyaltyCard LoyaltyCard { get; private set; }
        public Category Category { get; private set; }

        public LoyaltyCardEligibleCategory(int loyaltyCardId, int categoryId)
        {
            DomainExceptionValidation.When(loyaltyCardId <= 0, "Invalid loyalty card ID.");
            DomainExceptionValidation.When(categoryId <= 0, "Invalid category ID.");

            LoyaltyCardId = loyaltyCardId;
            CategoryId = categoryId;
            AddedAt = DateTime.UtcNow;
        }

        public LoyaltyCardEligibleCategory(int id, int loyaltyCardId, int categoryId, DateTime addedAt)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            DomainExceptionValidation.When(loyaltyCardId <= 0, "Invalid loyalty card ID.");
            DomainExceptionValidation.When(categoryId <= 0, "Invalid category ID.");

            Id = id;
            LoyaltyCardId = loyaltyCardId;
            CategoryId = categoryId;
            AddedAt = addedAt;
        }
    }
}