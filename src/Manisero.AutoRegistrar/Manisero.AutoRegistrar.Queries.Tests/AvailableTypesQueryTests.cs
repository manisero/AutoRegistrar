using Manisero.AutoRegistrar.Queries._Impl;
using FluentAssertions;
using NUnit.Framework;

namespace Manisero.AutoRegistrar.Queries.Tests
{
	public class AvailableTypesQueryTests
	{
		[Test]
		public void returns_some_known_types()
		{
			// Arrange
			var queryParameter = new AvailableTypesQueryParameter
				{
					RootAssembly = GetType().Assembly,
					ReferencedAssemblyFilter = x => x.Name.StartsWith("Manisero.AutoRegistrar")
				};

			var query = new AvailableTypesQuery();

			// Act
			var result = query.Execute(queryParameter);

			// Assert
			result.Should().Contain(typeof(AvailableTypesQueryTests));
			result.Should().Contain(typeof(AvailableTypesQuery));
		}
	}
}
