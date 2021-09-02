using System;
using System.Windows.Input;

namespace ctcontrol
{
    public class Command : ICommand
    {
        private readonly Action<object> _action = null;
        private bool _canExecute = false;

        public Command(Action<object> action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute
        {
            get => _canExecute;
            set
            {
                if (_canExecute != value)
                {
                    _canExecute = value;
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
