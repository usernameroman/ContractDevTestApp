using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using ContractDevTestApp.Application.Common.Mappings.Interfaces;

namespace ContractDevTestApp.Application.Common.Mappings
{
	public class CommonMappingProfile : Profile
	{
		public CommonMappingProfile()
		{
			ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
		}

		private void ApplyMappingsFromAssembly(Assembly assembly)
		{
			CreateGenericMapping(assembly, typeof(IMapFrom<>));
			CreateGenericMapping(assembly, typeof(IMapTo<>));
		}

		private void CreateGenericMapping(Assembly assembly, Type mappingType)
		{
			var types = assembly.GetTypes()
				.Where(t => t.GetInterfaces().Any(i =>
					i.IsGenericType && i.GetGenericTypeDefinition() == mappingType))
				.ToList();

			foreach (var type in types)
			{
				var instance = Activator.CreateInstance(type);

				var implementedMapping = type.GetMethod("Mapping");

				if (implementedMapping != null)
				{
					implementedMapping.Invoke(instance, new object[] { this });
				}
				else
				{
					// Selects all mappings methods, if class implements multiple IMapFrom or IMapTo interfaces
					var methodsInfo = type.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == mappingType).Select(x => x.GetMethod("Mapping"));

					foreach (var methodInfo in methodsInfo)
					{
						methodInfo?.Invoke(instance, new object[] { this });
					}
				}
			}
		}
	}
}