using NirDobovizki.MvvmMonkey.Advanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NirDobovizki.MvvmMonkey.Commands
{
    public class AsyncDelegateCommand : CommandBase
    {
        private Func<object, Task> _execute;
        private Func<object, bool> _canExecute;

        public AsyncDelegateCommand(Func<object, Task> target, Func<object, bool> canExecute = null)
        {
            _execute = target;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            if (InWork) return false;
            if (_canExecute == null) return true;
            return _canExecute(parameter);
        }

        public override async void Execute(object parameter)
        {
            if (InWork) return;
            InWork = true;
            try
            {
                await _execute(parameter);
            }
            finally
            {
                InWork = false;
            }
        }

        private bool _inWork;
        private bool InWork
        {
            get { return _inWork; }
            set
            {
                _inWork = value;
                RaiseCanExecuteChanged();
            }
        }
    }

    public class AsyncDelegateCommand<T> : AsyncDelegateCommand
    {
        public AsyncDelegateCommand(Func<T, Task> execute, Func<T, bool> canExecute) :
            base(o => execute((T)o), canExecute == null ? (Func<object, bool>)null : o => canExecute((T)o))
        {

        }
    }

}
