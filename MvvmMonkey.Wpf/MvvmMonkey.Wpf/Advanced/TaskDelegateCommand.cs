using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NirDobovizki.MvvmMonkey.Advanced
{
    public class TaskDelegateCommand : ICommand
    {
        private Func<object, Task> _target;

        public TaskDelegateCommand(Func<object, Task> target)
        {
            _target = target;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !InWork;
        }

        public async void Execute(object parameter)
        {
            if (InWork) return;
            InWork = true;
            try
            {
                await _target(parameter);
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
                var eh = CanExecuteChanged;
                if (eh != null) eh(this, EventArgs.Empty);
            }
        }


    }
}
