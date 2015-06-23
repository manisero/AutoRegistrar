using System;
using System.Collections.Generic;

namespace Manisero.AutoRegistrar.Queries
{
	public interface IAvailableTypesQuery : IParameterlessQuery<IEnumerable<Type>>
	{
	}
}
