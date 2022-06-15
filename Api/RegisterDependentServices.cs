
using Application;

using Infrastructure;
using Microsoft.OpenApi.Models;
using Persistence;
using Serilog;

namespace Api;

public static class RegisterDependentServices
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        // ******* Access the configuration *******
        var config = builder.Configuration;
        
        // Register your dependencies
        ConfigSerilog();

        // Add services to the container.
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(config);
        builder.Services.AddPersistenceServices(config);
        
        builder.Services.AddHttpContextAccessor();

        //builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
        
        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Open", b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "GloboTicket Ticket Management API",

            });

            //c.OperationFilter<FileResultContentTypeOperationFilter>();
        });
        
        return builder;
    }

    static void ConfigSerilog()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}