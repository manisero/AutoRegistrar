namespace Manisero.AutoRegistrar.Queries.Tests.Stubs.IntLifetme
{
	public class LongestIntLifetimeQuery : ILongestLifetimeQuery<int>
	{
		public int Execute()
		{
			return int.MaxValue;
		}
	}
}
