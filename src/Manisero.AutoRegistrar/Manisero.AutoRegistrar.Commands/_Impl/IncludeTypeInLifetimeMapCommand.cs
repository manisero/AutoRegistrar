using Manisero.AutoRegistrar.Queries;
using Manisero.AutoRegistrar.Queries.LongestLifetime;
using System.Linq;

namespace Manisero.AutoRegistrar.Commands._Impl
{
	public class IncludeTypeInLifetimeMapCommand<TLifetime> : IIncludeTypeInLifetimeMapCommand<TLifetime>
	{
		private readonly ITypeDependenciesQuery _typeDependenciesQuery;
		private readonly ILongestLifetimeQuery<TLifetime> _longestLifetimeQuery;

		public IncludeTypeInLifetimeMapCommand(ITypeDependenciesQuery typeDependenciesQuery, ILongestLifetimeQuery<TLifetime> longestLifetimeQuery)
		{
			_typeDependenciesQuery = typeDependenciesQuery;
			_longestLifetimeQuery = longestLifetimeQuery;
		}

		public Void Execute(IncludeTypeInLifetimeMapCommandParameter<TLifetime> parameter)
		{
			var dependencies = _typeDependenciesQuery.Execute(new TypeDependenciesQueryParameter
				{
					Type = parameter.Type
				});

			if (!dependencies.Any())
			{
				parameter.LifetimeMap[parameter.Type] = _longestLifetimeQuery.Execute(Void.Value);
			}

			return Void.Value;
		}
	}
}
