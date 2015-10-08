using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NirDobovizki.MvvmMonkey.Advanced
{
    public class SimpleDelegateCommand : ICommand
    {
        private Action<object> _target;

        public SimpleDelegateCommand(Action<object> target)
        {
            _target = target;
        }

#pragma warning disable 0067 // the CanExecuteChanged event is never used
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _target(parameter);
        }
    }
}
