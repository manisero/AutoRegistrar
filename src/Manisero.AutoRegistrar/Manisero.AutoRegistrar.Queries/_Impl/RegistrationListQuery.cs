using System.Collections.Generic;
using System.Linq;
using Manisero.AutoRegistrar.Core;

namespace Manisero.AutoRegistrar.Queries._Impl
{
	public class RegistrationListQuery<TLifetime> : IRegistrationListQuery<TLifetime>
	{
		public IList<Registration<TLifetime>> Execute(RegistrationListQueryParameter<TLifetime> parameter)
		{
			return parameter.TypeMap.
							 Select(x => new Registration<TLifetime>
								 {
									 SourceType = x.Key,
									 DestinationType = x.Value,
									 Lifetime = parameter.LifetimeMap[x.Value]
								 })
							.ToList();
		}
	}
}
