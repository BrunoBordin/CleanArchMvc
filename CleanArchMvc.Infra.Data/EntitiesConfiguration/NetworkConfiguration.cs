using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration
{
    public class NetworkConfiguration : IEntityTypeConfiguration<Network>
    {
        public void Configure(EntityTypeBuilder<Network> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.CNPJ).HasMaxLength(18).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(500).IsRequired();
            builder.Property(x => x.City).HasMaxLength(100).IsRequired();
            builder.Property(x => x.State).HasMaxLength(2).IsRequired();
            builder.Property(x => x.ZipCode).HasMaxLength(10).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);

            // Indexes
            builder.HasIndex(x => x.CNPJ).IsUnique();
            builder.HasIndex(x => x.Email);
            builder.HasIndex(x => x.IsActive);
            builder.HasIndex(x => x.CreatedAt);

            // Relationships
            builder.HasMany(x => x.Companies)
                .WithOne(x => x.Network)
                .HasForeignKey(x => x.NetworkId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.LoyaltyCards)
                .WithOne(x => x.Network)
                .HasForeignKey(x => x.NetworkId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}