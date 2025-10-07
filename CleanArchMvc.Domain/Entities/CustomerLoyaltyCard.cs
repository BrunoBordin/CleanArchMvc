using CleanArchMvc.Domain.Enums;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class CustomerLoyaltyCard : Entity
    {
        public int CustomerId { get; private set; }
        public int LoyaltyCardId { get; private set; }
        public int StoreId { get; private set; }
        public int CurrentStamps { get; private set; }
        public DateTime? FirstStampDate { get; private set; }
        public DateTime? LastStampDate { get; private set; }
        public DateTime? ExpirationDate { get; private set; }
        public bool IsCompleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Navigation properties
        public Customer Customer { get; private set; }
        public LoyaltyCard LoyaltyCard { get; private set; }
        public Store Store { get; private set; }
        public ICollection<LoyaltyCardRedemption> Redemptions { get; private set; }

        public CustomerLoyaltyCard(int customerId, int loyaltyCardId, int storeId)
        {
            DomainExceptionValidation.When(customerId <= 0, "Invalid customer ID.");
            DomainExceptionValidation.When(loyaltyCardId <= 0, "Invalid loyalty card ID.");
            DomainExceptionValidation.When(storeId <= 0, "Invalid store ID.");

            CustomerId = customerId;
            LoyaltyCardId = loyaltyCardId;
            StoreId = storeId;
            CurrentStamps = 0;
            IsCompleted = false;
            CreatedAt = DateTime.UtcNow;
            
            Redemptions = new List<LoyaltyCardRedemption>();
        }

        public CustomerLoyaltyCard(int id, int customerId, int loyaltyCardId, int storeId,
            int currentStamps, DateTime? firstStampDate, DateTime? lastStampDate,
            DateTime? expirationDate, bool isCompleted, DateTime createdAt)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            DomainExceptionValidation.When(customerId <= 0, "Invalid customer ID.");
            DomainExceptionValidation.When(loyaltyCardId <= 0, "Invalid loyalty card ID.");
            DomainExceptionValidation.When(storeId <= 0, "Invalid store ID.");
            DomainExceptionValidation.When(currentStamps < 0, "Current stamps cannot be negative.");

            Id = id;
            CustomerId = customerId;
            LoyaltyCardId = loyaltyCardId;
            StoreId = storeId;
            CurrentStamps = currentStamps;
            FirstStampDate = firstStampDate;
            LastStampDate = lastStampDate;
            ExpirationDate = expirationDate;
            IsCompleted = isCompleted;
            CreatedAt = createdAt;
            
            Redemptions = new List<LoyaltyCardRedemption>();
        }

        public void AddStamp()
        {
            if (IsCompleted)
                throw new InvalidOperationException("Cannot add stamps to a completed loyalty card.");

            if (IsExpired())
                throw new InvalidOperationException("Cannot add stamps to an expired loyalty card.");

            CurrentStamps++;
            
            if (FirstStampDate == null)
                FirstStampDate = DateTime.UtcNow;
            
            LastStampDate = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

            // Check if card is completed
            if (CurrentStamps >= LoyaltyCard?.StampGoal)
            {
                IsCompleted = true;
            }
        }

        public void AddMultipleStamps(int stampCount)
        {
            if (IsCompleted)
                throw new InvalidOperationException("Cannot add stamps to a completed loyalty card.");

            if (IsExpired())
                throw new InvalidOperationException("Cannot add stamps to an expired loyalty card.");

            DomainExceptionValidation.When(stampCount <= 0, "Stamp count must be greater than 0.");

            CurrentStamps += stampCount;
            
            if (FirstStampDate == null)
                FirstStampDate = DateTime.UtcNow;
            
            LastStampDate = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

            // Check if card is completed
            if (CurrentStamps >= LoyaltyCard?.StampGoal)
            {
                IsCompleted = true;
            }
        }

        public void Reset()
        {
            CurrentStamps = 0;
            FirstStampDate = null;
            LastStampDate = null;
            IsCompleted = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool IsExpired()
        {
            if (ExpirationDate == null || FirstStampDate == null)
                return false;

            return DateTime.UtcNow > ExpirationDate.Value;
        }

        public int GetRemainingStamps()
        {
            if (LoyaltyCard == null)
                return 0;

            return Math.Max(0, LoyaltyCard.StampGoal - CurrentStamps);
        }

        public void SetExpirationDate(DateTime expirationDate)
        {
            ExpirationDate = expirationDate;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}