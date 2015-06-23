using System;

namespace Shared.InterfaceChain
{
	public class InterfaceChainImplementation : IChildInterface
	{
		private readonly Guid _id = Guid.NewGuid();

		public Guid ID
		{
			get { return _id; }
		}
	}
}