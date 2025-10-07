using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        
        // Loyalty Card System
        public DbSet<LoyaltyCard> LoyaltyCards { get; set; }
        public DbSet<Network> Networks { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<LoyaltyCardNetwork> LoyaltyCardNetworks { get; set; }
        public DbSet<LoyaltyCardCompany> LoyaltyCardCompanies { get; set; }
        public DbSet<LoyaltyCardEligibleProduct> LoyaltyCardEligibleProducts { get; set; }
        public DbSet<LoyaltyCardEligibleCategory> LoyaltyCardEligibleCategories { get; set; }
        public DbSet<LoyaltyCardRewardProduct> LoyaltyCardRewardProducts { get; set; }
        public DbSet<CustomerLoyaltyCard> CustomerLoyaltyCards { get; set; }
        public DbSet<LoyaltyCardRedemption> LoyaltyCardRedemptions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //ApplyConfigurationsFromAssembly usado para buscar todas as entidades na entity configurations que irão ser criadas tabelas
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}