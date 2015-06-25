using Manisero.AutoRegistrar.Tests.Core.TestsHelpers.Scenario;

namespace Manisero.AutoRegistrar.Queries.Tests.Stubs.TestLifetimeLifetime
{
	public class IsTestLifetimeShorterThanQuery : IIsLifetimeShorterThanQuery<TestLifetime>
	{
		public bool Execute(IsLifetimeShorterThanQueryParameter<TestLifetime> parameter)
		{
			return parameter.Lifetime < parameter.OtherLifetime;
		}
	}
}
