namespace CleanArchMvc.Domain.Entities
{
    public sealed class LoyaltyCardStore : Entity
    {
        public int LoyaltyCardId { get; private set; }
        public int StoreId { get; private set; }
        public DateTime AddedAt { get; private set; }

        // Navigation properties
        public LoyaltyCard LoyaltyCard { get; private set; }
        public Store Store { get; private set; }

        public LoyaltyCardStore(int loyaltyCardId, int storeId)
        {
            DomainExceptionValidation.When(loyaltyCardId <= 0, "Invalid loyalty card ID.");
            DomainExceptionValidation.When(storeId <= 0, "Invalid store ID.");

            LoyaltyCardId = loyaltyCardId;
            StoreId = storeId;
            AddedAt = DateTime.UtcNow;
        }

        public LoyaltyCardStore(int id, int loyaltyCardId, int storeId, DateTime addedAt)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            DomainExceptionValidation.When(loyaltyCardId <= 0, "Invalid loyalty card ID.");
            DomainExceptionValidation.When(storeId <= 0, "Invalid store ID.");

            Id = id;
            LoyaltyCardId = loyaltyCardId;
            StoreId = storeId;
            AddedAt = addedAt;
        }
    }
}