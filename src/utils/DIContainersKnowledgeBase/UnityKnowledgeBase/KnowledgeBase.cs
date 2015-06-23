using System;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Shared.ClassInheritance;
using Shared.ImplementationChain;
using Shared.InterfaceChain;
using Shared.InterfaceImplementation;
using FluentAssertions;

namespace UnityKnowledgeBase
{
    public class KnowledgeBase
    {
		[Test]
		public void default_lifetime_is_transient()
		{
			// Arrange
			var container = new UnityContainer();
			container.RegisterType<IInterface, Implementation>();

			// Act
			var instance1 = container.Resolve<IInterface>();
			var instance2 = container.Resolve<IInterface>();

			// Assert
			instance2.Should().NotBeSameAs(instance1);
		}

		[Test]
		public void singleton_lifetime_is_available()
		{
			// Arrange
			var container = new UnityContainer();
			container.RegisterType<IInterface, Implementation>(new ContainerControlledLifetimeManager());

			// Act
			var instance1 = container.Resolve<IInterface>();
			var instance2 = container.Resolve<IInterface>();

			// Assert
			instance2.ShouldBeEquivalentTo(instance1);
		}

		[Test]
		public void implementation_lifetime_applies_to_interface()
		{
			// Arrange
			var container = new UnityContainer();
			container.RegisterType<IInterface, Implementation>();
			container.RegisterType<Implementation>(new ContainerControlledLifetimeManager());

			// Act
			var instance1 = container.Resolve<IInterface>();
			var instance2 = container.Resolve<IInterface>();

			// Assert
			instance2.ShouldBeEquivalentTo(instance1);
		}

		[Test]
		public void implementation_lifetime_applies_to_base_type()
		{
			// Arrange
			var container = new UnityContainer();
			container.RegisterType<Parent, Child>();
			container.RegisterType<Child>(new ContainerControlledLifetimeManager());

			// Act
			var instance1 = container.Resolve<Parent>();
			var instance2 = container.Resolve<Parent>();

			// Assert
			instance2.ShouldBeEquivalentTo(instance1);
		}

		[Test]
		public void interface_chaining_is_not_supported()
		{
			// Arrange
			var container = new UnityContainer();
			container.RegisterType<IParentInterface, IChildInterface>();
			container.RegisterType<IChildInterface, InterfaceChainImplementation>();
			container.RegisterType<InterfaceChainImplementation>();

			Action act = () => container.Resolve<IParentInterface>();

			// Assert
			act.ShouldThrow<Exception>();
		}

		[Test]
		public void class_chaining_is_not_supported()
		{
			// Arrange
			var container = new UnityContainer();
			container.RegisterType<IImplementationChainInterface, ParentImplementation>();
			container.RegisterType<ParentImplementation, ChildImplementation>();
			container.RegisterType<ChildImplementation>(new ContainerControlledLifetimeManager());

			// Act
			var instance = container.Resolve<IImplementationChainInterface>();

			// Assert
			instance.GetType().Should().NotBe(typeof(ChildImplementation));
		}
    }
}
