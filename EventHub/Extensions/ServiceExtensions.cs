using BusinessLayer.DtoModels.CategoryDto;
using BusinessLayer.DtoModels.EventsDto;
using BusinessLayer.Services.Contracts;
using BusinessLayer.Services.Implementations;
using DataLayer.Data;
using DataLayer.Repositories.UnitOfWork;
using EventHub.MiddlewareHandlers;
using EventHub.Validators;
using EventHub.Validators.Category;
using EventHub.Validators.Event;
using EventHub.Validators.Filters;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Microsoft.OpenApi.Models;
using NLog;


namespace EventHub.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });
    }

    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddScoped<ILoggerManager, LoggerManager>();
    }

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EventHubDbContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("sqlConnection"), b => 
                b.MigrationsAssembly("DataLayer")));
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoriesManager, RepositoriesManager>();
    
    public static void ConfigureEventService(this IServiceCollection services) =>
        services.AddScoped<IEventService, EventService>();
    
    public static void ConfigureCategoryService(this IServiceCollection services) =>
        services.AddScoped<ICategoryService, CategoryService>();

    public static void ConfigureValidation(this IServiceCollection services)
    {
        services.AddTransient<IValidator<CreateEventDto>, EventDtoValidator>();
        services.AddTransient<IValidator<CategoryDto>, CategoryDtoValidator>();
        services.AddTransient<IValidator<EventFiltersDto>, EventFiltersDtoValidator>();
        services.AddScoped<ValidateEventDtoAttribute>();
        services.AddScoped<ValidateEventFiltersDtoAttribute>();
        //services.AddTransient<IValidator<EntityByIdDto>, EntityByIdDtoValidator>();
    }

    public static void ConfigureLogger(this IServiceCollection services)
    {
        var currentDirection = Directory.GetCurrentDirectory();
        var parent = Directory.GetParent(currentDirection);
        var relativePath = Path.Combine(nameof(BusinessLayer), "nlog.config");
        var nlogConfigPath = Path.Combine(parent.FullName, relativePath); 
        LogManager.Setup().LoadConfigurationFromFile(nlogConfigPath);
    }
    
    public static void AppendMiddlewareErrorHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<GlobalErrorHandler>();
    }

    public static void UseSwaggerUi(this IApplicationBuilder builder)
    {
        builder.UseSwaggerUI(s =>
        {
            s.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Hub");
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Event Hub",
                Version = "v1"
            }));
    } 
        
}