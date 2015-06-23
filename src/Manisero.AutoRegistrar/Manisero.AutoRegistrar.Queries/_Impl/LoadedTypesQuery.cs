using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Manisero.AutoRegistrar.Queries._Impl
{
	public class LoadedTypesQuery : ILoadedTypesQuery
	{
		public IEnumerable<Type> Execute()
		{
			var knownAssemblies = new HashSet<Assembly>();
			var entryAssembly = Assembly.GetCallingAssembly();

			IncludeAssembly(entryAssembly, knownAssemblies);

			var knownTypes = knownAssemblies.SelectMany(x => x.ExportedTypes)
											.ToList();

			return knownTypes;
		}

		private void IncludeAssembly(Assembly assembly, HashSet<Assembly> knownAssemblies)
		{
			if (knownAssemblies.Contains(assembly))
			{
				return;
			}

			knownAssemblies.Add(assembly);

			foreach (var referencedAssemblyName in assembly.GetReferencedAssemblies())
			{
				var referencedAssembly = Assembly.Load(referencedAssemblyName);

				IncludeAssembly(referencedAssembly, knownAssemblies);
			}
		}
	}
}
