using System;
using System.Collections.Generic;
using FluentAssertions;
using Manisero.AutoRegistrar.Core;
using Manisero.AutoRegistrar.Queries._Impl;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.DependencyHelpers;
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
		public void assigns_lifetimes_to_implementations()
		{
			// Arrange & Act
			var typeMap = new Dictionary<Type, Type>
				{
					{ typeof(IInterface), typeof(Implementation) }
				};

			var lifetimeMap = new Dictionary<Type, int>
				{
					{ typeof(Implementation), 3 }
				};

			var result = Execute(typeMap, lifetimeMap);

			// Assert
			result.Should().Contain(x => x.SourceType == typeof(IInterface) && x.DestinationType == typeof(Implementation) && x.Lifetime == 3);
		}
	}
}
