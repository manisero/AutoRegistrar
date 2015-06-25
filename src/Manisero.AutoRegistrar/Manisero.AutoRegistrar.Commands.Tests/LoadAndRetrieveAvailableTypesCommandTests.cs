using System;
using System.Collections.Generic;
using System.Reflection;
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
		private IList<Type> Execute(Func<AssemblyName, bool> referencedAssemblyFilter, Func<Type, bool> typeFilter)
		{
			// Arrange
			var parameter = new LoadAndRetrieveAvailableTypesCommandParameter
			{
				RootAssembly = GetType().Assembly,
				ReferencedAssemblyFilter = referencedAssemblyFilter,
				TypeFilter = typeFilter
			};

			var command = new LoadAndRetrieveAvailableTypesCommand();

			// Act
			return command.Execute(parameter);
		}

		[Test]
		public void assembly_and_type_filtering_works()
		{
			// Arrange & Act
			var result = Execute(x => x.Name.StartsWith("Manisero.AutoRegistrar"),
								 x => (x.Namespace == null || !x.Namespace.StartsWith(typeof(TestLifetime).Namespace)) && x.Name != "Class2");

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

		[Test]
		public void assembly_filter_null___no_exception()
		{
			// Arrange
			Action act = () => Execute(null, x => true);

			// Assert
			act.ShouldNotThrow();
		}

		[Test]
		public void type_filter_null___no_exception()
		{
			// Arrange
			Action act = () => Execute(x => true, null);

			// Assert
			act.ShouldNotThrow();
		}
	}
}
