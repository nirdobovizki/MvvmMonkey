using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey.Wpf.Demo.ViewModels
{
    [TypeDescriptionProvider(typeof(MethodBinding))]
    [DisplayName("Method Binding")]
    public class MethodBindingViewModel 
    {
        public void Fire()
        {
            System.Windows.MessageBox.Show("yey");
        }
    }
}
