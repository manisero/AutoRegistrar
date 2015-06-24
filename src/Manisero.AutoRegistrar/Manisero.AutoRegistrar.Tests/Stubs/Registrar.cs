using System;
using System.Collections.Generic;
using Manisero.AutoRegistrar.Commands;
using Manisero.AutoRegistrar.Commands._Impl;
using Manisero.AutoRegistrar.Queries.Tests.Stubs;
using Manisero.AutoRegistrar.Queries._Impl;

namespace Manisero.AutoRegistrar.Tests.Stubs
{
	public class Registrar
	{
		public void Register()
		{
			// 1. Get available types
			var loadAndRetrieveAvailableTypesCommand = new LoadAndRetrieveAvailableTypesCommand();
			loadAndRetrieveAvailableTypesCommand.Execute(new LoadAndRetrieveAvailableTypesCommandParameter
				{
					RootAssembly = GetType().Assembly,
					ReferencedAssemblyFilter = x => x.FullName.StartsWith("Manisero.AutoRegistrar")
				});

			// 2. Build type map
			var typeMap = new Dictionary<Type, Type>();

			// 3. Get initial lifetime map
			var lifetimeMap = new Dictionary<Type, int>();

			// 4. Include concrete types from type map in lifetime map
			var includeTypeInLifetimeMapCommand = new IncludeTypeInLifetimeMapCommand<int>(new TypeDependenciesQuery(),
																						   new LongestIntLifetimeQuery(),
																						   new IsIntLifetimeShorterThanQuery());

			foreach (var destinationType in typeMap.Values)
			{
				includeTypeInLifetimeMapCommand.Execute(new IncludeTypeInLifetimeMapCommandParameter<int>
					{
						LifetimeMap = lifetimeMap,
						Type = destinationType
					});
			}

			// 5. Create registration map/list

			// 6. Register
		}
	}
}
