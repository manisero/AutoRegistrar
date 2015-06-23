using FluentAssertions;
using NUnit.Framework;
using Ninject;
using Shared.InterfaceImplementation;

namespace NInjectKnowledgeBase
{
    public class KnowledgeBase
    {
		[Test]
		public void default_lifetime_is_transient()
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
		public void implementation_lifetime_does_not_apply_to_interface()
		{
			// Arrange
			var kernel = new StandardKernel();

			kernel.Bind<IInterface>().To<Implementation>();
			kernel.Bind<Implementation>().ToSelf().InSingletonScope();

			// Act
			var instance1 = kernel.Get<IInterface>();
			var instance2 = kernel.Get<IInterface>();

			// Assert
			instance2.Should().NotBe(instance1);
		}
    }
}
