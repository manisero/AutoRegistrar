namespace Manisero.AutoRegistrar.Queries.Tests.Stubs
{
	public class LongestIntLifetimeQuery : ILongestLifetimeQuery<int>
	{
		public int Execute()
		{
			return int.MaxValue;
		}
	}
}
