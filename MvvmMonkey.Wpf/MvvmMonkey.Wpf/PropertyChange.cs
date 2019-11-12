using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey
{
    public class PropertyChange
    {
        public static void Notify(object caller, PropertyChangedEventHandler handler, [CallerMemberName] string propertyName="")
        {
            if(handler!=null)
            {
                handler(caller, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static void Set<T>(object caller, ref T field, T value, PropertyChangedEventHandler handler, [CallerMemberName] string propertyName = "") 
        {
            if(typeof(T).IsClass)
            {
                if(!object.ReferenceEquals(field,value))
                {
                    field = value;
                    handler?.Invoke(caller, new PropertyChangedEventArgs(propertyName));
                }
            }
            else
            {
                if(!field.Equals(value))
                {
                    field = value;
                    handler?.Invoke(caller, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
    }
}
