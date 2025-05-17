using fiap_fase1_tech_challenge.Configurations;
using fiap_fase1_tech_challenge.Extensions;
using fiap_fase1_tech_challenge.Middlewares;
using fiap_fase1_tech_challenge.Validators;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configura Serilog
SerilogConfiguration.SetupLogging(builder.Configuration);
builder.Host.UseSerilog();

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

builder.Services.AddApplicationServices();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", p => p.RequireRole("Admin"));
});
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddValidators();
builder.Services.ApplyMigrationsAndSeed();

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
