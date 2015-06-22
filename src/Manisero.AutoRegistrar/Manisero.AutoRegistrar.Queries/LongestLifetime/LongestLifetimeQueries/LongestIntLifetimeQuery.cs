namespace Manisero.AutoRegistrar.Queries.LongestLifetime.LongestLifetimeQueries
{
	public class LongestIntLifetimeQuery : ILongestLifetimeQuery<int>
	{
		public int Execute(Void parameter)
		{
			return int.MaxValue;
		}
	}
}
