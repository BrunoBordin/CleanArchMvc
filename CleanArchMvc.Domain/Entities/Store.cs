using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Store : Entity
    {
        public string Name { get; private set; }
        public string CNPJ { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Navigation properties
        public ICollection<LoyaltyCardStore> LoyaltyCards { get; private set; }
        public ICollection<CustomerLoyaltyCard> CustomerCards { get; private set; }
        public ICollection<LoyaltyCardRedemption> Redemptions { get; private set; }

        public Store(string name, string cnpj, string address, string city, string state, 
            string zipCode, string phone, string email)
        {
            ValidateDomain(name, cnpj, address, city, state, zipCode, phone, email);
            
            Name = name;
            CNPJ = cnpj;
            Address = address;
            City = city;
            State = state;
            ZipCode = zipCode;
            Phone = phone;
            Email = email;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            
            LoyaltyCards = new List<LoyaltyCardStore>();
            CustomerCards = new List<CustomerLoyaltyCard>();
            Redemptions = new List<LoyaltyCardRedemption>();
        }

        public Store(int id, string name, string cnpj, string address, string city, string state,
            string zipCode, string phone, string email, bool isActive, DateTime createdAt)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            Id = id;
            ValidateDomain(name, cnpj, address, city, state, zipCode, phone, email);
            
            Name = name;
            CNPJ = cnpj;
            Address = address;
            City = city;
            State = state;
            ZipCode = zipCode;
            Phone = phone;
            Email = email;
            IsActive = isActive;
            CreatedAt = createdAt;
            
            LoyaltyCards = new List<LoyaltyCardStore>();
            CustomerCards = new List<CustomerLoyaltyCard>();
            Redemptions = new List<LoyaltyCardRedemption>();
        }

        public void Update(string name, string address, string city, string state, 
            string zipCode, string phone, string email)
        {
            ValidateDomain(name, CNPJ, address, city, state, zipCode, phone, email);
            
            Name = name;
            Address = address;
            City = city;
            State = state;
            ZipCode = zipCode;
            Phone = phone;
            Email = email;
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

        private void ValidateDomain(string name, string cnpj, string address, string city, 
            string state, string zipCode, string phone, string email)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Invalid name. Name is required.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(cnpj),
                "Invalid CNPJ. CNPJ is required.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(address),
                "Invalid address. Address is required.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(city),
                "Invalid city. City is required.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(state),
                "Invalid state. State is required.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(zipCode),
                "Invalid zip code. Zip code is required.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(phone),
                "Invalid phone. Phone is required.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(email),
                "Invalid email. Email is required.");

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