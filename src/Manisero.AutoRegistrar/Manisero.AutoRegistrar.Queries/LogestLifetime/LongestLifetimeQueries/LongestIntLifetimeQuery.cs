namespace Manisero.AutoRegistrar.Queries.LogestLifetime.LongestLifetimeQueries
{
	public class LongestIntLifetimeQuery : ILongestLifetimeQuery<int>
	{
		public int Execute(Void parameter)
		{
			return int.MaxValue;
		}
	}
}
