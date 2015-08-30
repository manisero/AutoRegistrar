using System;
using FluentAssertions;
using NUnit.Framework;
using Shared.ClassInheritance;
using Shared.InterfaceChain;
using Shared.InterfaceImplementation;
using SimpleInjector;

namespace SimpleInjectorKnowledgeBase
{
    public class KnowledgeBase
    {
        [Test]
        public void default_lifetime_is_transient()
        {
            // Arrange
            var container = new Container();
            container.Register<IInterface, Implementation>();

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
            container.Register<IInterface, Implementation>(Lifestyle.Singleton);

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
            var registration = Lifestyle.Singleton.CreateRegistration<Implementation>(container);
            container.AddRegistration(typeof(IInterface), registration);

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
            var registration = Lifestyle.Singleton.CreateRegistration<Child>(container);
            container.AddRegistration(typeof(Parent), registration);

            // Act
            var instance1 = container.GetInstance<Parent>();
            var instance2 = container.GetInstance<Parent>();

            // Assert
            instance2.ShouldBeEquivalentTo(instance1);
        }

        [Test]
        public void interface_chaining_is_not_supported()
        {
            // Arrange
            var container = new Container();

            Action act = () =>
                {
                    container.Register<IParentInterface, IChildInterface>();
                    container.Register<IChildInterface, InterfaceChainImplementation>();

                    container.GetInstance<IParentInterface>();
                };

            // Assert
            act.ShouldThrow<Exception>();
        }
    }
}
