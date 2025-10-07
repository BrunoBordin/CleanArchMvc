using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration
{
    public class LoyaltyCardRedemptionConfiguration : IEntityTypeConfiguration<LoyaltyCardRedemption>
    {
        public void Configure(EntityTypeBuilder<LoyaltyCardRedemption> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CustomerLoyaltyCardId).IsRequired();
            builder.Property(x => x.StoreId).IsRequired();
            builder.Property(x => x.ProductId);
            builder.Property(x => x.DiscountValue).HasPrecision(18, 2);
            builder.Property(x => x.CashbackValue).HasPrecision(18, 2);
            builder.Property(x => x.RewardType).HasConversion<int>().IsRequired();
            builder.Property(x => x.RedeemedAt).IsRequired();
            builder.Property(x => x.BenefitExpirationDate);
            builder.Property(x => x.IsUsed).IsRequired();
            builder.Property(x => x.UsedAt);
            builder.Property(x => x.Notes).HasMaxLength(500);

            // Indexes
            builder.HasIndex(x => x.CustomerLoyaltyCardId);
            builder.HasIndex(x => x.StoreId);
            builder.HasIndex(x => x.RedeemedAt);
            builder.HasIndex(x => x.IsUsed);
            builder.HasIndex(x => x.BenefitExpirationDate);

            // Relationships
            builder.HasOne(x => x.CustomerLoyaltyCard)
                .WithMany(x => x.Redemptions)
                .HasForeignKey(x => x.CustomerLoyaltyCardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Store)
                .WithMany(x => x.Redemptions)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}