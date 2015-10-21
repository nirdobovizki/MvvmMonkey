using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey.Wpf.Demo.ViewModels
{
    [DisplayName("Password Binding")]
    public class PasswordViewModel : INotifyPropertyChanged
    {
        private SecureString _password;
        public SecureString Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChange.Notify(this, PropertyChanged);

                    PlainText = ConvertToUnsecureString(_password);
                }
            }
        }

        private string _plainText;
        public string PlainText
        {
            get { return _plainText; }
            set
            {
                if (_plainText != value)
                {
                    _plainText = value;
                    PropertyChange.Notify(this, PropertyChanged);
                }
            }
        }

        private static string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
                throw new ArgumentNullException("securePassword");

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
