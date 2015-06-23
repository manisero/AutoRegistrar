namespace Manisero.AutoRegistrar.Queries.LongestLifetime.LongestLifetimeQueries
{
	public class LongestIntLifetimeQuery : ILongestLifetimeQuery<int>
	{
		public int Execute()
		{
			return int.MaxValue;
		}
	}
}
