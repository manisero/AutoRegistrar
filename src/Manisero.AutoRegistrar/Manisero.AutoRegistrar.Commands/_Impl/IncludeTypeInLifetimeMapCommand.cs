using System;
using Manisero.AutoRegistrar.Queries;
using Manisero.AutoRegistrar.Queries.LongestLifetime;
using System.Linq;
using Manisero.AutoRegistrar.Extensions;

namespace Manisero.AutoRegistrar.Commands._Impl
{
	public class IncludeTypeInLifetimeMapCommand<TLifetime> : IIncludeTypeInLifetimeMapCommand<TLifetime>
		where TLifetime : IComparable
	{
		private readonly ITypeDependenciesQuery _typeDependenciesQuery;
		private readonly ILongestLifetimeQuery<TLifetime> _longestLifetimeQuery;

		public IncludeTypeInLifetimeMapCommand(ITypeDependenciesQuery typeDependenciesQuery, ILongestLifetimeQuery<TLifetime> longestLifetimeQuery)
		{
			_typeDependenciesQuery = typeDependenciesQuery;
			_longestLifetimeQuery = longestLifetimeQuery;
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

					if (dependencyLifetime.CompareTo(lifetime) < 0)
					{
						lifetime = dependencyLifetime;
					}
				}
			}

			parameter.LifetimeMap[parameter.Type] = lifetime;
		}
	}
}
