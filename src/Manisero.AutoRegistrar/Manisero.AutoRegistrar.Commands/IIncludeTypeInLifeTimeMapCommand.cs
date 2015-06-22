using System;
using System.Collections.Generic;

namespace Manisero.AutoRegistrar.Commands
{
	public class IncludeTypeInLifetimeMapCommandParameter<TLifetime>
	{
		public IDictionary<Type, TLifetime> LifetimeMap { get; set; }

		public Type Type { get; set; }
	}

	public interface IIncludeTypeInLifetimeMapCommand<TLifetime> : ICommand<IncludeTypeInLifetimeMapCommandParameter<TLifetime>, Void>
		where TLifetime : IComparable
	{
	}
}
