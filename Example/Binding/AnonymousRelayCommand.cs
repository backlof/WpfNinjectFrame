using System;
using System.Windows.Input;

namespace TestProject.Binding
{
	public class AnonymousRelayCommand : ICommand
	{
		public Action ToExecute { get; set; }
		public Func<bool> CanExecuteWhen { get; set; }

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter)
		{
			return CanExecuteWhen == null || CanExecuteWhen();
		}

		public void Execute(object parameter)
		{
			ToExecute?.Invoke();
		}
	}
}