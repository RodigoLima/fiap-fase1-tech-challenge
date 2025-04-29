using fiap_fase1_tech_challenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fiap_fase1_tech_challenge.Database.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.DiscountPercentage).IsRequired();
            builder.Property(p => p.InitialDate).IsRequired();
            builder.Property(p => p.FinalDate).IsRequired();
            builder.Property(p => p.GameId).IsRequired();
            builder.Property(p => p.CreatedAt).IsRequired();

            builder.HasOne(p => p.Game)
                .WithMany(g => g.Promotions)
                .HasForeignKey(p => p.GameId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
