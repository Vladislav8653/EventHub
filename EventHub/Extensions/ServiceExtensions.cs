using BusinessLayer.DtoModels;
using BusinessLayer.DtoModels.CategoryDto;
using BusinessLayer.DtoModels.EventsDto;
using BusinessLayer.Infrastructure.Validators;
using BusinessLayer.Infrastructure.Validators.Category;
using BusinessLayer.Infrastructure.Validators.Event;
using BusinessLayer.Services.Contracts;
using BusinessLayer.Services.Implementations;
using DataLayer.Data;
using DataLayer.Repositories.UnitOfWork;
using EventHub.MiddlewareHandlers;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
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
        services.AddTransient<IValidator<CreateEventDto>, CreateEventDtoValidator>();
        services.AddTransient<IValidator<UpdateEventDto>, UpdateEventDtoValidator>();
        
        services.AddTransient<IValidator<CreateCategoryDto>, CreateCategoryDtoValidator>();

        services.AddTransient<IValidator<EntityByIdDto>, EntityByIdDtoValidator>();
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
        
}