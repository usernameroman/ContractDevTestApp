using System;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ContractDevTestApp.Application;
using ContractDevTestApp.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ContractDevTestApp
{
	public static class StartupConfigurations
	{
		public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddCors(options =>
			{
				options.AddDefaultPolicy(
					builder =>
					{
						builder.WithOrigins(configuration["CorsPolicies:AllowedOrigins"].Split(","))
							.AllowAnyHeader()
							.AllowAnyMethod();
					});
			});
		}

		public static void ConfigureSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(s =>
			{

				// add JWT Authentication
				var securityScheme = new OpenApiSecurityScheme
				{
					Name = "JWT Authentication",
					Description = "Enter JWT Bearer token **_only_**",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "bearer", // must be lower case
					BearerFormat = "JWT",
					Reference = new OpenApiReference
					{
						Id = JwtBearerDefaults.AuthenticationScheme,
						Type = ReferenceType.SecurityScheme
					}
				};
				s.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);


				s.SwaggerDoc(
					"ContractDevTestApp_API",
					new OpenApiInfo()
					{
						Version = "v1",
						Title = "ContractDevTestApp API v1",
						Description = "REST API designed to serve authentication and application data to ContractDevTestApp."
					});

				s.OperationFilter<AuthResponsesOperationFilter>();

				s.IncludeXmlComments($"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
				s.IncludeXmlComments($"{Assembly.GetAssembly(typeof(ApplicationDependencyInjectionContainer)).GetName().Name}.xml");
			});
		}

		public static void ConfigureJwtAuth(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(cfg =>
			{
				cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;

				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = configuration["JwtSecurityToken:Issuer"],
					ValidAudience = configuration["JwtSecurityToken:Audience"],
					ValidateIssuerSigningKey = true,
					IssuerSigningKey =
						new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityToken:Key"])),
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero
				};

				options.Events = new JwtBearerEvents
				{
					OnAuthenticationFailed = context =>
					{
						if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
						{
							context.Response.Headers.Add("Access-Control-Expose-Headers", "Token-Expired");
							context.Response.Headers.Add("Token-Expired", "true");
						}

						return Task.CompletedTask;
					}
				};
			});
		}

		public static void ConfigureJsonOptions(this IServiceCollection services)
		{
			services.AddMvc().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
				options.JsonSerializerOptions.IgnoreNullValues = true;
			});
		}
	}
}