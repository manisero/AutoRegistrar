namespace Manisero.AutoRegistrar.Queries
{
	public class IsLifetimeShorterThanQueryParameter<TLifetime>
	{
		public TLifetime Lifetime { get; set; }

		public TLifetime OtherLifetime { get; set; }
	}

	public interface IIsLifetimeShorterThanQuery<TLifetime> : IQuery<IsLifetimeShorterThanQueryParameter<TLifetime>, bool>
	{
	}
}
