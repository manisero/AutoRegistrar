using System;
using System.Collections.Generic;
using System.Reflection;
using Manisero.AutoRegistrar.Core;

namespace Manisero.AutoRegistrar.Commands
{
	public class BuildRegistrationListCommandParameter<TLifetime>
	{
		public Assembly RootAssembly { get; set; }

		public Func<AssemblyName, bool> ReferencedAssemblyFilter { get; set; }

		public Func<Type, bool> TypeFilter { get; set; }

		public IDictionary<Type, Type> TypeMap { get; set; }

		public IDictionary<Type, TLifetime> LifetimeMap { get; set; }
	}

	public interface IBuildRegistrationListCommand<TLifetime> : IReturningCommand<BuildRegistrationListCommandParameter<TLifetime>, IList<Registration<TLifetime>>>
	{
	}
}
