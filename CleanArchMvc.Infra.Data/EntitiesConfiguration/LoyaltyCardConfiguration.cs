using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration
{
    public class LoyaltyCardConfiguration : IEntityTypeConfiguration<LoyaltyCard>
    {
        public void Configure(EntityTypeBuilder<LoyaltyCard> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PublicName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Status).HasConversion<int>().IsRequired();
            builder.Property(x => x.StampGoal).IsRequired();
            builder.Property(x => x.ValuePerStamp).HasPrecision(18, 2).IsRequired();
            builder.Property(x => x.EligibilityType).HasConversion<int>().IsRequired();
            builder.Property(x => x.AllowMultipleStampsPerPurchase).IsRequired();
            builder.Property(x => x.Scope).HasConversion<int>().IsRequired();
            builder.Property(x => x.RewardType).HasConversion<int>().IsRequired();
            builder.Property(x => x.DiscountValue).HasPrecision(18, 2);
            builder.Property(x => x.CashbackValue).HasPrecision(18, 2);
            builder.Property(x => x.CardValidityDays);
            builder.Property(x => x.BenefitValidityDays);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);

            // Indexes
            builder.HasIndex(x => x.Status);
            builder.HasIndex(x => x.Scope);
            builder.HasIndex(x => x.CreatedAt);

            // Relationships
            builder.HasMany(x => x.ParticipatingNetworks)
                .WithOne(x => x.LoyaltyCard)
                .HasForeignKey(x => x.LoyaltyCardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ParticipatingCompanies)
                .WithOne(x => x.LoyaltyCard)
                .HasForeignKey(x => x.LoyaltyCardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.EligibleProducts)
                .WithOne(x => x.LoyaltyCard)
                .HasForeignKey(x => x.LoyaltyCardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.EligibleCategories)
                .WithOne(x => x.LoyaltyCard)
                .HasForeignKey(x => x.LoyaltyCardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.RewardProducts)
                .WithOne(x => x.LoyaltyCard)
                .HasForeignKey(x => x.LoyaltyCardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.CustomerCards)
                .WithOne(x => x.LoyaltyCard)
                .HasForeignKey(x => x.LoyaltyCardId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}