using System;
using System.Collections.Generic;

namespace Manisero.AutoRegistrar.Commands
{
	public class IncludeTypeInTypeMapCommandParameter
	{
		public IDictionary<Type, Type> TypeMap { get; set; }

		public Type Type { get; set; }

		public IEnumerable<Type> AvailableTypes { get; set; }
	}

	public interface IIncludeTypeInTypeMapCommand : ICommand<IncludeTypeInTypeMapCommandParameter>
	{
	}
}
