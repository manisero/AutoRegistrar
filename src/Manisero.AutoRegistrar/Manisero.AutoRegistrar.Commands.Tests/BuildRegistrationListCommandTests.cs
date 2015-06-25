using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Manisero.AutoRegistrar.Commands._Impl;
using Manisero.AutoRegistrar.Queries.Tests.Stubs.TestLifetimeLifetime;
using Manisero.AutoRegistrar.Queries._Impl;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase.Commands;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase.Commands._Impl;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase.Queries;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase.Queries._Impl;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase._Impl;
using NUnit.Framework;

namespace Manisero.AutoRegistrar.Commands.Tests
{
	public class BuildRegistrationListCommandTests
	{
		[Test]
		public void test_scenario___implementations_and_lifetimes_assigned_properly()
		{
			// Arrange
			var command = new BuildRegistrationListCommand<TestLifetime>(new LoadAndRetrieveAvailableTypesCommand(),
																		 new IncludeTypeInTypeMapCommand(),
																		 new IncludeTypeInLifetimeMapCommand<TestLifetime>(new TypeDependenciesQuery(),
																														   new LongestTestLifetimeQuery(),
																														   new IsTypeConstructibleQuery(),
																														   new IsTestLifetimeShorterThanQuery()),
																		 new RegistrationListQuery<TestLifetime>());

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
			result.Should().HaveCount(8);

			result.Should().Contain(x => x.SourceType == typeof(IDataContext) && x.DestinationType == typeof(DataContext) && x.Lifetime == TestLifetime.Request);
			result.Should().Contain(x => x.SourceType == typeof(IRandomBehavior) && x.DestinationType == typeof(RandomBehavior) && x.Lifetime == TestLifetime.Transient);

			result.Should().Contain(x => x.SourceType == typeof(IDataQuery) && x.DestinationType == typeof(DataQuery) && x.Lifetime == TestLifetime.Request);
			result.Should().Contain(x => x.SourceType == typeof(IPureQuery) && x.DestinationType == typeof(PureQuery) && x.Lifetime == TestLifetime.Sigleton);
			result.Should().Contain(x => x.SourceType == typeof(IRandomQuery) && x.DestinationType == typeof(RandomQuery) && x.Lifetime == TestLifetime.Transient);

			result.Should().Contain(x => x.SourceType == typeof(IDataCommand) && x.DestinationType == typeof(DataCommand) && x.Lifetime == TestLifetime.Request);
			result.Should().Contain(x => x.SourceType == typeof(IGlobalStateCommand) && x.DestinationType == typeof(GlobalStateCommand) && x.Lifetime == TestLifetime.Sigleton);
			result.Should().Contain(x => x.SourceType == typeof(IRandomDataCommand) && x.DestinationType == typeof(RandomDataCommand) && x.Lifetime == TestLifetime.Transient);
		}
	}
}
