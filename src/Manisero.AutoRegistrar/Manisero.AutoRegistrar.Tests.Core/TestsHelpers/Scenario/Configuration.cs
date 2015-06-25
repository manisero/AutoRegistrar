using System;
using System.Collections.Generic;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase;
using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase._Impl;

namespace Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario
{
	public static class Configuration
	{
		public static readonly IReadOnlyDictionary<Type, TestLifetime> INITIAL_LIFETIME_MAP = new Dictionary<Type, TestLifetime>
			{
				{ typeof(GlobalState), TestLifetime.Sigleton },
				{ typeof(DataContext), TestLifetime.Request },
				{ typeof(RandomBehavior), TestLifetime.Transient }
			};
	}
}
