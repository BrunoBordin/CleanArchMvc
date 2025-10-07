using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Network : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
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
        public ICollection<Company> Companies { get; private set; }
        public ICollection<LoyaltyCardNetwork> LoyaltyCards { get; private set; }

        public Network(string name, string description, string cnpj, string address, string city, 
            string state, string zipCode, string phone, string email)
        {
            ValidateDomain(name, description, cnpj, address, city, state, zipCode, phone, email);
            
            Name = name;
            Description = description;
            CNPJ = cnpj;
            Address = address;
            City = city;
            State = state;
            ZipCode = zipCode;
            Phone = phone;
            Email = email;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            
            Companies = new List<Company>();
            LoyaltyCards = new List<LoyaltyCardNetwork>();
        }

        public Network(int id, string name, string description, string cnpj, string address, 
            string city, string state, string zipCode, string phone, string email, 
            bool isActive, DateTime createdAt)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            Id = id;
            ValidateDomain(name, description, cnpj, address, city, state, zipCode, phone, email);
            
            Name = name;
            Description = description;
            CNPJ = cnpj;
            Address = address;
            City = city;
            State = state;
            ZipCode = zipCode;
            Phone = phone;
            Email = email;
            IsActive = isActive;
            CreatedAt = createdAt;
            
            Companies = new List<Company>();
            LoyaltyCards = new List<LoyaltyCardNetwork>();
        }

        public void Update(string name, string description, string address, string city, 
            string state, string zipCode, string phone, string email)
        {
            ValidateDomain(name, description, CNPJ, address, city, state, zipCode, phone, email);
            
            Name = name;
            Description = description;
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

        private void ValidateDomain(string name, string description, string cnpj, string address, 
            string city, string state, string zipCode, string phone, string email)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Invalid name. Name is required.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description),
                "Invalid description. Description is required.");

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