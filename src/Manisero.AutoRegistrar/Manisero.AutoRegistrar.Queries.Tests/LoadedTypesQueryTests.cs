using Manisero.AutoRegistrar.Queries._Impl;
using FluentAssertions;
using NUnit.Framework;

namespace Manisero.AutoRegistrar.Queries.Tests
{
	public class LoadedTypesQueryTests
	{
		[Test]
		public void returns_some_known_types()
		{
			// Arrange
			var query = new LoadedTypesQuery();

			// Act
			var result = query.Execute();

			// Assert
			result.Should().Contain(typeof(LoadedTypesQueryTests));
		}
	}
}
