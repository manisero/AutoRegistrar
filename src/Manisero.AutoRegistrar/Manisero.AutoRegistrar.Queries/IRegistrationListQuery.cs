using System;
using System.Collections.Generic;
using Manisero.AutoRegistrar.Core;

namespace Manisero.AutoRegistrar.Queries
{
	public class RegistrationListQueryParameter<TLifetime>
	{
		public IDictionary<Type, Type> TypeMap { get; set; }

		public IDictionary<Type, TLifetime> LifetimeMap { get; set; }
	}

	public interface IRegistrationListQuery<TLifetime> : IQuery<RegistrationListQueryParameter<TLifetime>, IList<Registration<TLifetime>>>
	{
	}
}
