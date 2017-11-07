using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey.Advanced
{
    internal class MethodBindingTypeDescriptor : ICustomTypeDescriptor
    {
        private static Dictionary<Type, PropertyDescriptorCollection> _propertiesCache = new Dictionary<Type, PropertyDescriptorCollection>();
        private static object _propertiesCacheLock = new object();
        private object _instance;
        private Type _objectType;

        public MethodBindingTypeDescriptor(Type objectType, object instance)
        {
            _objectType = objectType;
            _instance = instance;
        }


        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return new AttributeCollection();
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return _objectType.Name;
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return _objectType.Name;
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return null;
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return null;
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return null;
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return null;
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return new EventDescriptorCollection(new EventDescriptor[0]);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return new EventDescriptorCollection(new EventDescriptor[0]);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            PropertyDescriptorCollection result;
            lock (_propertiesCacheLock)
            {
                if(_propertiesCache.TryGetValue(_objectType, out result))
                {
                    return result;
                }
            }

            result = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
            foreach(var currentProp in _objectType.GetProperties())
            {
                result.Add(new SimplePropertyDescriptor(_objectType, currentProp));
            }
            foreach(var currentMethod in _objectType.GetMethods())
            {
                PropertyInfo canExecuteProperty = null;
                if (currentMethod.GetParameters().Length<=1 &&
                    (currentMethod.ReturnType == typeof(void)||
                    currentMethod.ReturnType == typeof(Task)))
                {
                    try
                    {
                        canExecuteProperty = _objectType.GetProperty("Can" + currentMethod.Name);
                        if( canExecuteProperty!=null &&
                            canExecuteProperty.PropertyType != typeof(bool) )
                        {
                            canExecuteProperty = null;
                        }
                    }
                    catch(Exception ex)
                    {
                        canExecuteProperty = null;
                        System.Diagnostics.Debug.WriteLine("MvvmMonkey.MethodBinding: exception when trying to fine can execute method for " + currentMethod.Name + ":" + ex.ToString());
                    }
                    result.Add(new CommandMethodPropertyDescriptor(_objectType, currentMethod, canExecuteProperty));
                }
            }

            lock(_propertiesCacheLock)
            {
                if(!_propertiesCache.ContainsKey(_objectType))
                {
                    _propertiesCache.Add(_objectType, result);
                }
            }
            return result;
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
        {
            return ((ICustomTypeDescriptor)this).GetProperties();
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return _instance;
        }
    }
}
