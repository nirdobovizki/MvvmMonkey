using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey.Wpf.Demo.ViewModels
{
    [TypeDescriptionProvider(typeof(MethodBinding))]
    class DemoSelectionViewModel : INotifyPropertyChanged
    {
        public void MethodBindingDemo()
        {
            Child = new MethodBindingViewModel();
        }

        public void MethodBindingTaskDemo()
        {
            Child = new MethodBindingTaskViewModel();
        }

        public void ViewLocatorDemo()
        {
            Child = new ViewLocatorViewModel();
        }

        public void NotifyDemo()
        {
            Child = new NotifyViewModel();
        }

        public void PasswordDemo()
        {
            Child = new PasswordViewModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private object _child;
        public object Child
        {
            get { return _child; }
            set
            {
                if(_child!=value)
                {
                    _child = value;
                    PropertyChange.Notify(this, PropertyChanged);
                }
            }
        }
    }
}
