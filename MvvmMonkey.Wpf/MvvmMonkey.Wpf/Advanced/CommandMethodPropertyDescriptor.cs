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
        private MethodInfo _methodInfo;

        public CommandMethodPropertyDescriptor(Type objectType, MethodInfo methodInfo) : base(methodInfo.Name, new Attribute[0])
        {
            _objectType = objectType;
            _methodInfo = methodInfo;
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
            if(_methodInfo.ReturnType == typeof(Task))
            {
                if (_methodInfo.GetParameters().Length == 0)
                {
                    var d = (Func<Task>)_methodInfo.CreateDelegate(typeof(Func<Task>), component);
                    return new TaskDelegateCommand(_ => d());
                }
                else
                {
                    var d = (Func<object, Task>)_methodInfo.CreateDelegate(typeof(Func<object, Task>), component);
                    return new TaskDelegateCommand(d);
                }
            }
            else
            {
                if (_methodInfo.GetParameters().Length == 0)
                {
                    var d = (Action)_methodInfo.CreateDelegate(typeof(Action), component);
                    return new SimpleDelegateCommand(_ => d());
                }
                else
                {
                    var d = (Action<object>)_methodInfo.CreateDelegate(typeof(Action<object>), component);
                    return new SimpleDelegateCommand(d);
                }
            }
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
