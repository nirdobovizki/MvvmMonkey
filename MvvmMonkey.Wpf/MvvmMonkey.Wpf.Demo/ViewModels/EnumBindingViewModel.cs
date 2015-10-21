using NirDobovizki.MvvmMonkey.Wpf.Demo.SampleModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey.Wpf.Demo.ViewModels
{
    [DisplayName("Enum Binding")]
    class EnumBindingViewModel : INotifyPropertyChanged
    {
        private AnEnum _theValue;
        public AnEnum TheValue
        {
            get { return _theValue; }
            set
            {
                if (_theValue != value)
                {
                    _theValue = value;
                    PropertyChange.Notify(this, PropertyChanged);
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
