using System;
using System.Collections.Generic;

namespace Manisero.AutoRegistrar.Queries
{
	public interface ILoadedTypesQuery : IParameterlessQuery<IEnumerable<Type>>
	{
	}
}
