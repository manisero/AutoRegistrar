using System;
using System.Collections.Generic;
using Manisero.AutoRegistrar.Core;

namespace Manisero.AutoRegistrar.Queries._Impl
{
	public class RegistrationListQuery<TLifetime> : IRegistrationListQuery<TLifetime>
	{
		public IList<Registration<TLifetime>> Execute(RegistrationListQueryParameter<TLifetime> parameter)
		{
			throw new NotImplementedException();
		}
	}
}
