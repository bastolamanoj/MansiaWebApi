using DataProvider.DatabaseContext;
using DataProvider.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MansiaWebApi.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            return services;
        }
        public static IServiceCollection AddDbConfigAndIdentity(this IServiceCollection services,IConfiguration config)
        {
            //starting  by registering dbcontext here
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection String is not found..")),
                ServiceLifetime.Scoped
            );

            //Identity Code
           services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddRoles<Role>();
            return services;
        }
        public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            //Jwt Code
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["Jwt:Issuer"], //issuer is a entity which is reposible for issuing a token.
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                }

            );
            return services;

        }
        public static IServiceCollection AddSwaggerGen(this IServiceCollection services)
        {
            // Add authenticaion to swagger UI  
           services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;

        }


    }
}
