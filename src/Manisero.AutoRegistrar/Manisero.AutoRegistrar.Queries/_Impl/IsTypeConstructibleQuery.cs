using System;
using System.Linq;

namespace Manisero.AutoRegistrar.Queries._Impl
{
	public class IsTypeConstructibleQuery : IIsTypeConstructibleQuery
	{
		public bool Execute(Type parameter)
		{
			return parameter.GetConstructors().Any();
		}
	}
}
