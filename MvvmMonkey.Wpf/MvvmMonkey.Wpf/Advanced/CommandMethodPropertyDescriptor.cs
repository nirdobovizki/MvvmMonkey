using NirDobovizki.MvvmMonkey.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NirDobovizki.MvvmMonkey.Advanced
{
    internal class CommandMethodPropertyDescriptor : PropertyDescriptor
    {
        private Type _objectType;
        private MethodInfo _executeMethodInfo;
        private PropertyInfo _canExecuteMethodInfo;

        public CommandMethodPropertyDescriptor(Type objectType, MethodInfo executeMethodInfo, PropertyInfo canExecuteMethodInfo) : base(executeMethodInfo.Name, new Attribute[0])
        {
            _objectType = objectType;
            _executeMethodInfo = executeMethodInfo;
            _canExecuteMethodInfo = canExecuteMethodInfo;
        }

        public override Type ComponentType
        {
            get
            {
                return _objectType;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return typeof(ICommand);
            }
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override object GetValue(object component)
        {
            Func<object, bool> canExecuteDelegate = null;
            if(_canExecuteMethodInfo != null)
            {
                var get = _canExecuteMethodInfo.GetAccessors(false);
                if (get != null && get.Length == 1)
                {
                    var d = (Func<bool>)get[0].CreateDelegate(typeof(Func<bool>), component);
                    canExecuteDelegate = _ => d();
                }
                /*if (_canExecuteMethodInfo.GetParameters().Length == 0)
                {
                    var d = (Func<bool>)_canExecuteMethodInfo.CreateDelegate(typeof(Func<bool>), component);
                    canExecuteDelegate = _=>d();
                }
                else
                {
                    canExecuteDelegate = (Func<object, bool>)_canExecuteMethodInfo.CreateDelegate(typeof(Func<object, bool>), component);
                }*/
            }

            CommandBase command;
            if (_executeMethodInfo.ReturnType == typeof(Task))
            {
                if (_executeMethodInfo.GetParameters().Length == 0)
                {
                    var d = (Func<Task>)_executeMethodInfo.CreateDelegate(typeof(Func<Task>), component);
                    command = new AsyncDelegateCommand(_ => d(), canExecuteDelegate);
                }
                else
                {
                    var d = (Func<object, Task>)_executeMethodInfo.CreateDelegate(typeof(Func<object, Task>), component);
                    command = new AsyncDelegateCommand(d, canExecuteDelegate);
                }
            }
            else
            {
                if (_executeMethodInfo.GetParameters().Length == 0)
                {
                    var d = (Action)_executeMethodInfo.CreateDelegate(typeof(Action), component);
                    command = new DelegateCommand(_ => d(), canExecuteDelegate);
                }
                else
                {
                    var d = (Action<object>)_executeMethodInfo.CreateDelegate(typeof(Action<object>), component);
                    command =  new DelegateCommand(d, canExecuteDelegate);
                }
            }
            if(canExecuteDelegate!=null && _canExecuteMethodInfo!=null)
            {
                var inpc = component as INotifyPropertyChanged;
                if (inpc != null)
                {
                    command.RaiseCanExecuteChangedOn(inpc, _canExecuteMethodInfo.Name);
                }
            }
            return command;
        }

        public override void ResetValue(object component)
        {
            throw new NotSupportedException();
        }

        public override void SetValue(object component, object value)
        {
            throw new NotSupportedException();
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
    }
}
