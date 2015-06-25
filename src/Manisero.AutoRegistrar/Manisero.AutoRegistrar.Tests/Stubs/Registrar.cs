using System;
using System.Collections.Generic;
using System.Reflection;
using Manisero.AutoRegistrar.Commands;
using Manisero.AutoRegistrar.Commands._Impl;
using Manisero.AutoRegistrar.Queries.Tests.Stubs.TestLifetimeLifetime;
using Manisero.AutoRegistrar.Queries._Impl;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario;

namespace Manisero.AutoRegistrar.Tests.Stubs
{
	public class Registrar
	{
		private readonly IBuildRegistrationListCommand<TestLifetime> _buildRegistrationListCommand;

		public Registrar()
		{
			_buildRegistrationListCommand = new BuildRegistrationListCommand<TestLifetime>(new LoadAndRetrieveAvailableTypesCommand(),
																						   new IncludeTypeInTypeMapCommand(),
																						   new IncludeTypeInLifetimeMapCommand<TestLifetime>(new TypeDependenciesQuery(),
																																			 new LongestTestLifetimeQuery(),
																																			 new IsTypeConstructibleQuery(),
																																			 new IsTestLifetimeShorterThanQuery()),
																						   new RegistrationListQuery<TestLifetime>());

		}

		public void Register(Assembly rootAssembly,
							 Func<AssemblyName, bool> referencedAssemblyFilter,
							 Func<Type, bool> typeFilter,
							 IDictionary<Type, Type> typeMap,
							 IDictionary<Type, TestLifetime> lifetimeMap)
		{
			var buildRegistrationListCommandParameter = new BuildRegistrationListCommandParameter<TestLifetime>
				{
					RootAssembly = rootAssembly,
					ReferencedAssemblyFilter = referencedAssemblyFilter,
					TypeFilter = typeFilter,
					TypeMap = typeMap,
					LifetimeMap = lifetimeMap
				};

			var registrations = _buildRegistrationListCommand.Execute(buildRegistrationListCommandParameter);

			// Register
		}
	}
}
