using Microsoft.EntityFrameworkCore;
using OneCNPJ.Data;
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
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("OneCNPJ.Data")
            ));

        builder.Host.UseSerilog();

        builder.Services.AddHealthChecks();

        builder.Services.AddControllers();

        //builder.Services.AddScoped<ICnpjRepository, CnpjRepository>();
        //builder.Services.AddScoped<IConteudoRepository, ConteudoRepository>();
        //builder.Services.AddScoped<IFalhaRepository, FalhaRepository>();
        //builder.Services.AddScoped<IIgnoradoRepository, IgnoradoRepository>();
        //builder.Services.AddScoped<IRegistroClasseRepository, RegistroClasseRepository>();
        //builder.Services.AddScoped<IRegistroFundoRepository, RegistroFundoRepository>();
        //builder.Services.AddScoped<IRegistroSubclasseRepository, RegistroSubclasseRepository>();
        //builder.Services.AddScoped<ILayoutRepository, LayoutRepository>();
        //builder.Services.AddScoped<ILayoutCampoRepository, LayoutCampoRepository>();

        //builder.Services.AddScoped<ICnpjGetOneService, CnpjGetOneService>();
        //builder.Services.AddScoped<ICnpjImportService, CnpjImportService>();
        //builder.Services.AddScoped<IConteudoImportService, ConteudoImportService>();
        //builder.Services.AddScoped<IRegistroCnpjImportService, RegistroCnpjImportService>();

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