using System;
using Manisero.AutoRegistrar.Queries._Impl;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.ConstructorHelpers;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.DependencyHelpers;
using NUnit.Framework;
using FluentAssertions;

namespace Manisero.AutoRegistrar.Queries.Tests
{
	public class IsTypeConstructibleQueryTests
	{
		[Test]
		[TestCase(typeof(DefaultConstructor))]
		[TestCase(typeof(SingleConstructor_NoDependencies))]
		[TestCase(typeof(SingleConstructor_DefaultConstructor))]
		[TestCase(typeof(SingleConstructor_IntDefaultConstructorSingleConstructorInt))]
		[TestCase(typeof(MutlipleConstructors_Int))]
		[TestCase(typeof(MutlipleConstructors_IntStringBool))]
		[TestCase(typeof(MutlipleConstructors_SameNumberOfParameters))]
		[TestCase(typeof(PrivateAndPublicConstructor))]
		public void constructible_type___true(Type constructibleType)
		{
			// Arrange & Act
			var result = new IsTypeConstructibleQuery().Execute(constructibleType);

			// Assert
			result.Should().BeTrue();
		}

		[Test]
		[TestCase(typeof(IInterface))]
		[TestCase(typeof(Abstract))]
		[TestCase(typeof(PrivateConstructor))]
		public void inconstructible_type___false(Type constructibleType)
		{
			// Arrange & Act
			var result = new IsTypeConstructibleQuery().Execute(constructibleType);

			// Assert
			result.Should().BeFalse();
		}
	}
}
