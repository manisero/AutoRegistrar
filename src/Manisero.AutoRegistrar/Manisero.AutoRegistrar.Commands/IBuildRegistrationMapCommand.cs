using System;
using System.Collections.Generic;

namespace Manisero.AutoRegistrar.Commands
{
	public class BuildRegistrationMapCommandParameter
	{
		
	}

	public interface IBuildRegistrationMapCommand : IReturningCommand<BuildRegistrationMapCommandParameter, IDictionary<Type, Type>>
	{
	}
}
