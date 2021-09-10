using System.Reflection;
using ContractDevTestApp.Application.Common.Behaviours;
using ContractDevTestApp.Application.Common.Services;
using ContractDevTestApp.Application.Common.Services.Interfaces;
using ContractDevTestApp.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContractDevTestApp.Application
{
	public static class ApplicationDependencyInjectionContainer
	{
		public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddMediatR(Assembly.GetExecutingAssembly());

			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationsBehaviour<,>));
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(IpFetchBehaviour<,>));

			services.AddTransient(typeof(IAuthorizationService), typeof(AuthorizationService));

			services.AddInfrastructure(configuration);
			
			return services;
		}
	}
}