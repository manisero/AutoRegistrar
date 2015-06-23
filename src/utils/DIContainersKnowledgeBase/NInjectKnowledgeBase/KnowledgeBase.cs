using System;
using FluentAssertions;
using NUnit.Framework;
using Ninject;
using Shared.InterfaceImplementation;

namespace NInjectKnowledgeBase
{
    public class KnowledgeBase
    {
		[Test]
		public void default_lifetime_is_per_resolve()
		{
			// Arrange
			var kernel = new StandardKernel();
			kernel.Bind<IInterface>().To<Implementation>();

			// Act
			var instance1 = kernel.Get<IInterface>();
			var instance2 = kernel.Get<IInterface>();

			// Assert
			instance2.Should().NotBeSameAs(instance1);
		}

		[Test]
		public void singleton_lifetime_is_available()
		{
			// Arrange
			var kernel = new StandardKernel();
			kernel.Bind<IInterface>().To<Implementation>().InSingletonScope();

			// Act
			var instance1 = kernel.Get<IInterface>();
			var instance2 = kernel.Get<IInterface>();

			// Assert
			instance2.ShouldBeEquivalentTo(instance1);
		}

		[Test]
		public void implementation_lifetime_applies_to_interface()
		{
			// Arrange
			throw new NotImplementedException();

			// Act

			// Assert
		}

		[Test]
		public void implementation_lifetime_applies_to_base_type()
		{
			// Arrange
			throw new NotImplementedException();

			// Act

			// Assert
		}

		[Test]
		public void interface_chaining_is_not_supported()
		{
			// Arrange
			throw new NotImplementedException();

			// Act

			// Assert
		}

		[Test]
		public void class_chaining_is_not_supported()
		{
			// Arrange
			throw new NotImplementedException();

			// Act

			// Assert
		}
    }
}
