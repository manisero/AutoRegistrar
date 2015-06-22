using System;
using System.Collections.Generic;
using Manisero.AutoRegistrar.Commands._Impl;
using Manisero.AutoRegistrar.Queries.LongestLifetime.LongestLifetimeQueries;
using Manisero.AutoRegistrar.Queries._Impl;
using Manisero.AutoRegistrar.Tests.TestsHelpers;
using NUnit.Framework;
using FluentAssertions;

namespace Manisero.AutoRegistrar.Commands.Tests
{
	public class IncludeTypeInLifetimeMapCommandTests
	{
		private readonly TypeDependenciesQuery _typeDependenciesQuery = new TypeDependenciesQuery();
		private readonly LongestIntLifetimeQuery _longestIntLifetimeQuery = new LongestIntLifetimeQuery();

		private void Execute(Dictionary<Type, int> lifetimeMap, Type type)
		{
			// Arrange
			var command = new IncludeTypeInLifetimeMapCommand<int>(_typeDependenciesQuery, _longestIntLifetimeQuery);

			// Act
			command.Execute(new IncludeTypeInLifetimeMapCommandParameter<int>
				{
					LifetimeMap = lifetimeMap,
					Type = type
				});
		}

		[Test]
		public void type_already_in_map___InvalidOperationException()
		{
			// Arrange & Act
			var lifetimeMap = new Dictionary<Type, int>
				{
					{ typeof(DefaultConstructor), 0 }
				};

			Action act = () => Execute(lifetimeMap, typeof(DefaultConstructor));

			// Assert
			act.ShouldThrow<InvalidOperationException>();
		}

		[Test]
		public void no_dependencies___longest_lifetime()
		{
			// Arrange & Act
			var lifetimeMap = new Dictionary<Type, int>();
			Execute(lifetimeMap, typeof(DefaultConstructor));

			// Assert
			lifetimeMap.Should().HaveCount(1);
		}
	}
}
