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

			return availableAssemblies.SelectMany(x => RetrieveTypes(x, parameter.TypeFilter))
									  .ToList();
		}

		private void IncludeAssembly(Assembly assembly, Func<AssemblyName, bool> referencedAssemblyFilter, HashSet<Assembly> availableAssemblies)
		{
			if (availableAssemblies.Contains(assembly))
			{
				return;
			}

			availableAssemblies.Add(assembly);

			var referencedAssemblyNames = (IEnumerable<AssemblyName>)assembly.GetReferencedAssemblies();
				
			if (referencedAssemblyFilter != null)
			{
				referencedAssemblyNames = referencedAssemblyNames.Where(referencedAssemblyFilter);
			}

			foreach (var referencedAssemblyName in referencedAssemblyNames)
			{
				var referencedAssembly = Assembly.Load(referencedAssemblyName);
				IncludeAssembly(referencedAssembly, referencedAssemblyFilter, availableAssemblies);
			}
		}

		private IEnumerable<Type> RetrieveTypes(Assembly assembly, Func<Type, bool> typeFilter)
		{
			var types = assembly.ExportedTypes;

			if (typeFilter != null)
			{
				types = types.Where(typeFilter);
			}

			return types;
		}
	}
}
