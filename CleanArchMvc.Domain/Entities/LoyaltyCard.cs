using CleanArchMvc.Domain.Enums;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class LoyaltyCard : Entity
    {
        public string PublicName { get; private set; }
        public LoyaltyCardStatus Status { get; private set; }
        public int StampGoal { get; private set; }
        public decimal ValuePerStamp { get; private set; }
        public EligibilityType EligibilityType { get; private set; }
        public bool AllowMultipleStampsPerPurchase { get; private set; }
        public LoyaltyCardScope Scope { get; private set; }
        public RewardType RewardType { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public decimal? CashbackValue { get; private set; }
        public int? CardValidityDays { get; private set; }
        public int? BenefitValidityDays { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Navigation properties
        public ICollection<LoyaltyCardStore> ParticipatingStores { get; private set; }
        public ICollection<LoyaltyCardEligibleProduct> EligibleProducts { get; private set; }
        public ICollection<LoyaltyCardEligibleCategory> EligibleCategories { get; private set; }
        public ICollection<LoyaltyCardRewardProduct> RewardProducts { get; private set; }
        public ICollection<CustomerLoyaltyCard> CustomerCards { get; private set; }

        public LoyaltyCard(
            string publicName,
            int stampGoal,
            decimal valuePerStamp,
            EligibilityType eligibilityType,
            bool allowMultipleStampsPerPurchase,
            LoyaltyCardScope scope,
            RewardType rewardType,
            decimal? discountValue = null,
            decimal? cashbackValue = null,
            int? cardValidityDays = null,
            int? benefitValidityDays = null)
        {
            ValidateDomain(publicName, stampGoal, valuePerStamp, rewardType, discountValue, cashbackValue);
            
            PublicName = publicName;
            Status = LoyaltyCardStatus.Active;
            StampGoal = stampGoal;
            ValuePerStamp = valuePerStamp;
            EligibilityType = eligibilityType;
            AllowMultipleStampsPerPurchase = allowMultipleStampsPerPurchase;
            Scope = scope;
            RewardType = rewardType;
            DiscountValue = discountValue;
            CashbackValue = cashbackValue;
            CardValidityDays = cardValidityDays;
            BenefitValidityDays = benefitValidityDays;
            CreatedAt = DateTime.UtcNow;
            
            ParticipatingStores = new List<LoyaltyCardStore>();
            EligibleProducts = new List<LoyaltyCardEligibleProduct>();
            EligibleCategories = new List<LoyaltyCardEligibleCategory>();
            RewardProducts = new List<LoyaltyCardRewardProduct>();
            CustomerCards = new List<CustomerLoyaltyCard>();
        }

        public LoyaltyCard(int id, string publicName, LoyaltyCardStatus status, int stampGoal, 
            decimal valuePerStamp, EligibilityType eligibilityType, bool allowMultipleStampsPerPurchase,
            LoyaltyCardScope scope, RewardType rewardType, decimal? discountValue = null,
            decimal? cashbackValue = null, int? cardValidityDays = null, int? benefitValidityDays = null,
            DateTime createdAt = default)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            Id = id;
            ValidateDomain(publicName, stampGoal, valuePerStamp, rewardType, discountValue, cashbackValue);
            
            PublicName = publicName;
            Status = status;
            StampGoal = stampGoal;
            ValuePerStamp = valuePerStamp;
            EligibilityType = eligibilityType;
            AllowMultipleStampsPerPurchase = allowMultipleStampsPerPurchase;
            Scope = scope;
            RewardType = rewardType;
            DiscountValue = discountValue;
            CashbackValue = cashbackValue;
            CardValidityDays = cardValidityDays;
            BenefitValidityDays = benefitValidityDays;
            CreatedAt = createdAt == default ? DateTime.UtcNow : createdAt;
            
            ParticipatingStores = new List<LoyaltyCardStore>();
            EligibleProducts = new List<LoyaltyCardEligibleProduct>();
            EligibleCategories = new List<LoyaltyCardEligibleCategory>();
            RewardProducts = new List<LoyaltyCardRewardProduct>();
            CustomerCards = new List<CustomerLoyaltyCard>();
        }

        public void UpdateStatus(LoyaltyCardStatus status)
        {
            Status = status;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateConfiguration(string publicName, int stampGoal, decimal valuePerStamp,
            EligibilityType eligibilityType, bool allowMultipleStampsPerPurchase, RewardType rewardType,
            decimal? discountValue = null, decimal? cashbackValue = null, int? cardValidityDays = null,
            int? benefitValidityDays = null)
        {
            ValidateDomain(publicName, stampGoal, valuePerStamp, rewardType, discountValue, cashbackValue);
            
            PublicName = publicName;
            StampGoal = stampGoal;
            ValuePerStamp = valuePerStamp;
            EligibilityType = eligibilityType;
            AllowMultipleStampsPerPurchase = allowMultipleStampsPerPurchase;
            RewardType = rewardType;
            DiscountValue = discountValue;
            CashbackValue = cashbackValue;
            CardValidityDays = cardValidityDays;
            BenefitValidityDays = benefitValidityDays;
            UpdatedAt = DateTime.UtcNow;
        }

        private void ValidateDomain(string publicName, int stampGoal, decimal valuePerStamp,
            RewardType rewardType, decimal? discountValue, decimal? cashbackValue)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(publicName),
                "Invalid public name. Public name is required.");

            DomainExceptionValidation.When(publicName.Length < 3,
                "Invalid public name, too short, minimum 3 characters.");

            DomainExceptionValidation.When(stampGoal <= 0,
                "Invalid stamp goal. Must be greater than 0.");

            DomainExceptionValidation.When(valuePerStamp <= 0,
                "Invalid value per stamp. Must be greater than 0.");

            if (rewardType == RewardType.Discount)
            {
                DomainExceptionValidation.When(!discountValue.HasValue || discountValue <= 0,
                    "Discount value is required when reward type is Discount.");
            }

            if (rewardType == RewardType.Cashback)
            {
                DomainExceptionValidation.When(!cashbackValue.HasValue || cashbackValue <= 0,
                    "Cashback value is required when reward type is Cashback.");
            }
        }
    }
}