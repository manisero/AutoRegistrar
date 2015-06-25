using System;
using System.Collections.Generic;
using Manisero.AutoRegistrar.Commands;
using Manisero.AutoRegistrar.Commands._Impl;
using Manisero.AutoRegistrar.Queries.Tests.Stubs.TestLifetimeLifetime;
using Manisero.AutoRegistrar.Queries._Impl;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase;

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
					ReferencedAssemblyFilter = x => x.FullName == typeof(GlobalState).Assembly.FullName,
					TypeFilter = x => x.Namespace != null && x.Namespace.StartsWith(typeof(GlobalState).Namespace)
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
			var lifetimeMap = new Dictionary<Type, TestLifetime>();

			// Include concrete types from type map in lifetime map
			var includeTypeInLifetimeMapCommand = new IncludeTypeInLifetimeMapCommand<TestLifetime>(new TypeDependenciesQuery(),
																									new LongestTestLifetimeQuery(),
																									new IsTestLifetimeShorterThanQuery());

			foreach (var destinationType in typeMap.Values)
			{
				if (!lifetimeMap.ContainsKey(destinationType))
				{
					includeTypeInLifetimeMapCommand.Execute(new IncludeTypeInLifetimeMapCommandParameter<TestLifetime>
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
