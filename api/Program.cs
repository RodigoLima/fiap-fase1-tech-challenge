using fiap_fase1_tech_challenge.Common;
using fiap_fase1_tech_challenge.Common.Configurations;
using fiap_fase1_tech_challenge.Database;
using fiap_fase1_tech_challenge.Middlewares;
using fiap_fase1_tech_challenge.Modules.Authentication;
using fiap_fase1_tech_challenge.Modules.Games;
using fiap_fase1_tech_challenge.Modules.GamesLibrary;
using fiap_fase1_tech_challenge.Modules.Promotions;
using fiap_fase1_tech_challenge.Modules.Users;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configura Serilog
SerilogConfiguration.SetupLogging(builder.Configuration);
builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

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

builder.Services.AddDatabaseServices();
builder.Services.AddCommonServices();
builder.Services.AddAuthenticationModule(builder.Configuration);
builder.Services.AddUsersModule();
builder.Services.AddGamesModule();
builder.Services.AddPromotionsModule();
builder.Services.AddGamesLibraryModule();

var app = builder.Build();

// Middleware global de erro/log
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

try
{
    Log.Information("Iniciando aplicação");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Erro fatal ao iniciar o sistema");
}
finally
{
    Log.CloseAndFlush();
}
