using System;
using System.Windows.Input;

namespace AssemblyBrowser
{
    class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private Func<bool> canExecuteAction;

        private Action<T> executeAction;

        public RelayCommand(Action<T> executeAction, Func<bool> canExecuteAction) 
        {
            if (executeAction == null)
                throw new ArgumentNullException(nameof(executeAction));
            this.executeAction = executeAction;
            this.canExecuteAction = canExecuteAction;
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteAction == null || canExecuteAction();
        }

        public void Execute(object parameter)
        {
            this.executeAction((T)parameter);
        }
    }
}
