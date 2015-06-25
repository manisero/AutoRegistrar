using System;
using System.Collections.Generic;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase._Impl;

namespace Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario
{
	public static class Configuration
	{
		public static readonly IDictionary<Type, Lifetime> INITIAL_LIFETIME_MAP = new Dictionary<Type, Lifetime>
			{
				{ typeof(GlobalState), Lifetime.Sigleton },
				{ typeof(DataContext), Lifetime.Request },
				{ typeof(RandomBehavior), Lifetime.Transient }
			};
	}
}
