using System;
using System.Windows.Input;

namespace VirtualWallet.Controls
{
    public class CommandHandler : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action action;
        private Func<bool> canExecute;

        public CommandHandler(Action action, Func<bool> canExecute = null)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        public void Execute(object parameter)
        {
            this.action();
        }
    }
}
