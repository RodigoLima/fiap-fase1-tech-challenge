using fiap_fase1_tech_challenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fiap_fase1_tech_challenge.Database.Configurations
{
    public class GameLibraryConfiguration : IEntityTypeConfiguration<GameLibrary>
    {
        public void Configure(EntityTypeBuilder<GameLibrary> builder)
        {
            builder.Property(gl => gl.Id).UseIdentityColumn();
            builder.Property(gl => gl.UserId).IsRequired();
            builder.Property(gl => gl.GameId).IsRequired();
            builder.Property(gl => gl.CreatedAt).IsRequired();

            builder.HasOne(gl => gl.User).WithMany(u => u.GameLibraries).HasForeignKey(gl => gl.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(gl => gl.Game).WithMany(g => g.GameLibraries).HasForeignKey(gl => gl.GameId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
