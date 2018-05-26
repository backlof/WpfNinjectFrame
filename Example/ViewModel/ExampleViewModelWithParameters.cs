using NinjectFrame;
using System.Windows.Input;
using TestProject.Binding;
using TestProject.Proof;
using TestProject.View;

namespace TestProject.ViewModel
{
	public class PageParameters
	{
		public int Id { get; set; }
	}

	[ViewModelParameter("params", typeof(PageParameters))]
	public class ExampleViewModelWithParameters : ViewModelBase
	{
		private readonly IKernelFrameNavigationHandler _navigationHandler;
		private readonly IProofGenerator _proofGenerator;
		private string _dataContextTest;

		public string DataContextTest
		{
			get => _dataContextTest;
			set
			{
				_dataContextTest = value;
				OnPropertyChanged();
			}
		}

		public ExampleViewModelWithParameters(PageParameters @params, IKernelFrameNavigationHandler navigationHandler, IProofGenerator proofGenerator)
		{
			_navigationHandler = navigationHandler;
			_proofGenerator = proofGenerator;
			DataContextTest = $"Proof that datacontext is kept: {_proofGenerator.GetRandomString()}";
		}

		public ICommand NavigateToParameterlessPage => new AnonymousRelayCommand
		{
			ToExecute = () =>
			{
				_navigationHandler.NavigateTo<ExamplePageWithoutParameters>();
			}
		};
	}
}