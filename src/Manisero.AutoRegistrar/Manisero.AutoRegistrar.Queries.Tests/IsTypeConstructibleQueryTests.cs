using System;
using Manisero.AutoRegistrar.Queries._Impl;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.ConstructorHelpers;
using NUnit.Framework;
using FluentAssertions;

namespace Manisero.AutoRegistrar.Queries.Tests
{
	public class IsTypeConstructibleQueryTests
	{
		private bool Execute(Type type)
		{
			// Arrange
			var isTypeConstructibleQuery = new IsTypeConstructibleQuery();

			// Act
			return isTypeConstructibleQuery.Execute(type);
		}

		[Test]
		public void DefaultConstructor_true()
		{
			// Arrange & Act
			var result = Execute(typeof(DefaultConstructor));

			// Assert
			result.Should().BeTrue();
		}
	}
}
