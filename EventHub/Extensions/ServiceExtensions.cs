using System.Text;
using BusinessLayer.DtoModels.CategoryDto;
using BusinessLayer.DtoModels.CommonDto;
using BusinessLayer.DtoModels.EventsDto;
using BusinessLayer.DtoModels.EventsDto.QueryParams;
using BusinessLayer.DtoModels.ParticipantDto;
using BusinessLayer.DtoModels.UserDto;
using BusinessLayer.Infrastructure.Authentication;
using BusinessLayer.Logger;
using BusinessLayer.Mapper;
using BusinessLayer.Services.Contracts;
using BusinessLayer.Services.Contracts.Auth;
using BusinessLayer.Services.Implementations;
using DataLayer.Data;
using DataLayer.Repositories.UnitOfWork;
using EventHub.MiddlewareHandlers;
using EventHub.Validation.Category.Attributes;
using EventHub.Validation.Category.Validators;
using EventHub.Validation.CommonValidation;
using EventHub.Validation.Event.Attributes;
using EventHub.Validation.Event.Validators;
using EventHub.Validation.Participants.Attributes;
using EventHub.Validation.Participants.Validators;
using EventHub.Validation.User.Attributes;
using EventHub.Validation.User.Validators;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
//using NLog;


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
        services.AddScoped<IUserService, UserService>();
    }
    
    public static void ConfigureApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        var jwtOptions = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["token"];
                        return Task.CompletedTask;
                    }
                };
            });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
        });
    }

    public static void ConfigureCookiesPolicy(this IApplicationBuilder builder)
    {
        builder.UseCookiePolicy(new CookiePolicyOptions
        {
            MinimumSameSitePolicy = SameSiteMode.Strict,
            HttpOnly = HttpOnlyPolicy.Always,
            Secure = CookieSecurePolicy.Always
        });
    }
    
    public static void ConfigureValidation(this IServiceCollection services)
    {
        services.AddTransient<IValidator<CreateEventDto>, EventDtoValidator>();
        services.AddTransient<IValidator<CategoryDto>, CategoryDtoValidator>();
        services.AddTransient<IValidator<EventFiltersDto>, EventFiltersDtoValidator>();
        services.AddTransient<IValidator<PageParamsDto>, PageParamsDtoValidator>();
        services.AddTransient<IValidator<CreateParticipantDto>, ParticipantDtoValidator>();
        services.AddTransient<IValidator<RegisterUserRequest>, RegisterUserRequestValidator>();
        services.AddTransient<IValidator<LoginUserRequest>, LoginUserRequestValidator>();
        services.AddScoped<ValidateEventDtoAttribute>();
        services.AddScoped<ValidateEventQueryParamsAttribute>();
        services.AddScoped<ValidateParticipantDtoAttribute>();
        services.AddScoped<ValidateCategoryDtoAttribute>();
        services.AddScoped<ValidatePageParamsAttribute>();
        services.AddScoped<ValidateRegisterUserRequestAttribute>();
        services.AddScoped<ValidateLoginUserRequestAttribute>();
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
            cfg.AddProfile<UserMappingProfile>();
        }, AppDomain.CurrentDomain.GetAssemblies());
    }
        
}