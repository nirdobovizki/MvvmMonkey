using NirDobovizki.MvvmMonkey.Advanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NirDobovizki.MvvmMonkey.Commands
{
    public class DelegateCommand : CommandBase
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public DelegateCommand(Action<object> execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute != null ? (Func<object, bool>)(_ => canExecute()) : null;
        }

        public override bool CanExecute(object parameter)
        {
            if(_canExecute == null) return true;
            return _canExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            _execute(parameter);
        }
    }

    public class DelegateCommand<T> : DelegateCommand
    {
        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute) :
            base(o => execute((T)o), canExecute == null ? (Func<object, bool>)null : o => canExecute((T)o))
        {

        }
        public DelegateCommand(Action<T> execute, Func<bool> canExecute) :
            base(o => execute((T)o), canExecute == null ? (Func<object, bool>)null : _ => canExecute())
        {

        }
    }

}
