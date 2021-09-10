using System.Threading;
using System.Threading.Tasks;
using ContractDevTestApp.Infrastructure.Dtos;
using ContractDevTestApp.Infrastructure.Dtos.IpStack;

namespace ContractDevTestApp.Infrastructure.Interfaces
{
	public interface IIpStackService
	{
		public Task<IpStackResponseDto> GetIpInfoAsync(string ip, CancellationToken cancellationToken);
	}
}