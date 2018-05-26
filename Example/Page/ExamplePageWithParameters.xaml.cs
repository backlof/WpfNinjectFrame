using NinjectFrame;
using TestProject.ViewModel;

namespace TestProject.Page
{
	[DataContextViewModel(typeof(ExampleViewModelWithParameters))]
	public partial class ExamplePageWithParameters : System.Windows.Controls.Page
	{
		public ExamplePageWithParameters()
		{
			InitializeComponent();
		}
	}
}