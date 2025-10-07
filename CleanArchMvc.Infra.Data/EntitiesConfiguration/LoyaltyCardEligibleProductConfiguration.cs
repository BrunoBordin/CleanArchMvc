using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration
{
    public class LoyaltyCardEligibleProductConfiguration : IEntityTypeConfiguration<LoyaltyCardEligibleProduct>
    {
        public void Configure(EntityTypeBuilder<LoyaltyCardEligibleProduct> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LoyaltyCardId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.AddedAt).IsRequired();

            // Indexes
            builder.HasIndex(x => new { x.LoyaltyCardId, x.ProductId }).IsUnique();
            builder.HasIndex(x => x.LoyaltyCardId);
            builder.HasIndex(x => x.ProductId);

            // Relationships
            builder.HasOne(x => x.LoyaltyCard)
                .WithMany(x => x.EligibleProducts)
                .HasForeignKey(x => x.LoyaltyCardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}