﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using Manisero.AutoRegistrar.Queries._Impl;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.ConstructorHelpers;
using NUnit.Framework;

namespace Manisero.AutoRegistrar.Queries.Tests
{
	public class TypeDependenciesQueryTests
	{
		private IReadOnlyList<Type> Execute(Type type)
		{
			// Arrange
			var query = new TypeDependenciesQuery();

			// Act
			return query.Execute(type);
		}

		[Test]
		public void default_constructor___no_dependencies()
		{
			// Arrange & Act
			var result = Execute(typeof(DefaultConstructor));

			// Assert
			result.Should().HaveCount(0);
		}

		[Test]
		public void single_constructor_no_dependencies___no_dependencies()
		{
			// Arrange & Act
			var result = Execute(typeof(SingleConstructor_NoDependencies));

			// Assert
			result.Should().HaveCount(0);
		}

		[Test]
		public void single_constructor_one_dependency___dependency_from_the_only_constructor()
		{
			// Arrange & Act
			var result = Execute(typeof(SingleConstructor_Int));

			// Assert
			result.Should().HaveCount(1);
			result[0].ShouldBeEquivalentTo(typeof(int));
		}

		[Test]
		public void mutliple_constructors_single_dependency___dependency_from_constructor_with_parameter()
		{
			// Arrange & Act
			var result = Execute(typeof(MutlipleConstructors_Int));

			// Assert
			result.Should().HaveCount(1);
			result[0].ShouldBeEquivalentTo(typeof(int));
		}

		[Test]
		public void mutliple_constructors_multiple_dependencies___dependency_from_constructor_with_most_parameters()
		{
			// Arrange & Act
			var result = Execute(typeof(MutlipleConstructors_IntStringBool));

			// Assert
			result.Should().HaveCount(3);
			result[0].ShouldBeEquivalentTo(typeof(int));
			result[1].ShouldBeEquivalentTo(typeof(string));
			result[2].ShouldBeEquivalentTo(typeof(bool));
		}

		[Test]
		public void mutliple_constructors_same_number_of_parameters___exception()
		{
			// Arrange
			Action act = (() => Execute(typeof(MutlipleConstructors_SameNumberOfParameters)));

			// Assert
			act.ShouldThrow<InvalidOperationException>();
		}
	}
}
