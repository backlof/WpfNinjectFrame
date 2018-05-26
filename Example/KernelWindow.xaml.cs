using System.Windows;
using TestProject.Page;
using TestProject.Proof;

namespace TestProject
{
	public partial class KernelWindow : System.Windows.Window
	{
		public KernelWindow()
		{
			InitializeComponent();

			#region Dependencies

			Frame.Bind<IProofGenerator>().To<ProofGenerator>();

			#endregion Dependencies
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Frame.NavigateTo<ExamplePageWithoutParameters>();
		}
	}
}