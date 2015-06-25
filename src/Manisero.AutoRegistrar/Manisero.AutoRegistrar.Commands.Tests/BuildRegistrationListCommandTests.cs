using System;
using System.Collections.Generic;
using System.Linq;
using Manisero.AutoRegistrar.Commands._Impl;
using Manisero.AutoRegistrar.Queries.Tests.Stubs.TestLifetimeLifetime;
using Manisero.AutoRegistrar.Queries._Impl;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase;
using NUnit.Framework;

namespace Manisero.AutoRegistrar.Commands.Tests
{
	public class BuildRegistrationListCommandTests
	{
		[Test]
		public void test_scenario___implementations_and_lifetimes_assigned_properly()
		{
			// Arrange
			var command = new BuildRegistrationListCommand<TestLifetime>(new LoadAndRetrieveAvailableTypesCommand(), new IncludeTypeInTypeMapCommand(), new IncludeTypeInLifetimeMapCommand<TestLifetime>(new TypeDependenciesQuery(), new LongestTestLifetimeQuery(), new IsTypeConstructibleQuery(), new IsTestLifetimeShorterThanQuery()));

			var parameter = new BuildRegistrationListCommandParameter<TestLifetime>
				{
					RootAssembly = typeof(GlobalState).Assembly,
					ReferencedAssemblyFilter = x => false,
					TypeFilter = x => x.Namespace != null && x.Namespace.StartsWith(typeof(GlobalState).Namespace),
					TypeMap = new Dictionary<Type, Type>(),
					LifetimeMap = Configuration.INITIAL_LIFETIME_MAP.ToDictionary(x => x.Key, x => x.Value)
				};

			// Act
			var result = command.Execute(parameter);

			// Assert
		}
	}
}
