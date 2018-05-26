using System;
using System.Windows.Input;

namespace TestProject.Binding
{
	public class GenericRelayCommand<T> : ICommand
	{
		public Action<T> ToExecute { get; set; }
		public Func<T, bool> CanExecuteWhen { get; set; }

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter)
		{
			return CanExecuteWhen == null || CanExecuteWhen((T)parameter);
		}

		public void Execute(object parameter)
		{
			ToExecute?.Invoke((T)parameter);
		}
	}
}