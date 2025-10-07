namespace CleanArchMvc.Domain.Entities
{
    public sealed class LoyaltyCardCompany : Entity
    {
        public int LoyaltyCardId { get; private set; }
        public int CompanyId { get; private set; }
        public DateTime AddedAt { get; private set; }

        // Navigation properties
        public LoyaltyCard LoyaltyCard { get; private set; }
        public Company Company { get; private set; }

        public LoyaltyCardCompany(int loyaltyCardId, int companyId)
        {
            DomainExceptionValidation.When(loyaltyCardId <= 0, "Invalid loyalty card ID.");
            DomainExceptionValidation.When(companyId <= 0, "Invalid company ID.");

            LoyaltyCardId = loyaltyCardId;
            CompanyId = companyId;
            AddedAt = DateTime.UtcNow;
        }

        public LoyaltyCardCompany(int id, int loyaltyCardId, int companyId, DateTime addedAt)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            DomainExceptionValidation.When(loyaltyCardId <= 0, "Invalid loyalty card ID.");
            DomainExceptionValidation.When(companyId <= 0, "Invalid company ID.");

            Id = id;
            LoyaltyCardId = loyaltyCardId;
            CompanyId = companyId;
            AddedAt = addedAt;
        }
    }
}