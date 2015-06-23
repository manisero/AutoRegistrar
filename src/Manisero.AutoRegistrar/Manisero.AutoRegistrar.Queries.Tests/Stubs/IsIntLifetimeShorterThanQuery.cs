namespace Manisero.AutoRegistrar.Queries.Tests.Stubs
{
	public class IsIntLifetimeShorterThanQuery : IIsLifetimeShorterThanQuery<int>
	{
		public bool Execute(IsLifetimeShorterThanQueryParameter<int> parameter)
		{
			return parameter.Lifetime < parameter.OtherLifetime;
		}
	}
}
