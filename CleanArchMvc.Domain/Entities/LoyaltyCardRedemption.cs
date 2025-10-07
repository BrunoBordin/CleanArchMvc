using CleanArchMvc.Domain.Enums;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class LoyaltyCardRedemption : Entity
    {
        public int CustomerLoyaltyCardId { get; private set; }
        public int CompanyId { get; private set; }
        public int? ProductId { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public decimal? CashbackValue { get; private set; }
        public RewardType RewardType { get; private set; }
        public DateTime RedeemedAt { get; private set; }
        public DateTime? BenefitExpirationDate { get; private set; }
        public bool IsUsed { get; private set; }
        public DateTime? UsedAt { get; private set; }
        public string? Notes { get; private set; }

        // Navigation properties
        public CustomerLoyaltyCard CustomerLoyaltyCard { get; private set; }
        public Company Company { get; private set; }
        public Product? Product { get; private set; }

        public LoyaltyCardRedemption(int customerLoyaltyCardId, int companyId, RewardType rewardType,
            int? productId = null, decimal? discountValue = null, decimal? cashbackValue = null,
            DateTime? benefitExpirationDate = null, string? notes = null)
        {
            DomainExceptionValidation.When(customerLoyaltyCardId <= 0, "Invalid customer loyalty card ID.");
            DomainExceptionValidation.When(companyId <= 0, "Invalid company ID.");

            ValidateRewardType(rewardType, productId, discountValue, cashbackValue);

            CustomerLoyaltyCardId = customerLoyaltyCardId;
            CompanyId = companyId;
            RewardType = rewardType;
            ProductId = productId;
            DiscountValue = discountValue;
            CashbackValue = cashbackValue;
            BenefitExpirationDate = benefitExpirationDate;
            Notes = notes;
            RedeemedAt = DateTime.UtcNow;
            IsUsed = false;
        }

        public LoyaltyCardRedemption(int id, int customerLoyaltyCardId, int companyId, RewardType rewardType,
            int? productId, decimal? discountValue, decimal? cashbackValue, DateTime redeemedAt,
            DateTime? benefitExpirationDate, bool isUsed, DateTime? usedAt, string? notes)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            DomainExceptionValidation.When(customerLoyaltyCardId <= 0, "Invalid customer loyalty card ID.");
            DomainExceptionValidation.When(companyId <= 0, "Invalid company ID.");

            Id = id;
            CustomerLoyaltyCardId = customerLoyaltyCardId;
            CompanyId = companyId;
            RewardType = rewardType;
            ProductId = productId;
            DiscountValue = discountValue;
            CashbackValue = cashbackValue;
            RedeemedAt = redeemedAt;
            BenefitExpirationDate = benefitExpirationDate;
            IsUsed = isUsed;
            UsedAt = usedAt;
            Notes = notes;
        }

        public void MarkAsUsed(string? notes = null)
        {
            if (IsUsed)
                throw new InvalidOperationException("Redemption is already marked as used.");

            if (IsExpired())
                throw new InvalidOperationException("Cannot use an expired redemption.");

            IsUsed = true;
            UsedAt = DateTime.UtcNow;
            if (!string.IsNullOrEmpty(notes))
                Notes = notes;
        }

        public bool IsExpired()
        {
            if (BenefitExpirationDate == null)
                return false;

            return DateTime.UtcNow > BenefitExpirationDate.Value;
        }

        private void ValidateRewardType(RewardType rewardType, int? productId, decimal? discountValue, decimal? cashbackValue)
        {
            switch (rewardType)
            {
                case RewardType.Product:
                    DomainExceptionValidation.When(!productId.HasValue || productId <= 0,
                        "Product ID is required when reward type is Product.");
                    break;
                case RewardType.Discount:
                    DomainExceptionValidation.When(!discountValue.HasValue || discountValue <= 0,
                        "Discount value is required when reward type is Discount.");
                    break;
                case RewardType.Cashback:
                    DomainExceptionValidation.When(!cashbackValue.HasValue || cashbackValue <= 0,
                        "Cashback value is required when reward type is Cashback.");
                    break;
            }
        }
    }
}