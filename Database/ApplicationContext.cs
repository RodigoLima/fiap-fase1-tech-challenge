using fiap_fase1_tech_challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace fiap_fase1_tech_challenge.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameLibrary> GameLibraries { get; set; }
        public DbSet<Promotion> Promotions { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            var userRoleId = new Guid("7f3fec04-b8f5-4a9a-b8d7-24e15abb6494");
            var adminRoleId = new Guid("abbf75f9-53d8-4c80-9d3a-40cc1dd117ab");

            //roles seed
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = userRoleId, Name = "user"},
                new Role { Id = adminRoleId, Name = "admin"}
            );

            var adminId = new Guid("c4fa95b4-1d43-4a79-a63e-5c64856cbdf6");

            //admin user seed
            modelBuilder.Entity<User>().HasData(
                new User {Id = adminId, Name = "admin", Email = "admin@mail.com", Password = "Ad123p@ssword", RoleId = adminRoleId }
            );
        }
    }
}
