using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Customer : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Document { get; private set; } // CPF ou CNPJ
        public DateTime? BirthDate { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Navigation properties
        public ICollection<CustomerLoyaltyCard> LoyaltyCards { get; private set; }
        public ICollection<LoyaltyCardRedemption> Redemptions { get; private set; }

        public Customer(string name, string email, string phone, string document, DateTime? birthDate = null)
        {
            ValidateDomain(name, email, phone, document);
            
            Name = name;
            Email = email;
            Phone = phone;
            Document = document;
            BirthDate = birthDate;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            
            LoyaltyCards = new List<CustomerLoyaltyCard>();
            Redemptions = new List<LoyaltyCardRedemption>();
        }

        public Customer(int id, string name, string email, string phone, string document, 
            DateTime? birthDate, bool isActive, DateTime createdAt)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            Id = id;
            ValidateDomain(name, email, phone, document);
            
            Name = name;
            Email = email;
            Phone = phone;
            Document = document;
            BirthDate = birthDate;
            IsActive = isActive;
            CreatedAt = createdAt;
            
            LoyaltyCards = new List<CustomerLoyaltyCard>();
            Redemptions = new List<LoyaltyCardRedemption>();
        }

        public void Update(string name, string email, string phone, string document, DateTime? birthDate = null)
        {
            ValidateDomain(name, email, phone, document);
            
            Name = name;
            Email = email;
            Phone = phone;
            Document = document;
            BirthDate = birthDate;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        private void ValidateDomain(string name, string email, string phone, string document)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Invalid name. Name is required.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(email),
                "Invalid email. Email is required.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(phone),
                "Invalid phone. Phone is required.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(document),
                "Invalid document. Document is required.");

            DomainExceptionValidation.When(!IsValidEmail(email),
                "Invalid email format.");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}