﻿using System;
using NUnit.Framework;
using UIComponents.Core;
using UIComponents.Core.Exceptions;

namespace UIComponents.Tests
{
    [TestFixture]
    public class DependencyInjectorTests
    {
        private interface IDependency {}

        public class DependencyOne : IDependency {}

        public class DependencyTwo : IDependency {}

        [Test]
        public void Can_Be_Created_Using_DependencyAttributes()
        {
            var dependencyAttributes = new[]
            {
                new DependencyAttribute(typeof(IDependency), typeof(DependencyOne))
            };

            var injector = new DependencyInjector(dependencyAttributes);
            
            Assert.That(injector.Provide<IDependency>(), Is.InstanceOf<DependencyOne>());
        }

        [TestFixture]
        public class Provide
        {
            [Test]
            public void Returns_Desired_Dependency()
            {
                var injector = new DependencyInjector();
                injector.SetDependency<IDependency>(new DependencyOne());
                Assert.That(injector.Provide<IDependency>(), Is.InstanceOf<DependencyOne>());
            }

            [Test]
            public void Throws_If_No_Provider_Exists()
            {
                var injector = new DependencyInjector();

                var exception = Assert.Throws<MissingProviderException>(
                    () => injector.Provide<IDependency>()
                );
                
                Assert.That(exception.Message, Is.EqualTo("No provider found for IDependency"));
            }
        }

        [TestFixture]
        public class TryProvide
        {
            [Test]
            public void Returns_If_Dependency_Could_Be_Provided()
            {
                var injector = new DependencyInjector();
                
                Assert.That(injector.TryProvide<IDependency>(out _), Is.False);
                
                injector.SetDependency<IDependency>(new DependencyOne());

                var wasProvided = injector.TryProvide<IDependency>(out var instance);
                
                Assert.That(wasProvided, Is.True);
                Assert.That(instance, Is.InstanceOf<DependencyOne>());
            }

            [Test]
            public void Yields_Null_If_Dependency_Can_Not_Be_Provided()
            {
                var injector = new DependencyInjector();

                injector.TryProvide<IDependency>(out var instance);
                
                Assert.That(instance, Is.Null);
            }
        }
        
        [TestFixture]
        public class SetDependency
        {
            [Test]
            public void Switches_The_Dependency()
            {
                var injector = new DependencyInjector();
                
                injector.SetDependency<IDependency>(new DependencyOne());
                injector.SetDependency<IDependency>(new DependencyTwo());
                
                Assert.That(injector.Provide<IDependency>(), Is.InstanceOf<DependencyTwo>());
            }

            [Test]
            public void Throws_Exception_If_Null_Is_Given_As_Parameter()
            {
                var injector = new DependencyInjector();

                Assert.Throws<ArgumentNullException>(
                    () => injector.SetDependency<IDependency>(null)
                );
            }
        }

        [TestFixture]
        public class ClearDependency
        {
            [Test]
            public void Removes_Dependency_Instance()
            {
                var injector = new DependencyInjector();
                
                injector.SetDependency<IDependency>(new DependencyOne());
                
                injector.ClearDependency<IDependency>();

                Assert.Throws<MissingProviderException>(() => injector.Provide<IDependency>());
            }

            [Test]
            public void Does_Not_Throw_If_Dependency_Does_Not_Exist()
            {
                var injector = new DependencyInjector();
                
                Assert.DoesNotThrow(() => injector.ClearDependency<IDependency>());
            }
        }
    }
}