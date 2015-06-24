namespace Manisero.AutoRegistrar.Commands._Impl
{
	public class IncludeTypeInTypeMapCommand : IIncludeTypeInTypeMapCommand
	{
		public void Execute(IncludeTypeInTypeMapCommandParameter parameter)
		{
			if (!parameter.Type.IsAbstract)
			{
				parameter.TypeMap.Add(parameter.Type, parameter.Type);
			}
		}
	}
}
