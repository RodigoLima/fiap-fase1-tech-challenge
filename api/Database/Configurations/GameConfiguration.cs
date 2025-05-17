using fiap_fase1_tech_challenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fiap_fase1_tech_challenge.Database.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.Property(g => g.Id).UseIdentityColumn();
            builder.Property(g => g.Name).IsRequired().HasMaxLength(50);
            builder.Property(g => g.Description).HasMaxLength(200);
            builder.Property(g => g.Price).IsRequired();
            builder.Property(g => g.Genre).HasMaxLength(20).IsRequired();
            builder.Property(g => g.CreatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasMany(g => g.GameLibraries)
                .WithOne(gl => gl.Game)
                .HasForeignKey(gl => gl.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(g => g.Promotions)
                .WithOne(p => p.Game)
                .HasForeignKey(p => p.GameId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
