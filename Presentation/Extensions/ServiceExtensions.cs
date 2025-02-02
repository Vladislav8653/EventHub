﻿using System.Text;
using Application.Contracts;
using Application.Contracts.AuthContracts;
using Application.Contracts.ImageServiceContracts;
using Application.Contracts.UseCaseContracts.CategoryUseCaseContracts;
using Application.Contracts.UseCaseContracts.EventUseCaseContracts;
using Application.Contracts.UseCaseContracts.ParticipantUseCaseContracts;
using Application.Contracts.UseCaseContracts.UserUseCaseContracts;
using Application.DtoModels.CategoryDto;
using Application.DtoModels.CommonDto;
using Application.DtoModels.EventsDto;
using Application.DtoModels.EventsDto.QueryParams;
using Application.DtoModels.ParticipantDto;
using Application.DtoModels.UserDto;
using Application.MappingProfiles;
using Application.UseCases.CategoryUseCases;
using Application.UseCases.EventUseCases;
using Application.UseCases.ParticipantUseCases;
using Application.UseCases.UserUseCases;
using Infrastructure.Validation.Category;
using Infrastructure.Validation.CommonValidation;
using Infrastructure.Validation.Event;
using Infrastructure.Validation.Participants;
using Domain;
using Domain.RepositoryContracts;
using FluentValidation;
using Infrastructure;
using Infrastructure.Authentication;
using Infrastructure.CookieService;
using Infrastructure.ImageService;
using Infrastructure.RepositoryImplementations;
using Infrastructure.Validation.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.Middlewares;

namespace Presentation.Extensions;

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

  
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EventHubDbContext>(opts =>
            opts.UseNpgsql(configuration.GetConnectionString("sqlConnection"), b => 
                b.MigrationsAssembly("Infrastructure")));
    }

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IParticipantRepository, ParticipantRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoriesManager, RepositoriesManager>();
    
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
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["access-token"];
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
        services.AddTransient<IValidator<EventQueryParamsDto>, EventQueryParamsDtoValidator>();
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

    public static void ConfigureImageService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ImageStorageSettings>(configuration.GetSection("ImageStorage"));
        services.AddSingleton<IImageService, ImageService>();
    }
    
    public static void ConfigureCookieService(this IServiceCollection services)
    {
        services.AddScoped<ICookieService, CookieService>();
    }
    
    
    public static void ConfigureUseCases(this IServiceCollection services)
    {
        ConfigureCategoryUseCases(services);
        ConfigureEventUseCases(services);
        ConfigureParticipantUseCases(services);
        ConfigureUserUseCases(services);
    }
    
    private static void ConfigureCategoryUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateCategoryUseCase, CreateCategoryUseCase>();
        services.AddScoped<IDeleteCategoryUseCase, DeleteCategoryUseCase>();
        services.AddScoped<IGetAllCategoriesUseCase, GetAllCategoriesUseCase>();
        services.AddScoped<IGetCategoryByIdUseCase, GetCategoryByIdUseCase>();
        services.AddScoped<IGetCategoryByNameUseCase, GetCategoryByNameUseCase>();
    }

    private static void ConfigureParticipantUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateParticipantUseCase, CreateParticipantUseCase>();
        services.AddScoped<IDeleteParticipantUseCase, DeleteParticipantUseCase>();
        services.AddScoped<IGetAllParticipantsUseCase, GetAllParticipantsUseCase>();
        services.AddScoped<IGetParticipantUseCase, GetParticipantUseCase>();
    }

    private static void ConfigureEventUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateEventUseCase, CreateEventUseCase>();
        services.AddScoped<IDeleteEventUseCase, DeleteEventUseCase>();
        services.AddScoped<IGetAllEventsUseCase, GetAllEventsUseCase>();
        services.AddScoped<IGetEventByIdUseCase, GetEventByIdUseCase>();
        services.AddScoped<IGetEventByNameUseCase, GetEventByNameUseCase>();
        services.AddScoped<IUpdateEventUseCase, UpdateEventUseCase>();
        services.AddScoped<IGetAllUserEventsUseCase, GetAllUserEventsUseCase>();
    }

    private static void ConfigureUserUseCases(this IServiceCollection services)
    {
        services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
        services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
    
}