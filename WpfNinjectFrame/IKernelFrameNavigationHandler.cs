using System.Windows.Controls;

namespace NinjectFrame
{
	public interface IKernelFrameNavigationHandler
	{
		void NavigateTo<TPage>(string name, object @param) where TPage : Page, new();

		void NavigateTo<TPage>() where TPage : Page, new();

		void NavigateTo<TPage>(object @params) where TPage : Page, new();

		bool CanGoForward { get; }
		bool CanGoBack { get; }

		void GoBack();

		void GoForward();

		void RemoveBackEntry();
	}
}