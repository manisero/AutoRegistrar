using System;
using System.Collections.Generic;
using Manisero.AutoRegistrar.Queries.LogestLifetime;

namespace Manisero.AutoRegistrar.Commands._Impl
{
	public class IncludeTypeInLifetimeMapCommand<TLifetime> : IIncludeTypeInLifetimeMapCommand<TLifetime>
	{
		private readonly ILongestLifetimeQuery<TLifetime> _longestLifetimeQuery;

		public IncludeTypeInLifetimeMapCommand(ILongestLifetimeQuery<TLifetime> longestLifetimeQuery)
		{
			_longestLifetimeQuery = longestLifetimeQuery;
		}

		public IDictionary<Type, TLifetime> Execute(IncludeTypeInLifetimeMapCommandParameter<TLifetime> parameter)
		{
			throw new NotImplementedException();
		}
	}
}
