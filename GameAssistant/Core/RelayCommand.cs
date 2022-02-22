using System;
using System.Windows.Input;

namespace GameAssistant.Core
{
    internal class RelayCommand : ICommand
    {
        private Action<object> execute { get; set; }
        private Func<object, bool> canExecute { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parametr)
        {
            return this.canExecute == null || this.canExecute(parametr);
        }

        public void Execute(object parametr)
        {
            this.execute(parametr);
        }

    }
}
