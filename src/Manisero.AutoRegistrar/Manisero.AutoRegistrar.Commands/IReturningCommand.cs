namespace Manisero.AutoRegistrar.Commands
{
	public interface IReturningCommand<TParameter, TResult>
	{
		TResult Execute(TParameter parameter);
	}
}
