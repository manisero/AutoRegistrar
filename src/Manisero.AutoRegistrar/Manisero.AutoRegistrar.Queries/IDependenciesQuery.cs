using System;
using System.Collections.Generic;

namespace Manisero.AutoRegistrar.Queries
{
	public class DependenciesQueryParameter
	{
		public Type Type { get; set; }
	}

	public interface IDependenciesQuery : IQuery<DependenciesQueryParameter, IReadOnlyList<Type>>
	{
	}
}
