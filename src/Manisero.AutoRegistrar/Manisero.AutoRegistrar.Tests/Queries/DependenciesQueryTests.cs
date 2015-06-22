using Manisero.AutoRegistrar.Queries;
using Manisero.AutoRegistrar.Queries._Impl;
using FluentAssertions;
using NUnit.Framework;

namespace Manisero.AutoRegistrar.Tests.Queries
{
	public class DependenciesQueryTests
	{
		private class SingleConstructor_SingleParamter
		{
			public SingleConstructor_SingleParamter(int dependency) { }
		}

		private class MutlipleConstructors_NoParametersAndSingleParamter
		{
			public MutlipleConstructors_NoParametersAndSingleParamter() { }
			public MutlipleConstructors_NoParametersAndSingleParamter(int dependency) { }
		}

		[Test]
		public void single_constructor_single_parameter___single_parameter_as_dependency()
		{
			// Arrange
			var parameter = new DependenciesQueryParameter
				{
					Type = typeof(SingleConstructor_SingleParamter)
				};

			var query = new DependenciesQuery();

			// Act
			var result = query.Execute(parameter);

			// Assert
			result.Should().ContainSingle(x => x == typeof(int));
		}
	}
}
