namespace Manisero.AutoRegistrar.Commands
{
	public interface ICommand<TParameter>
	{
		void Execute(TParameter parameter);
	}
}
