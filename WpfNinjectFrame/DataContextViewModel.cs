using System;

namespace NinjectFrame
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class DataContextViewModel : Attribute
	{
		public DataContextViewModel(Type type)
		{
			Type = type;
		}

		public Type Type { get; }
	}
}