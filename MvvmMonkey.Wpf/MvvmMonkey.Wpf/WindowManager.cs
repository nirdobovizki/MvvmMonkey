using NirDobovizki.MvvmMonkey.Advanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey
{
    public class WindowManager
    {
        private static IWindowManagerImpl _impl = new MultiWinWindowManagerImpl();

        public static void SetImplementation(IWindowManagerImpl impl)
        {
            _impl = impl;
        }

        public static void OpenNonModal(object viewModel, bool useViewModelAsContent = false)
        {
            _impl.OpenNonModal(viewModel, useViewModelAsContent);
        }

        public static bool? OpenDialog(object viewModel, bool useViewModelAsContent = false)
        {
            return _impl.OpenDialog(viewModel, useViewModelAsContent);
        }

        public static void Close(object viewModel, bool? result = null)
        {
            _impl.Close(viewModel, result);
        }

    }
}
