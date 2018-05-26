using Ninject;
using Ninject.Parameters;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace NinjectFrame
{
	public class KernelFrame : Frame, IKernelFrameNavigationHandler, IBindingRoot
	{
		private readonly IKernel _kernel;

		public KernelFrame()
		{
			_kernel = new StandardKernel();

			_kernel.Bind<IKernelFrameNavigationHandler>().ToMethod(context => (IKernelFrameNavigationHandler)this);
			_kernel.Bind<IBindingRoot>().ToMethod(context => (IBindingRoot)this);
		}

		[Obsolete("This method has been deprecated and should no longer be used.", true)]
		public new bool Navigate(object content)
		{
			throw new NotImplementedException();
		}

		[Obsolete("This method has been deprecated and should no longer be used.", true)]
		public new bool Navigate(Uri source, object extraData)
		{
			throw new NotImplementedException();
		}

		[Obsolete("This method has been deprecated and should no longer be used.", true)]
		public new bool Navigate(Uri source)
		{
			throw new NotImplementedException();
		}

		[Obsolete("This method has been deprecated and should no longer be used.", true)]
		public new bool Navigate(object content, object extraData)
		{
			throw new NotImplementedException();
		}

		public new void RemoveBackEntry()
		{
			base.RemoveBackEntry();
		}

		private void NavigateWithDataContext<TPage>(object datacontext) where TPage : Page, new()
		{
			base.Navigate(new TPage { DataContext = datacontext });
		}

		private Type GetDataContextViewModelType<TPage>() where TPage : Page, new()
		{
			var typeAttribute = typeof(TPage)
				.GetCustomAttributes(typeof(DataContextViewModel), false)
				.Cast<DataContextViewModel>()
				.FirstOrDefault();

			if (typeAttribute == null)
			{
				throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, "Missing data context attribute {0}", typeof(TPage)));
			}

			return typeAttribute.Type;
		}

		public void NavigateTo<TPage>(string name, object @param) where TPage : Page, new()
		{
			var type = GetDataContextViewModelType<TPage>();
			var datacontext = _kernel.Get(type, new ConstructorArgument(name, param));
			NavigateWithDataContext<TPage>(datacontext);
		}

		public void NavigateTo<TPage>() where TPage : Page, new()
		{
			var type = GetDataContextViewModelType<TPage>();

			var parameterAttribute = type
				.GetCustomAttributes(typeof(ViewModelParameter), false)
				.Cast<ViewModelParameter>()
				.FirstOrDefault();

			if (parameterAttribute != null)
			{
				var datacontext = _kernel.Get(GetDataContextViewModelType<TPage>(), new ConstructorArgument("params", parameterAttribute.Type.IsValueType ? Activator.CreateInstance(parameterAttribute.Type) : null));
				NavigateWithDataContext<TPage>(datacontext);
			}
			else
			{
				var datacontext = _kernel.Get(GetDataContextViewModelType<TPage>());
				NavigateWithDataContext<TPage>(datacontext);
			}
		}

		public void NavigateTo<TPage>(object @params) where TPage : Page, new()
		{
			if (@params == null)
			{
				NavigateTo<TPage>();
			}

			var type = GetDataContextViewModelType<TPage>();

			var parameterAttribute = type
				.GetCustomAttributes(typeof(ViewModelParameter), false)
				.Cast<ViewModelParameter>()
				.FirstOrDefault();

			if (parameterAttribute == null)
			{
				var datacontext = _kernel.Get(GetDataContextViewModelType<TPage>(), new ConstructorArgument("params", @params));
				NavigateWithDataContext<TPage>(datacontext);
			}
			else if (parameterAttribute.Type != @params.GetType())
			{
				throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, "The parameter is not of the type specified in attribute {0} {1}", type, @params.GetType()));
			}
			else
			{
				var datacontext = _kernel.Get(type, new ConstructorArgument(parameterAttribute.Name, @params));
				NavigateWithDataContext<TPage>(datacontext);
			}
		}

		public IBindingToSyntax<T> Bind<T>()
		{
			return _kernel.Bind<T>();
		}

		public IBindingToSyntax<T1, T2> Bind<T1, T2>()
		{
			return _kernel.Bind<T1, T2>();
		}

		public IBindingToSyntax<T1, T2, T3> Bind<T1, T2, T3>()
		{
			return _kernel.Bind<T1, T2, T3>();
		}

		public IBindingToSyntax<T1, T2, T3, T4> Bind<T1, T2, T3, T4>()
		{
			return _kernel.Bind<T1, T2, T3, T4>();
		}

		public IBindingToSyntax<object> Bind(params Type[] services)
		{
			return _kernel.Bind(services);
		}

		public void Unbind<T>()
		{
			_kernel.Unbind<T>();
		}

		public void Unbind(Type service)
		{
			_kernel.Unbind(service);
		}

		public IBindingToSyntax<T1> Rebind<T1>()
		{
			return _kernel.Rebind<T1>();
		}

		public IBindingToSyntax<T1, T2> Rebind<T1, T2>()
		{
			return _kernel.Rebind<T1, T2>();
		}

		public IBindingToSyntax<T1, T2, T3> Rebind<T1, T2, T3>()
		{
			return _kernel.Rebind<T1, T2, T3>();
		}

		public IBindingToSyntax<T1, T2, T3, T4> Rebind<T1, T2, T3, T4>()
		{
			return _kernel.Rebind<T1, T2, T3, T4>();
		}

		public IBindingToSyntax<object> Rebind(params Type[] services)
		{
			return _kernel.Rebind(services);
		}

		public void AddBinding(IBinding binding)
		{
			_kernel.AddBinding(binding);
		}

		public void RemoveBinding(IBinding binding)
		{
			_kernel.RemoveBinding(binding);
		}
	}
}