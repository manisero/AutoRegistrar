namespace Manisero.AutoRegistrar.Queries.Tests.Stubs.IntLifetme
{
	public class IsIntLifetimeShorterThanQuery : IIsLifetimeShorterThanQuery<int>
	{
		public bool Execute(IsLifetimeShorterThanQueryParameter<int> parameter)
		{
			return parameter.Lifetime < parameter.OtherLifetime;
		}
	}
}
