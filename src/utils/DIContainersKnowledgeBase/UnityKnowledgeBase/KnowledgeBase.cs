using Microsoft.Practices.Unity;
using NUnit.Framework;
using Shared.InterfaceImplementation;
using FluentAssertions;

namespace UnityKnowledgeBase
{
    public class KnowledgeBase
    {
		[Test]
		public void default_lifetime_is_per_resolve()
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
			var container = new UnityContainer();
			container.RegisterType<IInterface, Implementation>();

		}
    }
}
