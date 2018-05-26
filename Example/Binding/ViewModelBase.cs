using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace TestProject.Binding
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpresion)
		{
			var property = (MemberExpression)propertyExpresion.Body;

			if (property.Member.GetType().IsAssignableFrom(typeof(PropertyInfo)))
			{
				throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, "Invalid Property Expression {0}", propertyExpresion));
			}

			if (((ConstantExpression)property.Expression).Value != this)
			{
				throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, "Invalid Property Expression {0}", propertyExpresion));
			}

			OnPropertyChanged(property.Member.Name);
		}
	}
}