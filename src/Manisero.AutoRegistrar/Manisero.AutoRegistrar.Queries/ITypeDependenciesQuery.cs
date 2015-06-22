using System;
using System.Collections.Generic;

namespace Manisero.AutoRegistrar.Queries
{
	public class TypeDependenciesQueryParameter
	{
		public Type Type { get; set; }
	}

	public interface ITypeDependenciesQuery : IQuery<TypeDependenciesQueryParameter, IReadOnlyList<Type>>
	{
	}
}
