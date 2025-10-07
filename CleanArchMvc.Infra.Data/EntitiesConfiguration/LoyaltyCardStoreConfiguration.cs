using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration
{
    public class LoyaltyCardStoreConfiguration : IEntityTypeConfiguration<LoyaltyCardStore>
    {
        public void Configure(EntityTypeBuilder<LoyaltyCardStore> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LoyaltyCardId).IsRequired();
            builder.Property(x => x.StoreId).IsRequired();
            builder.Property(x => x.AddedAt).IsRequired();

            // Indexes
            builder.HasIndex(x => new { x.LoyaltyCardId, x.StoreId }).IsUnique();
            builder.HasIndex(x => x.LoyaltyCardId);
            builder.HasIndex(x => x.StoreId);

            // Relationships
            builder.HasOne(x => x.LoyaltyCard)
                .WithMany(x => x.ParticipatingStores)
                .HasForeignKey(x => x.LoyaltyCardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Store)
                .WithMany(x => x.LoyaltyCards)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}