using System.Text;
using FluentValidation;
using KuSys.Contracts.MappingConfigs;
using KuSys.Core.Middlewares;
using KuSys.Core.Types;
using KuSys.DataAccess;
using KuSys.DataAccess.Repositories.Course;
using KuSys.DataAccess.Repositories.Student;
using KuSys.DataAccess.Repositories.StudentCourses;
using KuSys.DataAccess.Repositories.User;
using KuSys.Entities;
using KuSys.Services;
using KuSys.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace KuSys.Extensions;

/// <summary>
/// KuSys Configuration extensions.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Use Global exception handler
    /// </summary>
    /// <param name="app"></param>
    public static void UseExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionHandler>();
    }

    /// <summary>
    /// Add required services for KuSys System.
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="config">Configuration manager</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddKuSysServices(this IServiceCollection serviceCollection, ConfigurationManager config)
    {
        // Get jwt settings from appSettings.json and bind it into object
        JwtSettings jwtSettings = new JwtSettings();
        config.GetSection("JwtSettings").Bind(jwtSettings);
        serviceCollection.AddValidatorsFromAssemblyContaining(typeof(EntityBase<>));
        TypeAdapterConfig.GlobalSettings.Scan(typeof(StudentMapConfigs).Assembly); // This will scan the assembly containing MappingConfig and register the configurations
        serviceCollection
                // Add swagger support
            .AddSwaggerSupport()
                // Register repositories and services
            .RegisterServices()
                // Configure AspNet.Identity and JWT Auth.
            .ConfigureIdentity(jwtSettings)
                // Add and configure DatabaseContext
            .AddKuSysDbContext(config.GetConnectionString("LocalConnection"));
        
        return serviceCollection;
    }
    
     private static IServiceCollection AddSwaggerSupport(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(c =>
        {
            const string apiXml = "../../../../KuSys.Api/KuSys.Api.xml";
            const string coreXml = "../../../../KuSys.Core/KuSys.Core.xml";
            const string entitiesXml = "../../../../KuSys.Entities/KuSys.Entities.xml";
            const string dataAccessXml = "../../../../KuSys.DataAccess/KuSys.DataAccess.xml";
            const string extensionsXml = "../../../../KuSys.Extensions/KuSys.Extensions.xml";
            const string servicesXml = "../../../../KuSys.Services/KuSys.Services.xml";
    
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, apiXml));
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, coreXml));
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, entitiesXml));
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, dataAccessXml));
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, extensionsXml));
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, servicesXml));
    
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "KuSys Api",
                Version = "v1",
                Description = "KuSys Stunder/Course Tracking System",
                Contact = new OpenApiContact()
                {
                    Name = "Serhat Kıyanç",
                    Email = "serhat.dev@outlook.com"
                }
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat= "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Token"
            });
    
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
        return serviceCollection;
    }

     private static IServiceCollection AddKuSysDbContext(this IServiceCollection serviceCollection,string connectionString)
    {
        serviceCollection.AddDbContext<KuSysDbContext>(o =>
        {
            o.UseNpgsql(connectionString);
        });
        return serviceCollection;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICourseRepository, CourseRepository>();
        serviceCollection.AddScoped<ICourseService, CourseService>();

        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IUserService,UserService>();

        serviceCollection.AddScoped<IStudentRepository, StudentRepository>();
        serviceCollection.AddScoped<IStudentService, StudentService>();

        serviceCollection.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
        serviceCollection.AddScoped<IStudentCourseService, StudentCourseService>();
        return serviceCollection;
    }

    private static IServiceCollection ConfigureIdentity(this IServiceCollection serviceCollection,JwtSettings jwtSettings)
    {
        // Add identity system with provided values, Add EF as store.
        serviceCollection.AddIdentity<User, UserRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
            })
            .AddRoles<UserRole>()
            .AddEntityFrameworkStores<KuSysDbContext>()
            .AddDefaultTokenProviders();
        
        // Add and configure JWT base Authentication.
        serviceCollection.AddAuthentication(o =>
            {
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });
        return serviceCollection;
    }
}