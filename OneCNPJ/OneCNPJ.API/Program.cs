using Microsoft.EntityFrameworkCore;
using OneCNPJ.Data;
using OneCNPJ.Infrastructure.Repository;
using OneCNPJ.Infrastructure.Repository.Interfaces;
using OneCNPJ.Services.Interfaces;
using OneCNPJ.Services.Interfaces.Import;
using OneCNPJ.Services.Persistences;
using OneCNPJ.Services.Service.Import;
using OneCNPJ.Services.Services;
using OneCNPJ.Services.Services.GetMany;
using OneCNPJ.Services.Services.Import;
using Serilog;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        // Registra encodings legacy, incluindo 1252
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();



        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        // Configura��o do Serilog
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .Enrich.FromLogContext()
            .CreateLogger();

        // Configura��o do ApplicationDbContext com PostgreSQL
        builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("OneCNPJ.Data")
            ));
        builder.Services.AddHttpClient();

        builder.Services.AddScoped<ICnpjImportacaoRepository, CnpjImportacaoRepository>();

        builder.Services.AddScoped<ICnpjImportacaoGetOneService, CnpjImportacaoGetOneService>();
        builder.Services.AddScoped<ICnpjImportacaoGetManyService, CnpjImportacaoGetManyService>();
        builder.Services.AddScoped<ICnpjImportacaoPersistService, CnpjImportacaoPersistService>();

        builder.Services.AddScoped<ICnpjImportService, CnpjImportService>();
        builder.Services.AddScoped<ICnpjEmpresasImportStep, CnpjEmpresasImportStep>();
        builder.Services.AddScoped<ICnpjEstabelecimentosImportStep, CnpjEstabelecimentosImportStep>();
        builder.Services.AddScoped<ICnpjSociosImportStep, CnpjSociosImportStep>();
        builder.Services.AddScoped<ICnpjSimplesImportStep, CnpjSimplesImportStep>();
        builder.Services.AddScoped<ICnpjSatelitesImportStep, CnpjSatelitesImportStep>();


        builder.Host.UseSerilog();

        builder.Services.AddHealthChecks();

        builder.Services.AddControllers();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.MapHealthChecks("/health");

        app.MapControllers();
        app.Run();
    }
}