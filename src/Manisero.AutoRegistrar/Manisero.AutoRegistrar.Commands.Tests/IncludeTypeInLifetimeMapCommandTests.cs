using System;
using System.Collections.Generic;
using Manisero.AutoRegistrar.Commands._Impl;
using Manisero.AutoRegistrar.Queries.Tests.Stubs;
using Manisero.AutoRegistrar.Queries.Tests.Stubs.IntLifetme;
using Manisero.AutoRegistrar.Queries._Impl;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.ConstructorHelpers;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.DependencyHelpers;
using NUnit.Framework;
using FluentAssertions;

namespace Manisero.AutoRegistrar.Commands.Tests
{
	public class IncludeTypeInLifetimeMapCommandTests
	{
		private readonly TypeDependenciesQuery _typeDependenciesQuery = new TypeDependenciesQuery();
		private readonly LongestIntLifetimeQuery _longestIntLifetimeQuery = new LongestIntLifetimeQuery();
		private readonly IsIntLifetimeShorterThanQuery _isIntLifetimeShorterThanQuery = new IsIntLifetimeShorterThanQuery();
		private readonly int _longestLifetime;

		public IncludeTypeInLifetimeMapCommandTests()
		{
			_longestLifetime = _longestIntLifetimeQuery.Execute();
		}

		private void Execute(Dictionary<Type, int> lifetimeMap, Type type, IDictionary<Type, Type> typeMap = null)
		{
			// Arrange
			var command = new IncludeTypeInLifetimeMapCommand<int>(_typeDependenciesQuery,
																   _longestIntLifetimeQuery,
																   _isIntLifetimeShorterThanQuery);

			// Act
			command.Execute(new IncludeTypeInLifetimeMapCommandParameter<int>
				{
					LifetimeMap = lifetimeMap,
					Type = type,
					TypeMap = typeMap ?? new Dictionary<Type, Type>()
				});
		}

		[Test]
		public void type_already_in_map___InvalidOperationException()
		{
			// Arrange
			var lifetimeMap = new Dictionary<Type, int>
				{
					{ typeof(DefaultConstructor), 3 }
				};

			Action act = () => Execute(lifetimeMap, typeof(DefaultConstructor));

			// Assert
			act.ShouldThrow<InvalidOperationException>();
		}

		[Test]
		public void no_dependencies___longest_lifetime()
		{
			// Arrange & Act
			var lifetimeMap = new Dictionary<Type, int>();
			Execute(lifetimeMap, typeof(DefaultConstructor));

			// Assert
			lifetimeMap.Should().HaveCount(1);
			lifetimeMap.Should().Contain(typeof(DefaultConstructor), _longestLifetime);
		}

		[Test]
		public void single_dependency_present_in_map___copy_its_lifetime()
		{
			// Arrange & Act
			var existingLifetime = 3;
			var lifetimeMap = new Dictionary<Type, int>
				{
					{ typeof(int), existingLifetime }
				};

			Execute(lifetimeMap, typeof(SingleConstructor_Int));

			// Assert
			lifetimeMap.Should().HaveCount(2);
			lifetimeMap.Should().Contain(typeof(SingleConstructor_Int), existingLifetime);
		}

		[Test]
		public void multiple_dependencies_present_in_map___copy_lowest_lifetime()
		{
			// Arrange & Act
			var lifetimeMap = new Dictionary<Type, int>
				{
					{ typeof(int), 7 },
					{ typeof(string), 3 },
					{ typeof(bool), 5 }
				};

			Execute(lifetimeMap, typeof(SingleConstructor_IntStringBool));

			// Assert
			lifetimeMap.Should().HaveCount(4);
			lifetimeMap.Should().Contain(typeof(SingleConstructor_IntStringBool), 3);
		}

		[Test]
		public void single_implementation_dependency_not_present_in_map_but_present_in_type_map___include_it_and_copy_its_lifetime()
		{
			// Arrange & Act
			var lifetimeMap = new Dictionary<Type, int>();
			var typeMap = new Dictionary<Type, Type> { { typeof(IInterface), typeof(Implementation) } };
			Execute(lifetimeMap, typeof(ImplementationDependant), typeMap);

			// Assert
			lifetimeMap.Should().HaveCount(2);
			lifetimeMap.Should().Contain(typeof(Implementation), _longestLifetime);
			lifetimeMap.Should().Contain(typeof(ImplementationDependant), _longestLifetime);
		}

		[Test]
		public void single_interface_dependency_not_present_in_map_but_present_in_type_map___include_implementation_and_copy_its_lifetime()
		{
			// Arrange & Act
			var lifetimeMap = new Dictionary<Type, int>();
			var typeMap = new Dictionary<Type, Type> { { typeof(IInterface), typeof(Implementation) } };
			Execute(lifetimeMap, typeof(InterfaceDependant), typeMap);

			// Assert
			lifetimeMap.Should().HaveCount(3);
			lifetimeMap.Should().Contain(typeof(Implementation), _longestLifetime);
			lifetimeMap.Should().Contain(typeof(IInterface), _longestLifetime);
			lifetimeMap.Should().Contain(typeof(InterfaceDependant), _longestLifetime);
		}

		[Test]
		public void single_dependency_not_present_in_map_nor_type_map___InvalidOperationException()
		{
			// Arrange & Act
			var lifetimeMap = new Dictionary<Type, int>();
			Action act = () => Execute(lifetimeMap, typeof(ImplementationDependant));

			// Assert
			act.ShouldThrow<InvalidOperationException>();
		}

		[Test]
		public void tree_partially_present_in_map_with_rest_in_type_map___include_missig_and_copy_lowest_lifetime()
		{
			// Arrange & Act
			var lifetimeMap = new Dictionary<Type, int>
				{
					{ typeof(int), 3 }
				};

			var typeMap = new Dictionary<Type, Type>
				{
					{ typeof(DefaultConstructor), typeof(DefaultConstructor) },
					{ typeof(SingleConstructor_Int), typeof(SingleConstructor_Int) }
				};

			Execute(lifetimeMap, typeof(SingleConstructor_IntDefaultConstructorSingleConstructorInt), typeMap);

			// Assert
			lifetimeMap.Should().HaveCount(4);
			lifetimeMap.Should().Contain(typeof(int), 3);
			lifetimeMap.Should().Contain(typeof(DefaultConstructor), _longestLifetime);
			lifetimeMap.Should().Contain(typeof(SingleConstructor_Int), 3);
			lifetimeMap.Should().Contain(typeof(SingleConstructor_IntDefaultConstructorSingleConstructorInt), 3);
		}

		[Test]
		public void tree_partially_present_in_map_and_type_map___InvalidOperationException()
		{
			// Arrange
			var lifetimeMap = new Dictionary<Type, int>
				{
					{ typeof(int), 3 }
				};

			var typeMap = new Dictionary<Type, Type>
				{
					{ typeof(DefaultConstructor), typeof(DefaultConstructor) }
				};

			Action act = () => Execute(lifetimeMap, typeof(SingleConstructor_IntDefaultConstructorSingleConstructorInt), typeMap);

			// Assert
			act.ShouldThrow<InvalidOperationException>();
		}
	}
}
