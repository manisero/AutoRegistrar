using System;
using System.Collections.Generic;
using FluentAssertions;
using Manisero.AutoRegistrar.Core;
using Manisero.AutoRegistrar.Queries._Impl;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase.Queries;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase.Queries._Impl;
using NUnit.Framework;

namespace Manisero.AutoRegistrar.Queries.Tests
{
	public class RegistrationListQueryTests
	{
		private IList<Registration<int>> Execute(IDictionary<Type, Type> typeMap, IDictionary<Type, int> lifetimeMap)
		{
			// Arrange
			var parameter = new RegistrationListQueryParameter<int>
				{
					TypeMap = typeMap,
					LifetimeMap = lifetimeMap
				};

			var query = new RegistrationListQuery<int>();

			// Act
			return query.Execute(parameter);
		}
		
		[Test]
		public void assigns_lifetimes_to_implementations_and_ignores_lifetimes_of_types_not_present_in_type_map()
		{
			// Arrange & Act
			var typeMap = new Dictionary<Type, Type>
				{
					{ typeof(IDataQuery), typeof(DataQuery) },
					{ typeof(IPureQuery), typeof(PureQuery) },
				};

			var lifetimeMap = new Dictionary<Type, int>
				{
					{ typeof(DataQuery), 3 },
					{ typeof(PureQuery), 5 },
					{ typeof(GlobalState), 7 },
				};

			var result = Execute(typeMap, lifetimeMap);

			// Assert
			result.Should().HaveCount(2);
			result.Should().Contain(x => x.SourceType == typeof(IDataQuery) && x.DestinationType == typeof(DataQuery) && x.Lifetime == 3);
			result.Should().Contain(x => x.SourceType == typeof(IPureQuery) && x.DestinationType == typeof(PureQuery) && x.Lifetime == 5);
		}

		[Test]
		public void implementation_not_present_in_lifetime_map___Exception()
		{
			// Arrange
			var typeMap = new Dictionary<Type, Type>
				{
					{ typeof(IDataQuery), typeof(DataQuery) }
				};

			var lifetimeMap = new Dictionary<Type, int>();

			Action act = () => Execute(typeMap, lifetimeMap);

			// Assert
			act.ShouldThrow<Exception>();
		}
	}
}
