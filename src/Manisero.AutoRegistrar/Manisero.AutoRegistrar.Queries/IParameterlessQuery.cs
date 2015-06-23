namespace Manisero.AutoRegistrar.Queries
{
	public interface IParameterlessQuery<TResult>
	{
		TResult Execute();
	}
}
