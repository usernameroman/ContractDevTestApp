using ContractDevTestApp.Infrastructure.Interfaces;

namespace ContractDevTestApp.Infrastructure.Services
{
	public class IpStackConfiguration: IIpStackConfiguration
	{
		public string Url { get; set; }
		public string AccessKey { get; set; }
	}
}