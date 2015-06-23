using System;

namespace Shared.ClassInheritance
{
	public class Parent
	{
		private readonly Guid _id = Guid.NewGuid();

		public Guid ID
		{
			get { return _id; }
		}
	}
}
