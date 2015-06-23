using System;

namespace Shared.InterfaceImplementation
{
	public class Implementation : IInterface
	{
		private readonly Guid _id = Guid.NewGuid();

		public Guid ID
		{
			get { return _id; }
		}
	}
}
