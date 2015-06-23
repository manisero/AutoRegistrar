using System;
using Manisero.AutoRegistrar.Core.Extensions;
using Manisero.AutoRegistrar.Queries;
using Manisero.AutoRegistrar.Queries.LongestLifetime;
using System.Linq;

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
				throw new InvalidOperationException("Lifetime Map already contains {0} type".FormatWith(parameter.Type));
			}

			var lifetime = _longestLifetimeQuery.Execute();
			var dependencies = _typeDependenciesQuery.Execute(parameter.Type);

			if (dependencies.Any())
			{
				foreach (var dependency in dependencies)
				{
					TLifetime dependencyLifetime;

					if (!parameter.LifetimeMap.TryGetValue(dependency, out dependencyLifetime))
					{
						Execute(new IncludeTypeInLifetimeMapCommandParameter<TLifetime>
							{
								LifetimeMap = parameter.LifetimeMap,
								Type = dependency
							});

						dependencyLifetime = parameter.LifetimeMap[dependency];
					}

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
	}
}
