using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey.Advanced
{
    public interface IWindowManagerImpl
    {
        void OpenNonModal(object viewModel, bool useViewModelAsContent = false);
        bool? OpenDialog(object viewModel, bool useViewModelAsContent = false, bool setMainWindowAsOwner = true);
        void Close(object viewModel, bool? result);
    }
}
