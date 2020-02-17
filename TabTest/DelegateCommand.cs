using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TabTest
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecuteMethod;
        private readonly Action<T> _executeMethod;
        private bool _isExecuting;

        public DelegateCommand(Action<T> executeMethod)
            : this(executeMethod, null)
        {
        }

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            if ((executeMethod == null) && (canExecuteMethod == null))
            {
                throw new ArgumentNullException("executeMethod", @"Execute Method cannot be null");
            }
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        bool ICommand.CanExecute(object parameter)
        {
            T parm = parameter == null ? default(T) : (T)parameter;
            return !_isExecuting && CanExecute(parm);
        }

        void ICommand.Execute(object parameter)
        {
            _isExecuting = true;
            try
            {
                RaiseCanExecuteChanged();
                T parm = parameter == null ? default(T) : (T)parameter;
                Execute(parm);
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public bool CanExecute(T parameter)
        {
            if (_canExecuteMethod == null)
                return true;

            return _canExecuteMethod(parameter);
        }

        public void Execute(T parameter)
        {
            _executeMethod(parameter);
        }
    }
}
