using System;
using System.Collections.Generic;
using System.Reflection;

namespace Manisero.AutoRegistrar.Queries
{
	public class AvailableTypesQueryParameter
	{
		public Assembly RootAssembly { get; set; }

		public Func<AssemblyName, bool> ReferencedAssemblyFilter { get; set; }
	}

	public interface IAvailableTypesQuery : IQuery<AvailableTypesQueryParameter, IEnumerable<Type>>
	{
	}
}
