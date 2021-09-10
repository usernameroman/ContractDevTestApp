using System;
using ContractDevTestApp.Application.Common.Mappings.Interfaces;
using ContractDevTestApp.Domain.Entities;
using ContractDevTestApp.Infrastructure.Dtos.IpStack;

namespace ContractDevTestApp.Application.Common.Models.Persistent
{
	public class UserInfoDto : IMapFrom<IpStackResponseDto>, IMapTo<UserIpInfo>, IMapFrom<UserIpInfo>
	{
		public string Ip { get; set; }
		public string Hostname { get; set; }
		public string Type { get; set; }
		public string ContinentCode { get; set; }
		public string ContinentName { get; set; }
		public string CountryCode { get; set; }
		public string CountryName { get; set; }
		public string RegionCode { get; set; }
		public string RegionName { get; set; }
		public string City { get; set; }
		public string Zip { get; set; }

		public double? Latitude { get; set; }
		public double? Longitude { get; set; }

		public DateTimeOffset CreatedAt { get; set; }
	}
}