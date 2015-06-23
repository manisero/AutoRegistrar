using System;

namespace Shared.ImplementationChain
{
	public class ParentImplementation : IImplementationChainInterface
	{
		private readonly Guid _id = Guid.NewGuid();

		public Guid ID
		{
			get { return _id; }
		}
	}
}
