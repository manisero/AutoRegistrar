using System;
using System.Collections.Generic;

namespace Manisero.AutoRegistrar.Queries
{
	public interface ITypeDependenciesQuery : IQuery<Type, IReadOnlyList<Type>>
	{
	}
}
