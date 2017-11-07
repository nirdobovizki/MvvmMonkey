using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NirDobovizki.MvvmMonkey.Advanced
{
    public abstract class CommandBase : ICommand
    {
        private SynchronizationContext _creatorThread;

        public CommandBase()
        {
            _creatorThread = SynchronizationContext.Current;
        }

        public void RaiseCanExecuteChanged()
        {
            if(_creatorThread == null)
            {
                OnCanExecuteChanged();
            }
            else if(SynchronizationContext.Current == _creatorThread)
            {
                OnCanExecuteChanged();
            }
            else
            {
                _creatorThread.Post(_ => OnCanExecuteChanged(), null);
            }
        }

        public CommandBase RaiseCanExecuteChangedOn(INotifyPropertyChanged obj, string propertyName)
        {
            obj.PropertyChanged += (_, ea) =>
                {
                    if (ea.PropertyName == null || ea.PropertyName == propertyName)
                        RaiseCanExecuteChanged();
                };
            return this;
        }

        public CommandBase RaiseCanExecuteChangedOn<T>(Expression<Func<T>> property)
        {
            var exp = property.Body as MemberExpression;
            var propName = exp.Member.Name;
            var obj = ((ConstantExpression)exp.Expression).Value;
            return RaiseCanExecuteChangedOn((INotifyPropertyChanged)obj,propName);
        }

        protected virtual void OnCanExecuteChanged()
        {
            var eh = CanExecuteChanged;
            if (eh != null) eh(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);

    }
}
