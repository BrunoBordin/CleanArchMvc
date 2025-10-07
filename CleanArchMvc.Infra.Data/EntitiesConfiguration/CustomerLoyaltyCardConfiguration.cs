using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration
{
    public class CustomerLoyaltyCardConfiguration : IEntityTypeConfiguration<CustomerLoyaltyCard>
    {
        public void Configure(EntityTypeBuilder<CustomerLoyaltyCard> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CustomerId).IsRequired();
            builder.Property(x => x.LoyaltyCardId).IsRequired();
            builder.Property(x => x.StoreId).IsRequired();
            builder.Property(x => x.CurrentStamps).IsRequired();
            builder.Property(x => x.FirstStampDate);
            builder.Property(x => x.LastStampDate);
            builder.Property(x => x.ExpirationDate);
            builder.Property(x => x.IsCompleted).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);

            // Indexes
            builder.HasIndex(x => new { x.CustomerId, x.LoyaltyCardId, x.StoreId }).IsUnique();
            builder.HasIndex(x => x.CustomerId);
            builder.HasIndex(x => x.LoyaltyCardId);
            builder.HasIndex(x => x.StoreId);
            builder.HasIndex(x => x.IsCompleted);
            builder.HasIndex(x => x.ExpirationDate);

            // Relationships
            builder.HasOne(x => x.Customer)
                .WithMany(x => x.LoyaltyCards)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.LoyaltyCard)
                .WithMany(x => x.CustomerCards)
                .HasForeignKey(x => x.LoyaltyCardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Store)
                .WithMany(x => x.CustomerCards)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Redemptions)
                .WithOne(x => x.CustomerLoyaltyCard)
                .HasForeignKey(x => x.CustomerLoyaltyCardId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}