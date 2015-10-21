using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NirDobovizki.MvvmMonkey.Advanced
{
    public class MultiWinWindowManagerImpl : IWindowManagerImpl
    {
        public Type WindowType = typeof(MultiWinWindowManagerDefaultWindow);

        public void Close(object viewModel, bool? result = default(bool?))
        {
            var wnd = Application.Current.Windows.Cast<Window>().FirstOrDefault(o=>o.DataContext == viewModel);
            if(wnd!=null)
            {
                if(result != null)
                {
                    try
                    {
                        wnd.DialogResult = result;
                    }
                    catch(InvalidOperationException)
                    {
                        wnd.Close();
                    }
                }
                else
                {
                    wnd.Close();
                }
            }
        }

        public bool? OpenDialog(object viewModel)
        {
            var win = (Window)Activator.CreateInstance(WindowType);
            win.DataContext = viewModel;
            win.ShowInTaskbar = Application.Current.Windows.Count == 0;
            return win.ShowDialog();
        }

        public void OpenNonModal(object viewModel)
        {
            var win = (Window)Activator.CreateInstance(WindowType);
            win.DataContext = viewModel;
            win.Show();
        }
    }
}
