using System;
using FluentAssertions;
using NUnit.Framework;
using Shared.ClassInheritance;
using Shared.ImplementationChain;
using Shared.InterfaceChain;
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

        [Test]
        public void implementation_lifetime_applies_to_interface___with_additional_configuration()
        {
            // Arrange
            var container = new Container();
            container.Configure(x =>
                {
                    x.Redirect<IInterface, Implementation>();
                    x.For<Implementation>().Singleton();
                });

            // Act
            var instance1 = container.GetInstance<IInterface>();
            var instance2 = container.GetInstance<IInterface>();

            // Assert
            instance2.ShouldBeEquivalentTo(instance1);
        }

        [Test]
        public void implementation_lifetime_applies_to_base_type___with_additional_configuration()
        {
            // Arrange
            var container = new Container();
            container.Configure(x =>
                {
                    x.Redirect<Parent, Child>();
                    x.For<Child>().Singleton();
                });

            // Act
            var instance1 = container.GetInstance<Parent>();
            var instance2 = container.GetInstance<Parent>();

            // Assert
            instance2.ShouldBeEquivalentTo(instance1);
        }

        [Test]
        public void interface_chaining_is_supported___with_additional_configuration()
        {
            // Arrange
            var container = new Container();
            container.Configure(x =>
                {
                    x.Redirect<IParentInterface, IChildInterface>();
                    x.Redirect<IChildInterface, InterfaceChainImplementation>();
                });

            // Act
            var instance = container.GetInstance<IParentInterface>();

            // Assert
            instance.Should().BeOfType<InterfaceChainImplementation>();
        }

        [Test]
        public void class_chaining_is_supported___with_additional_configuration()
        {
            // Arrange
            var container = new Container();
            container.Configure(x =>
                {
                    x.Redirect<IImplementationChainInterface, ParentImplementation>();
                    x.Redirect<ParentImplementation, ChildImplementation>();
                });

            // Act
            var instance = container.GetInstance<IImplementationChainInterface>();

            // Assert
            instance.Should().BeOfType<ChildImplementation>();
        }
    }
}
