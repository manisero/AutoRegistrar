using FluentAssertions;
using Manisero.AutoRegistrar.Commands._Impl;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase.Commands._Impl;
using NUnit.Framework;

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
					ReferencedAssemblyFilter = x => x.Name.StartsWith("Manisero.AutoRegistrar"),
					TypeFilter = x => (x.Namespace == null || !x.Namespace.StartsWith(typeof(TestLifetime).Namespace)) && x.Name != "Class2"
				};

			var command = new LoadAndRetrieveAvailableTypesCommand();

			// Act
			var result = command.Execute(parameter);

			// Assert
			result.Should().Contain(typeof(LoadAndRetrieveAvailableTypesCommandTests));
			result.Should().Contain(typeof(LoadAndRetrieveAvailableTypesCommand));
			result.Should().Contain(x => x.Assembly.FullName.StartsWith("Manisero.AutoRegistrar.Tests.ReferencedByTestsCoreOnly") &&
										 x.Name == "Class1");

			result.Should().NotContain(typeof(TestLifetime));
			result.Should().NotContain(typeof(GlobalState));
			result.Should().NotContain(typeof(GlobalStateCommand));
			result.Should().NotContain(x => x.Assembly.FullName.StartsWith("Manisero.AutoRegistrar.Tests.ReferencedByTestsCoreOnly") &&
											x.Name == "Class2");
		}
	}
}
