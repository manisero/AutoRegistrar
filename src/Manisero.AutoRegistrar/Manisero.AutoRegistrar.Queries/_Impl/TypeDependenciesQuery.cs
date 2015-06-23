using System;
using System.Collections.Generic;
using System.Linq;
using Manisero.AutoRegistrar.Extensions;

namespace Manisero.AutoRegistrar.Queries._Impl
{
	public class TypeDependenciesQuery : ITypeDependenciesQuery
	{
		public IReadOnlyList<Type> Execute(Type parameter)
		{
			var parameterLists = parameter.GetConstructors()
										  .Select(x => x.GetParameters())
										  .OrderByDescending(x => x.Length)
										  .ToList();

			if (parameterLists.Count > 1 &&
				parameterLists[0].Length == parameterLists[1].Length)
			{
				throw new InvalidOperationException("Unable to determine main constructor for type {0}".FormatWith(parameter));
			}

			return parameterLists.First()
								 .Select(x => x.ParameterType)
								 .ToList();
		}
	}
}
