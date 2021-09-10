using System;
using ContractDevTestApp.Domain.Entities.Base;
using ContractDevTestApp.Domain.Interfaces;

namespace ContractDevTestApp.Domain.Entities
{
	public class UserIpInfo : BaseEntity, IHasCreatedAt
	{
		public Guid UserId { get; set; }

		public DateTimeOffset CreatedAt { get; set; }

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

		public User User { get; set; }
	}
}