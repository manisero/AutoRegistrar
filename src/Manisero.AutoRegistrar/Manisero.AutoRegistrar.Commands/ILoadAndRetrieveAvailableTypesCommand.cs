using System;
using System.Collections.Generic;
using System.Reflection;

namespace Manisero.AutoRegistrar.Commands
{
	public class LoadAndRetrieveAvailableTypesCommandParameter
	{
		public Assembly RootAssembly { get; set; }

		public Func<AssemblyName, bool> ReferencedAssemblyFilter { get; set; }
	}

	public interface ILoadAndRetrieveAvailableTypesCommand : IReturningCommand<LoadAndRetrieveAvailableTypesCommandParameter, IList<Type>>
	{
	}
}