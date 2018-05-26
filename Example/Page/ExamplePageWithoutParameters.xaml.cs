using NinjectFrame;
using TestProject.ViewModel;

namespace TestProject.Page
{
	[DataContextViewModel(typeof(ExampleViewModelWithoutParameters))]
	public partial class ExamplePageWithoutParameters : System.Windows.Controls.Page
	{
		public ExamplePageWithoutParameters()
		{
			InitializeComponent();
		}
	}
}