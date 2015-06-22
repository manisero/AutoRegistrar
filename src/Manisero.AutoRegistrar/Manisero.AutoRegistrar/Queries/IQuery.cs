namespace Manisero.AutoRegistrar.Queries
{
	public interface IQuery<TParameter, TResult>
	{
		TResult Execute(TParameter parameter);
	}
}
