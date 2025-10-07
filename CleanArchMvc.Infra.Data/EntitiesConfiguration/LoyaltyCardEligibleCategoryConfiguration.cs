using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration
{
    public class LoyaltyCardEligibleCategoryConfiguration : IEntityTypeConfiguration<LoyaltyCardEligibleCategory>
    {
        public void Configure(EntityTypeBuilder<LoyaltyCardEligibleCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LoyaltyCardId).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.AddedAt).IsRequired();

            // Indexes
            builder.HasIndex(x => new { x.LoyaltyCardId, x.CategoryId }).IsUnique();
            builder.HasIndex(x => x.LoyaltyCardId);
            builder.HasIndex(x => x.CategoryId);

            // Relationships
            builder.HasOne(x => x.LoyaltyCard)
                .WithMany(x => x.EligibleCategories)
                .HasForeignKey(x => x.LoyaltyCardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}