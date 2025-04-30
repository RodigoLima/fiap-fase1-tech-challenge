using fiap_fase1_tech_challenge.Database;
using fiap_fase1_tech_challenge.Extensions;
using fiap_fase1_tech_challenge.Services.Interfaces;
using fiap_fase1_tech_challenge.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwt();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseNpgsql(connectionString);

    //if (builder.Environment.IsDevelopment())
    //{
    //    options.EnableSensitiveDataLogging()
    //           .LogTo(Console.WriteLine);
    //}
});

#region Services
builder.Services.AddScoped<IGameService, GameService>();
#endregion


builder.Services.AddApplicationServices();
builder.Services.AddAuthorizationPolicies();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.ApplyMigrationsAndSeed();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
