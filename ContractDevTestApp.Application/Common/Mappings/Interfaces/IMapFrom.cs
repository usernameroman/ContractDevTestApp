using AutoMapper;

namespace ContractDevTestApp.Application.Common.Mappings.Interfaces
{
	public interface IMapFrom<T>
	{
		void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
	}
}