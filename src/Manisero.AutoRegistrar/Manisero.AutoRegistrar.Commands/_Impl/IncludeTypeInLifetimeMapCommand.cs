using System;
using System.Collections.Generic;
using Manisero.AutoRegistrar.Queries;
using System.Linq;
using Manisero.AutoRegistrar.Core.Extensions;

namespace Manisero.AutoRegistrar.Commands._Impl
{
	public class IncludeTypeInLifetimeMapCommand<TLifetime> : IIncludeTypeInLifetimeMapCommand<TLifetime>
	{
		private readonly ITypeDependenciesQuery _typeDependenciesQuery;
		private readonly ILongestLifetimeQuery<TLifetime> _longestLifetimeQuery;
		private readonly IIsLifetimeShorterThanQuery<TLifetime> _isLifetimeShorterThanQuery;

		public IncludeTypeInLifetimeMapCommand(ITypeDependenciesQuery typeDependenciesQuery,
											   ILongestLifetimeQuery<TLifetime> longestLifetimeQuery,
											   IIsLifetimeShorterThanQuery<TLifetime> isLifetimeShorterThanQuery)
		{
			_typeDependenciesQuery = typeDependenciesQuery;
			_longestLifetimeQuery = longestLifetimeQuery;
			_isLifetimeShorterThanQuery = isLifetimeShorterThanQuery;
		}

		public void Execute(IncludeTypeInLifetimeMapCommandParameter<TLifetime> parameter)
		{
			if (parameter.LifetimeMap.ContainsKey(parameter.Type))
			{
				throw new InvalidOperationException("Lifetime Map already contains {0} type.".FormatWith(parameter.Type));
			}

			var lifetime = _longestLifetimeQuery.Execute();
			var dependencies = _typeDependenciesQuery.Execute(parameter.Type);

			if (dependencies.Any())
			{
				foreach (var dependency in dependencies)
				{
					TLifetime dependencyLifetime = GetDependencyLifetime(parameter.LifetimeMap, dependency, parameter.TypeMap);

					if (_isLifetimeShorterThanQuery.Execute(new IsLifetimeShorterThanQueryParameter<TLifetime>
						{
							Lifetime = dependencyLifetime,
							OtherLifetime = lifetime
						}))
					{
						lifetime = dependencyLifetime;
					}
				}
			}

			parameter.LifetimeMap[parameter.Type] = lifetime;
		}

		private TLifetime GetDependencyLifetime(IDictionary<Type, TLifetime> lifetimeMap, Type dependency, IDictionary<Type, Type> typeMap)
		{
			if (!lifetimeMap.ContainsKey(dependency))
			{
				if (typeMap.Values.Contains(dependency))
				{
					Execute(new IncludeTypeInLifetimeMapCommandParameter<TLifetime>
						{
							LifetimeMap = lifetimeMap,
							Type = dependency,
							TypeMap = typeMap
						});
				}
				else if (typeMap.ContainsKey(dependency))
				{
					var dependencyImplementation = typeMap[dependency];

					if (!lifetimeMap.ContainsKey(dependencyImplementation))
					{
						Execute(new IncludeTypeInLifetimeMapCommandParameter<TLifetime>
							{
								LifetimeMap = lifetimeMap,
								Type = dependencyImplementation,
								TypeMap = typeMap
							});
					}

					lifetimeMap[dependency] = lifetimeMap[dependencyImplementation];
				}
				else
				{
					throw new InvalidOperationException("Unable to determine lifetime of {0} type. The type's implementation is not known.".FormatWith(dependency));
				}
			}

			return lifetimeMap[dependency];
		}
	}
}
