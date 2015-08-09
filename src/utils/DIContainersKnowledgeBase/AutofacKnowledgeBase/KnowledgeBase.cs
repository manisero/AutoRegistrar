using Autofac;
using FluentAssertions;
using NUnit.Framework;
using Shared.InterfaceImplementation;

namespace AutofacKnowledgeBase
{
    public class KnowledgeBase
    {
        [Test]
        public void default_lifetime_is_transient()
        {
            // Arrange
            var builder = new ContainerBuilder();
            builder.RegisterType<Implementation>().As<IInterface>();

            var container = builder.Build();

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
            var builder = new ContainerBuilder();
            builder.RegisterType<Implementation>().As<IInterface>().SingleInstance();

            var container = builder.Build();

            // Act
            var instance1 = container.Resolve<IInterface>();
            var instance2 = container.Resolve<IInterface>();

            // Assert
            instance2.ShouldBeEquivalentTo(instance1);
        }

        [Test]
        public void implementation_lifetime_does_not_apply_to_interface()
        {
            // Arrange
            var builder = new ContainerBuilder();
            builder.RegisterType<Implementation>().As<IInterface>();
            builder.RegisterType<Implementation>().SingleInstance();

            var container = builder.Build();

            // Act
            var instance1 = container.Resolve<IInterface>();
            var instance2 = container.Resolve<IInterface>();

            // Assert
            instance2.Should().NotBe(instance1);
        }
    }
}
