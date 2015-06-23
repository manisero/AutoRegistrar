namespace Manisero.AutoRegistrar.Core.Extensions
{
	public static class StringExtensions
	{
		public static string FormatWith(this string format, params object[] arguments)
		{
			return string.Format(format, arguments);
		}
	}
}
