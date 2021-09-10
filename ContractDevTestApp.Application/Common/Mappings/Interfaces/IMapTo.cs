using AutoMapper;

namespace ContractDevTestApp.Application.Common.Mappings.Interfaces
{
	public interface IMapTo<T>
	{
		void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
	}
}