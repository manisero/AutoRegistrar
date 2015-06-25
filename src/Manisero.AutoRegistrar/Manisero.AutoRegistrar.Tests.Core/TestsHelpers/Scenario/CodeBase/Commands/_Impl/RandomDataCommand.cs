using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase.Queries;

namespace Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario.CodeBase.Commands._Impl
{
	public class RandomDataCommand : IRandomDataCommand
	{
		public RandomDataCommand(IDataQuery dataQuery, IRandomBehavior randomBehavior)
		{
		}
	}
}
