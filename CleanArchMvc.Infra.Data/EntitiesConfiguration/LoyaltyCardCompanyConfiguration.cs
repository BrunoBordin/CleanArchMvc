using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration
{
    public class LoyaltyCardCompanyConfiguration : IEntityTypeConfiguration<LoyaltyCardCompany>
    {
        public void Configure(EntityTypeBuilder<LoyaltyCardCompany> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LoyaltyCardId).IsRequired();
            builder.Property(x => x.CompanyId).IsRequired();
            builder.Property(x => x.AddedAt).IsRequired();

            // Indexes
            builder.HasIndex(x => new { x.LoyaltyCardId, x.CompanyId }).IsUnique();
            builder.HasIndex(x => x.LoyaltyCardId);
            builder.HasIndex(x => x.CompanyId);

            // Relationships
            builder.HasOne(x => x.LoyaltyCard)
                .WithMany(x => x.ParticipatingCompanies)
                .HasForeignKey(x => x.LoyaltyCardId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Company)
                .WithMany(x => x.LoyaltyCards)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}