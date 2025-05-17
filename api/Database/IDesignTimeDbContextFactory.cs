using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using fiap_fase1_tech_challenge.Database;

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
  public ApplicationContext CreateDbContext(string[] args)
  {
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.Development.json", optional: true)
        .Build();

    var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
    optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

    return new ApplicationContext(optionsBuilder.Options);
  }
}
