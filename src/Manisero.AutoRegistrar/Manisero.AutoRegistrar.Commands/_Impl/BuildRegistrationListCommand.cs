using System.Collections.Generic;
using Manisero.AutoRegistrar.Core;

namespace Manisero.AutoRegistrar.Commands._Impl
{
	public class BuildRegistrationListCommand<TLifetime> : IBuildRegistrationListCommand<TLifetime>
	{
		public IList<Registration<TLifetime>> Execute(BuildRegistrationListCommandParameter<TLifetime> parameter)
		{
			throw new System.NotImplementedException();
		}
	}
}
