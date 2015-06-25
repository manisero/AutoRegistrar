using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario;

namespace Manisero.AutoRegistrar.Queries.Tests.Stubs.TestLifetimeLifetime
{
	public class LongestTestLifetimeQuery : ILongestLifetimeQuery<TestLifetime>
	{
		public TestLifetime Execute()
		{
			return TestLifetime.Sigleton;
		}
	}
}
