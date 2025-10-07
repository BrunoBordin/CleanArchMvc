using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Document).HasMaxLength(20).IsRequired();
            builder.Property(x => x.BirthDate);
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);

            // Indexes
            builder.HasIndex(x => x.Email);
            builder.HasIndex(x => x.Document);
            builder.HasIndex(x => x.IsActive);
            builder.HasIndex(x => x.CreatedAt);

            // Relationships
            builder.HasMany(x => x.LoyaltyCards)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Redemptions)
                .WithOne(x => x.CustomerLoyaltyCard)
                .HasForeignKey(x => x.CustomerLoyaltyCardId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}