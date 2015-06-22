namespace Manisero.AutoRegistrar.Commands
{
	public interface ICommand<TParameter, TResult>
	{
		TResult Execute(TParameter parameter);
	}
}
