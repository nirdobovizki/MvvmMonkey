using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NirDobovizki.MvvmMonkey
{
    public class PasswordBinding
    {
        public static readonly DependencyProperty IsPasswordBindingEnabledProperty = DependencyProperty.RegisterAttached("IsPasswordBindingEnabled", typeof(bool), typeof(PasswordBinding), new PropertyMetadata(IsPasswordBindingEnabledChanged));

        public static bool GetIsPasswordBindingEnabled(PasswordBox obj)
        {
            return (bool)obj.GetValue(IsPasswordBindingEnabledProperty);
        }

        public static void SetIsPasswordBindingEnabled(PasswordBox obj, bool value)
        {
            obj.SetValue(IsPasswordBindingEnabledProperty, value);
        }

        private static void IsPasswordBindingEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as PasswordBox;
            if (d == null) return;

            if (!(bool)e.OldValue && (bool)e.NewValue)
            {
                control.PasswordChanged += control_PasswordChanged;
            }
            else if ((bool)e.OldValue && !(bool)e.NewValue)
            {
                control.PasswordChanged -= control_PasswordChanged;
            }
        }

        private static void control_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var control = sender as PasswordBox;
            if (control == null) return;

            SetPassword(control, ConvertToSecureString(control.Password));
        }

        private static SecureString ConvertToSecureString(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            var securePassword = new SecureString();
            foreach (char c in password)
                securePassword.AppendChar(c);
            securePassword.MakeReadOnly();
            return securePassword;
        }

        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("Password", typeof(SecureString), typeof(PasswordBinding), new FrameworkPropertyMetadata(null,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,PasswordChanged));

        public static SecureString GetPassword(PasswordBox obj)
        {
            return (SecureString)obj.GetValue(PasswordProperty);
        }
        public static void SetPassword(PasswordBox obj, SecureString value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        private static void PasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

    }
}
