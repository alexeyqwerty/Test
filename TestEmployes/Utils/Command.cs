using System;
using System.Windows.Input;

namespace TestEmployes.Utils
{
    public class Command : ICommand
    {
        private readonly Action _action;
        private readonly Func<bool> _canAction;
        private readonly Action<object> _actionParameterized;
        private readonly Func<object, bool> _canActionParameterized;

        public Command(Action action, Func<bool> canAction)
        {
            _action = action;
            _canAction = canAction;
        }

        public Command(Action<object> action, Func<object, bool> canAction)
        {
            _actionParameterized = action;
            _canActionParameterized = canAction;
        }

        #region ICommand Members

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (_action != null)
            {
                _action();
            }
            else
            {
                if (parameter != null)
                {
                    _actionParameterized(parameter);
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_canAction != null)
            {
                return _canAction();
            }
            else
            {
                if (parameter != null)
                {
                    return _canActionParameterized(parameter);
                }
            }

            return false;
        }

        #endregion

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, null);  // raise event
        }
    }
}
