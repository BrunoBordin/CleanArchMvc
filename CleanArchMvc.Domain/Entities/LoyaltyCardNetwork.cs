namespace CleanArchMvc.Domain.Entities
{
    public sealed class LoyaltyCardNetwork : Entity
    {
        public int LoyaltyCardId { get; private set; }
        public int NetworkId { get; private set; }
        public DateTime AddedAt { get; private set; }

        // Navigation properties
        public LoyaltyCard LoyaltyCard { get; private set; }
        public Network Network { get; private set; }

        public LoyaltyCardNetwork(int loyaltyCardId, int networkId)
        {
            DomainExceptionValidation.When(loyaltyCardId <= 0, "Invalid loyalty card ID.");
            DomainExceptionValidation.When(networkId <= 0, "Invalid network ID.");

            LoyaltyCardId = loyaltyCardId;
            NetworkId = networkId;
            AddedAt = DateTime.UtcNow;
        }

        public LoyaltyCardNetwork(int id, int loyaltyCardId, int networkId, DateTime addedAt)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            DomainExceptionValidation.When(loyaltyCardId <= 0, "Invalid loyalty card ID.");
            DomainExceptionValidation.When(networkId <= 0, "Invalid network ID.");

            Id = id;
            LoyaltyCardId = loyaltyCardId;
            NetworkId = networkId;
            AddedAt = addedAt;
        }
    }
}