using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey.Advanced
{
    internal class SimplePropertyDescriptor : PropertyDescriptor
    {
        private Type _objectType;
        private PropertyInfo _propertyInfo;

        public SimplePropertyDescriptor(Type objectType, PropertyInfo propertyInfo): base(propertyInfo.Name,new Attribute[0])
        {
            _objectType = objectType;
            _propertyInfo = propertyInfo;
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
                return !_propertyInfo.CanWrite;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return _propertyInfo.PropertyType;
            }
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override object GetValue(object component)
        {
            return _propertyInfo.GetValue(component);
        }

        public override void ResetValue(object component)
        {
            throw new NotSupportedException();
        }

        public override void SetValue(object component, object value)
        {
            _propertyInfo.SetValue(component, value);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }
    }
}
