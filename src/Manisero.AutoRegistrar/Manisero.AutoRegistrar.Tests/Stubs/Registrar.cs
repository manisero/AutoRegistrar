using System;
using System.Collections.Generic;
using System.Reflection;
using Manisero.AutoRegistrar.Commands;
using Manisero.AutoRegistrar.Commands._Impl;
using Manisero.AutoRegistrar.Queries.Tests.Stubs.TestLifetimeLifetime;
using Manisero.AutoRegistrar.Queries._Impl;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario;
using System.Linq;

namespace Manisero.AutoRegistrar.Tests.Stubs
{
	public class Registrar
	{
		public class Registration<TLifetime>
		{
			public Type SourceType { get; set; }

			public Type DestinationType { get; set; }

			public TLifetime Lifetime { get; set; }
		}

		public void Register(Assembly rootAssembly,
							 Func<AssemblyName, bool> referencedAssemblyFilter,
							 Func<Type, bool> typeFilter,
							 IDictionary<Type, Type> typeMap,
							 IDictionary<Type, TestLifetime> lifetimeMap)
		{
			// Get available types
			var loadAndRetrieveAvailableTypesCommand = new LoadAndRetrieveAvailableTypesCommand();
			var availableTypes = loadAndRetrieveAvailableTypesCommand.Execute(new LoadAndRetrieveAvailableTypesCommandParameter
				{
					RootAssembly = rootAssembly,
					ReferencedAssemblyFilter = referencedAssemblyFilter,
					TypeFilter = typeFilter
				});

			// Build type map
			var includeTypeInTypeMapCommand = new IncludeTypeInTypeMapCommand();

			foreach (var type in availableTypes)
			{
				includeTypeInTypeMapCommand.Execute(new IncludeTypeInTypeMapCommandParameter
					{
						TypeMap = typeMap,
						Type = type,
						AvailableTypes = availableTypes
					});
			}

			// Include implementations from Type Map in Lifetime Map
			var includeTypeInLifetimeMapCommand = new IncludeTypeInLifetimeMapCommand<TestLifetime>(new TypeDependenciesQuery(),
																									new LongestTestLifetimeQuery(),
																									new IsTypeConstructibleQuery(),
																									new IsTestLifetimeShorterThanQuery());

			foreach (var implementationType in typeMap.Values)
			{
				if (!lifetimeMap.ContainsKey(implementationType))
				{
					includeTypeInLifetimeMapCommand.Execute(new IncludeTypeInLifetimeMapCommandParameter<TestLifetime>
						{
							LifetimeMap = lifetimeMap,
							Type = implementationType,
							TypeMap = typeMap
						});
				}
			}

			// Create registration map/list
			var registrarions = typeMap.Select(x => new Registration<TestLifetime>
				{
					SourceType = x.Key,
					DestinationType = x.Value,
					Lifetime = lifetimeMap[x.Value]
				})
									   .ToList();

			// Register
		}
	}
}
