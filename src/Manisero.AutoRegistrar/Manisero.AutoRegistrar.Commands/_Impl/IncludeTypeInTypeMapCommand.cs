namespace Manisero.AutoRegistrar.Commands._Impl
{
	public class IncludeTypeInTypeMapCommand : IIncludeTypeInTypeMapCommand
	{
		public void Execute(IncludeTypeInTypeMapCommandParameter parameter)
		{
			if (!parameter.Type.IsAbstract)
			{
				var interfaces = parameter.Type.GetInterfaces();

				foreach (var @interface in interfaces)
				{
					parameter.TypeMap.Add(@interface, parameter.Type);
				}
			}
		}
	}
}
