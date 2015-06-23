using FluentAssertions;
using Manisero.AutoRegistrar.Commands._Impl;
using NUnit.Framework;
using System.Linq;

namespace Manisero.AutoRegistrar.Commands.Tests
{
	public class LoadAndRetrieveAvailableTypesCommandTests
	{
		[Test]
		public void returns_some_known_types()
		{
			// Arrange
			var parameter = new LoadAndRetrieveAvailableTypesCommandParameter
				{
					RootAssembly = GetType().Assembly,
					ReferencedAssemblyFilter = x => x.Name.StartsWith("Manisero.AutoRegistrar")
				};

			var command = new LoadAndRetrieveAvailableTypesCommand();

			// Act
			var result = command.Execute(parameter);

			// Assert
			result.Should().Contain(typeof(LoadAndRetrieveAvailableTypesCommandTests));
			result.Should().Contain(typeof(LoadAndRetrieveAvailableTypesCommand));
			result.Should().Contain(x => x.Assembly.FullName.StartsWith("Manisero.AutoRegistrar.Core") &&
										 x.Name == "StringExtensions");
		}
	}
}
