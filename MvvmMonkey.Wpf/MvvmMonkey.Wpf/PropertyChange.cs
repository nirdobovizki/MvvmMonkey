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
    }
}
