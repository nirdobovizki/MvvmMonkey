using NirDobovizki.MvvmMonkey.Advanced;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey
{
    public class MethodBinding : TypeDescriptionProvider
    {
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            return new MethodBindingTypeDescriptor(objectType, instance);
        }
    }
}
