using FluentAssertions;
using NUnit.Framework;
using Shared.InterfaceImplementation;
using StructureMap;

namespace StructureMapKnowledgeBase
{
    public class KnowledgeBase
    {
        [Test]
        public void default_lifetime_is_transient()
        {
            // Arrange
            var container = new Container();
            container.Configure(x => x.For<IInterface>().Use<Implementation>());

            // Act
            var instance1 = container.GetInstance<IInterface>();
            var instance2 = container.GetInstance<IInterface>();

            // Assert
            instance2.Should().NotBeSameAs(instance1);
        }

        [Test]
        public void singleton_lifetime_is_available()
        {
            // Arrange
            var container = new Container();
            container.Configure(x => x.For<IInterface>().Use<Implementation>().Singleton());

            // Act
            var instance1 = container.GetInstance<IInterface>();
            var instance2 = container.GetInstance<IInterface>();

            // Assert
            instance2.ShouldBeEquivalentTo(instance1);
        }
    }
}
