using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey.Wpf.Demo.ViewModels
{
    [TypeDescriptionProvider(typeof(MethodBinding))]
    class WindowManagerChildViewModel
    {
        public void Close()
        {
            WindowManager.Close(this, true);
        }

        public string WindowTitle { get { return "Window Manager Demo"; } }
    }
}
