using Manisero.AutoRegistrar.Commands;
using Manisero.AutoRegistrar.Commands._Impl;

namespace Manisero.AutoRegistrar.Tests.Stubs
{
	public class Registrar
	{
		public void Register()
		{
			// 1. Get available types
			var loadAndRetrieveAvailableTypesCommand = new LoadAndRetrieveAvailableTypesCommand();
			loadAndRetrieveAvailableTypesCommand.Execute(new LoadAndRetrieveAvailableTypesCommandParameter
				{
					RootAssembly = GetType().Assembly,
					ReferencedAssemblyFilter = x => x.FullName.StartsWith("Manisero.AutoRegistrar")
				});

			// 2. Build type map
			// 3. Get initial lifetime map
			// 4. Include concrete types from type map in lifetime map
			// 5. Create registration map
			// 6. Register
		}
	}
}
