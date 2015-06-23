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
			registrar.Register();

			// Assert
		}
	}
}
