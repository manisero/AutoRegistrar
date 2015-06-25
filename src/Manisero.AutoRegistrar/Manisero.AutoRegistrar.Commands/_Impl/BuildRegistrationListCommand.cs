using System.Collections.Generic;
using Manisero.AutoRegistrar.Core;
using System.Linq;

namespace Manisero.AutoRegistrar.Commands._Impl
{
	public class BuildRegistrationListCommand<TLifetime> : IBuildRegistrationListCommand<TLifetime>
	{
		private readonly ILoadAndRetrieveAvailableTypesCommand _loadAndRetrieveAvailableTypesCommand;
		private readonly IIncludeTypeInTypeMapCommand _includeTypeInTypeMapCommand;
		private readonly IIncludeTypeInLifetimeMapCommand<TLifetime> _includeTypeInLifetimeMapCommand;

		public BuildRegistrationListCommand(ILoadAndRetrieveAvailableTypesCommand loadAndRetrieveAvailableTypesCommand,
											IIncludeTypeInTypeMapCommand includeTypeInTypeMapCommand,
											IIncludeTypeInLifetimeMapCommand<TLifetime> includeTypeInLifetimeMapCommand)
		{
			_loadAndRetrieveAvailableTypesCommand = loadAndRetrieveAvailableTypesCommand;
			_includeTypeInTypeMapCommand = includeTypeInTypeMapCommand;
			_includeTypeInLifetimeMapCommand = includeTypeInLifetimeMapCommand;
		}

		public IList<Registration<TLifetime>> Execute(BuildRegistrationListCommandParameter<TLifetime> parameter)
		{
			var availableTypes = _loadAndRetrieveAvailableTypesCommand.Execute(new LoadAndRetrieveAvailableTypesCommandParameter
				{
					RootAssembly = parameter.RootAssembly,
					ReferencedAssemblyFilter = parameter.ReferencedAssemblyFilter,
					TypeFilter = parameter.TypeFilter
				});

			foreach (var type in availableTypes)
			{
				_includeTypeInTypeMapCommand.Execute(new IncludeTypeInTypeMapCommandParameter
					{
						TypeMap = parameter.TypeMap,
						Type = type,
						AvailableTypes = availableTypes
					});
			}

			foreach (var implementationType in parameter.TypeMap.Values)
			{
				if (!parameter.LifetimeMap.ContainsKey(implementationType))
				{
					_includeTypeInLifetimeMapCommand.Execute(new IncludeTypeInLifetimeMapCommandParameter<TLifetime>
						{
							LifetimeMap = parameter.LifetimeMap,
							Type = implementationType,
							TypeMap = parameter.TypeMap
						});
				}
			}

			return parameter.TypeMap.Select(x => new Registration<TLifetime>
				{
					SourceType = x.Key,
					DestinationType = x.Value,
					Lifetime = parameter.LifetimeMap[x.Value]
				})
							.ToList();
		}
	}
}
