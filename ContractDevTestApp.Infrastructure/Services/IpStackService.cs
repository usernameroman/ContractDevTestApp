using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ContractDevTestApp.Infrastructure.Dtos.IpStack;
using ContractDevTestApp.Infrastructure.Helpers;
using ContractDevTestApp.Infrastructure.Interfaces;

namespace ContractDevTestApp.Infrastructure.Services
{
	public class IpStackService : IIpStackService
	{
		private readonly IIpStackConfiguration _configuration;
		private readonly HttpClient _httpClient;

		private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
		{
			PropertyNameCaseInsensitive = true,
			PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
		};

		public IpStackService(IIpStackConfiguration configuration, HttpClient httpClient)
		{
			_configuration = configuration;
			_httpClient = httpClient;
		}

		public async Task<IpStackResponseDto> GetIpInfoAsync(string ip, CancellationToken cancellationToken)
		{
			var response = await _httpClient.GetAsync($"{_configuration.Url}/{ip}?access_key={_configuration.AccessKey}", cancellationToken);
			var result = await response.Content.ReadAsStringAsync(cancellationToken);

			return JsonSerializer.Deserialize<IpStackResponseDto>(result, JsonSerializerOptions);
		}
	}
}