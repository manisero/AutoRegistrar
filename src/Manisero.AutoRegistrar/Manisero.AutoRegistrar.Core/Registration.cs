using System;

namespace Manisero.AutoRegistrar.Core
{
	public class Registration<TLifetime>
	{
		public Type SourceType { get; set; }

		public Type DestinationType { get; set; }

		public TLifetime Lifetime { get; set; }
	}
}
