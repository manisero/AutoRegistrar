using System;
using System.Collections.Generic;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase;
using Manisero.AutoRegistrar.Tests.Stubs;
using NUnit.Framework;

namespace Manisero.AutoRegistrar.Tests
{
	public class RegistrarTests
	{
		[Test]
		public void test()
		{
			// Arrange
			var registrar = new Registrar();

			// Act
			registrar.Register(GetType().Assembly,
							   x => x.FullName == typeof(GlobalState).Assembly.FullName,
							   x => x.Namespace != null && x.Namespace.StartsWith(typeof(GlobalState).Namespace),
							   new Dictionary<Type, Type>(),
							   new Dictionary<Type, TestLifetime>());

			// Assert
		}
	}
}
