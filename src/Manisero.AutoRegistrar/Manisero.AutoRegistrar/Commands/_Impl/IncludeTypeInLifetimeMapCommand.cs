using System;
using System.Collections.Generic;

namespace Manisero.AutoRegistrar.Commands._Impl
{
	public class IncludeTypeInLifetimeMapCommand<TLifetime> : IIncludeTypeInLifetimeMapCommand<TLifetime>
	{
		public IDictionary<Type, TLifetime> Execute(IncludeTypeInLifetimeMapCommandParameter<TLifetime> parameter)
		{
			throw new NotImplementedException();
		}
	}
}
