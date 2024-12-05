using BusinessLayer.DtoModels.CategoryDto;
using BusinessLayer.DtoModels.CommonDto;
using BusinessLayer.DtoModels.EventsDto;
using BusinessLayer.DtoModels.EventsDto.QueryParams;
using BusinessLayer.DtoModels.ParticipantDto;
using BusinessLayer.Logger;
using BusinessLayer.Mapper;
using BusinessLayer.Services.Contracts;
using BusinessLayer.Services.Implementations;
using DataLayer.Data;
using DataLayer.Repositories.UnitOfWork;
using EventHub.MiddlewareHandlers;
using EventHub.Validation.CommonValidation;
using EventHub.Validation.Event.Attributes;
using EventHub.Validation.Event.Validators;
using EventHub.Validation.Participants.Validators;
using EventHub.Validators.Category.Attributes;
using EventHub.Validators.Participants.Attributes;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Microsoft.OpenApi.Models;
using NLog;

using CategoryDtoValidator = EventHub.Validation.Category.Validators.CategoryDtoValidator;


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

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IParticipantService, ParticipantService>();
    }
    
    public static void ConfigureValidation(this IServiceCollection services)
    {
        services.AddTransient<IValidator<CreateEventDto>, EventDtoValidator>();
        services.AddTransient<IValidator<CategoryDto>, CategoryDtoValidator>();
        services.AddTransient<IValidator<EventFiltersDto>, EventFiltersDtoValidator>();
        services.AddTransient<IValidator<PageParamsDto>, PageParamsDtoValidator>();
        services.AddTransient<IValidator<CreateParticipantDto>, ParticipantDtoValidator>();
        services.AddScoped<ValidateEventDtoAttribute>();
        services.AddScoped<ValidateEventQueryParamsAttribute>();
        services.AddScoped<ValidateParticipantDtoAttribute>();
        services.AddScoped<ValidateCategoryDtoAttribute>();
        services.AddScoped<ValidatePageParamsAttribute>();
    }

    public static void ConfigureLogger(this IServiceCollection services)
    {
        /*var currentDirection = Directory.GetCurrentDirectory();
        var parent = Directory.GetParent(currentDirection);
        var nlogConfigPath = Path.Combine(parent.FullName, nameof(BusinessLayer), "Logger", "nlog.config"); 
        LogManager.Setup().LoadConfigurationFromFile(nlogConfigPath);*/
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

    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<EventMappingProfile>();
            cfg.AddProfile<CategoryMappingProfile>();
            cfg.AddProfile<ParticipantMappingProfile>();
        }, AppDomain.CurrentDomain.GetAssemblies());
    }
        
}