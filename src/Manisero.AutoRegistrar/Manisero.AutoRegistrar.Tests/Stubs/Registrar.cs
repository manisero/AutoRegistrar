using System;
using System.Collections.Generic;
using Manisero.AutoRegistrar.Commands;
using Manisero.AutoRegistrar.Commands._Impl;
using Manisero.AutoRegistrar.Queries.Tests.Stubs;
using Manisero.AutoRegistrar.Queries.Tests.Stubs.IntLifetme;
using Manisero.AutoRegistrar.Queries._Impl;

namespace Manisero.AutoRegistrar.Tests.Stubs
{
	public class Registrar
	{
		public void Register()
		{
			// Get available types
			var loadAndRetrieveAvailableTypesCommand = new LoadAndRetrieveAvailableTypesCommand();
			var availableTypes = loadAndRetrieveAvailableTypesCommand.Execute(new LoadAndRetrieveAvailableTypesCommandParameter
				{
					RootAssembly = GetType().Assembly,
					ReferencedAssemblyFilter = x => x.FullName.StartsWith("Manisero.AutoRegistrar")
				});

			// Get initial type map
			var typeMap = new Dictionary<Type, Type>();

			// Build type map
			var includeTypeInTypeMapCommand = new IncludeTypeInTypeMapCommand();

			foreach (var availableType in availableTypes)
			{
				includeTypeInTypeMapCommand.Execute(new IncludeTypeInTypeMapCommandParameter
					{
						TypeMap = typeMap,
						Type = availableType,
						AvailableTypes = availableTypes
					});
			}

			// Get initial lifetime map
			var lifetimeMap = new Dictionary<Type, int>();

			// Include concrete types from type map in lifetime map
			var includeTypeInLifetimeMapCommand = new IncludeTypeInLifetimeMapCommand<int>(new TypeDependenciesQuery(),
																						   new LongestIntLifetimeQuery(),
																						   new IsIntLifetimeShorterThanQuery());

			foreach (var destinationType in typeMap.Values)
			{
				if (!lifetimeMap.ContainsKey(destinationType))
				{
					includeTypeInLifetimeMapCommand.Execute(new IncludeTypeInLifetimeMapCommandParameter<int>
						{
							LifetimeMap = lifetimeMap,
							Type = destinationType,
							TypeMap = typeMap
						});
				}
			}

			// Create registration map/list

			// Register
		}
	}
}
