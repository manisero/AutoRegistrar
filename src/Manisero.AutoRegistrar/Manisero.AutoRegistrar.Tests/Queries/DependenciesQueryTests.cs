using System;
using System.Collections.Generic;
using FluentAssertions;
using Manisero.AutoRegistrar.Queries;
using Manisero.AutoRegistrar.Queries._Impl;
using NUnit.Framework;

namespace Manisero.AutoRegistrar.Tests.Queries
{
	public class DependenciesQueryTests
	{
		private class SingleConstructor_SingleDependency
		{
			public SingleConstructor_SingleDependency(int dependency) { }
		}

		private class MutlipleConstructors_SingleDependency
		{
			public MutlipleConstructors_SingleDependency() { }
			public MutlipleConstructors_SingleDependency(int dependency) { }
		}

		private class MutlipleConstructors_MultipleDependencies
		{
			public MutlipleConstructors_MultipleDependencies() { }
			public MutlipleConstructors_MultipleDependencies(int dependency) { }
			public MutlipleConstructors_MultipleDependencies(int dependency1, string dependncy2, bool dependency3) { }
			public MutlipleConstructors_MultipleDependencies(int dependency1, string dependncy2) { }
		}

		private class MutlipleConstructors_SameNumberOfParameters
		{
			public MutlipleConstructors_SameNumberOfParameters(int dependency) { }
			public MutlipleConstructors_SameNumberOfParameters(string dependency) { }
		}

		private IReadOnlyList<Type> Execute(Type type)
		{
			// Arrange
			var parameter = new DependenciesQueryParameter
			{
				Type = type
			};

			var query = new DependenciesQuery();

			// Act
			return query.Execute(parameter);
		}

		[Test]
		public void single_constructor_one_dependency___dependency_from_the_only_constructor()
		{
			// Arrange & Act
			var result = Execute(typeof(SingleConstructor_SingleDependency));

			// Assert
			result.Should().HaveCount(1);
			result[0].ShouldBeEquivalentTo(typeof(int));
		}

		[Test]
		public void mutliple_constructors_single_dependency___dependency_from_constructor_with_parameter()
		{
			// Arrange & Act
			var result = Execute(typeof(MutlipleConstructors_SingleDependency));

			// Assert
			result.Should().HaveCount(1);
			result[0].ShouldBeEquivalentTo(typeof(int));
		}

		[Test]
		public void mutliple_constructors_multiple_dependencies___dependency_from_constructor_with_most_parameters()
		{
			// Arrange & Act
			var result = Execute(typeof(MutlipleConstructors_MultipleDependencies));

			// Assert
			result.Should().HaveCount(3);
			result[0].ShouldBeEquivalentTo(typeof(int));
			result[1].ShouldBeEquivalentTo(typeof(string));
			result[2].ShouldBeEquivalentTo(typeof(bool));
		}

		[Test]
		public void mutliple_constructors_same_number_of_parameters___NotSupportedException()
		{
			// Arrange
			Action act = (() => Execute(typeof(MutlipleConstructors_SameNumberOfParameters)));

			// Assert
			act.ShouldThrow<InvalidOperationException>();
		}
	}
}
