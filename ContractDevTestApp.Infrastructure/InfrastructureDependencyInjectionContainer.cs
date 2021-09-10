using ContractDevTestApp.Infrastructure.Interfaces;
using ContractDevTestApp.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContractDevTestApp.Infrastructure
{
	public static class InfrastructureDependencyInjectionContainer
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<IIpStackConfiguration, IpStackConfiguration>(opt=>new IpStackConfiguration()
			{
				AccessKey = configuration["IpStack:AccessKey"],
				Url = configuration["IpStack:Url"]
			});

			services.AddTransient<IIpStackService, IpStackService>();

			return services;
		}
	}
}