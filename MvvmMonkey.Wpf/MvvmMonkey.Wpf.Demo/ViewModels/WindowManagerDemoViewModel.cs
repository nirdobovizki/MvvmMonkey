using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey.Wpf.Demo.ViewModels
{
    [DisplayName("Winodws manager")]
    [TypeDescriptionProvider(typeof(MethodBinding))]
    class WindowManagerDemoViewModel
    {
        public void OpenDialog()
        {
            WindowManager.OpenDialog(new WindowManagerChildViewModel());
        }

        public void OpenNonModal()
        {
            WindowManager.OpenNonModal(new WindowManagerChildViewModel());
        }
    }
}
