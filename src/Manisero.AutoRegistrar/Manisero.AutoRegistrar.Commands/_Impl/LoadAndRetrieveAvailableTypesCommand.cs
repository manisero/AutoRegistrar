using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Manisero.AutoRegistrar.Commands._Impl
{
	public class LoadAndRetrieveAvailableTypesCommand : ILoadAndRetrieveAvailableTypesCommand
	{
		public IList<Type> Execute(LoadAndRetrieveAvailableTypesCommandParameter parameter)
		{
			var availableAssemblies = new HashSet<Assembly>();
			IncludeAssembly(parameter.RootAssembly, parameter.ReferencedAssemblyFilter, availableAssemblies);

			return availableAssemblies.SelectMany(x => x.ExportedTypes)
									  .ToList();
		}

		private void IncludeAssembly(Assembly assembly, Func<AssemblyName, bool> referencedAssemblyFilter, HashSet<Assembly> availableAssemblies)
		{
			if (availableAssemblies.Contains(assembly))
			{
				return;
			}

			availableAssemblies.Add(assembly);

			foreach (var referencedAssemblyName in assembly.GetReferencedAssemblies().Where(referencedAssemblyFilter))
			{
				var referencedAssembly = Assembly.Load(referencedAssemblyName);
				IncludeAssembly(referencedAssembly, referencedAssemblyFilter, availableAssemblies);
			}
		}
	}
}
