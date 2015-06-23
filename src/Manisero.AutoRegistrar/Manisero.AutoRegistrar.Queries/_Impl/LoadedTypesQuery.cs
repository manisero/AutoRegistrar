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

			return knownAssemblies.SelectMany(x => x.ExportedTypes)
								  .ToList();
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
				try
				{
					var referencedAssembly = Assembly.ReflectionOnlyLoad(referencedAssemblyName.FullName);
					IncludeAssembly(referencedAssembly, knownAssemblies);
				}
				catch (Exception)
				{
				}
			}
		}
	}
}
