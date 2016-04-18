using System;
using System.Windows.Input;

namespace VirtualWallet.Controls
{
    public class CommandHandler : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action action;

        public CommandHandler(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.action();
        }
    }
}
