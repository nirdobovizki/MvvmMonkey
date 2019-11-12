using NirDobovizki.MvvmMonkey.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
            Func<bool> canExecuteDelegate = null;
            if(_canExecuteMethodInfo != null)
            {
                var get = _canExecuteMethodInfo.GetAccessors(false).Where(mi => mi.Name.StartsWith("get_")).ToArray();
                if (get.Length == 1)
                {
                    canExecuteDelegate = (Func<bool>)get[0].CreateDelegate(typeof(Func<bool>), component);
                }
            }

            CommandBase command;
            var executeMethodParams = _executeMethodInfo.GetParameters();
            if (_executeMethodInfo.ReturnType == typeof(Task))
            {
                if (executeMethodParams.Length == 0)
                {
                    var d = (Func<Task>)_executeMethodInfo.CreateDelegate(typeof(Func<Task>), component);
                    command = new AsyncDelegateCommand(_ => d(), canExecuteDelegate);
                }
                else if (executeMethodParams.Length == 1)
                {
                    var delegateType = typeof(Func<,>).MakeGenericType(executeMethodParams[0].ParameterType, typeof(Task));
                    var d = _executeMethodInfo.CreateDelegate(delegateType, component);
                    command = (CommandBase)typeof(AsyncDelegateCommand<>).
                        MakeGenericType(executeMethodParams[0].ParameterType).
                        GetConstructor(new Type[] { delegateType, typeof(Func<bool>) }).
                        Invoke(new object[] { d, canExecuteDelegate });
                }
                else
                {
                    Debug.WriteLine($"MvvmMonkey: method {_executeMethodInfo.DeclaringType.Name}.{_executeMethodInfo.Name} must take zero or one parameters to be used as a command");
                    return null;
                }
            }
            else if (_executeMethodInfo.ReturnType == typeof(void))
            {
                if (executeMethodParams.Length == 0)
                {
                    var d = (Action)_executeMethodInfo.CreateDelegate(typeof(Action), component);
                    command = new DelegateCommand(_ => d(), canExecuteDelegate);
                }
                else if (executeMethodParams.Length == 1)
                {
                    var delegateType = typeof(Action<>).MakeGenericType(executeMethodParams[0].ParameterType);
                    var d = _executeMethodInfo.CreateDelegate(delegateType, component);
                    command = (CommandBase)typeof(DelegateCommand<>).
                        MakeGenericType(executeMethodParams[0].ParameterType).
                        GetConstructor(new Type[] { delegateType, typeof(Func<bool>) }).
                        Invoke(new object[] { d, canExecuteDelegate });
                }
                else
                {
                    Debug.WriteLine($"MvvmMonkey: method {_executeMethodInfo.DeclaringType.Name}.{_executeMethodInfo.Name} must take zero or one parameters to be used as a command");
                    return null;
                }
            }
            else
            {
                Debug.WriteLine($"MvvmMonkey: method {_executeMethodInfo.DeclaringType.Name}.{_executeMethodInfo.Name} must return void or Task to be used as a command");
                return null;
            }
            if (canExecuteDelegate!=null && _canExecuteMethodInfo!=null)
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
