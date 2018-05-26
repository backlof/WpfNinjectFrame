using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestProject.Proof;
using TestProject.View;

namespace TestProject
{
	public partial class Window : System.Windows.Window
	{
		public Window()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			#region Dependencies

			Frame.Bind<IProofGenerator>().To<ProofGenerator>();

			#endregion Dependencies

			Frame.NavigateTo<ExamplePageWithoutParameters>();
		}
	}
}