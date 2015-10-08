using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey.Wpf.Demo.ViewModels
{
    [TypeDescriptionProvider(typeof(MethodBinding))]
    class MethodBindingTaskViewModel
    {
        public Task CallDelay()
        {
            return Task.Delay(2000);
        }
    }
}
