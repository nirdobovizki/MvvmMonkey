using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey.Wpf.Demo.ViewModels
{
    [TypeDescriptionProvider(typeof(MethodBinding))]
    [DisplayName("Method Binding")]
    public class MethodBindingViewModel : INotifyPropertyChanged
    {
        public void Fire()
        {
            System.Windows.MessageBox.Show("yey");
        }

        public void DoSomething()
        {
            System.Windows.MessageBox.Show("did something");
        }

        private bool _canDoSomething = true;
        public bool CanDoSomething
        {
            get { return _canDoSomething; }
            set { PropertyChange.Set(this, ref _canDoSomething, value, PropertyChanged); }
        }

        public void DoSomethingParam(string p)
        {
            System.Windows.MessageBox.Show(p);
        }

        private bool _canDoSomethingParam = true;
        public bool CanDoSomethingParam
        {
            get { return _canDoSomethingParam; }
            set { PropertyChange.Set(this, ref _canDoSomethingParam, value, PropertyChanged); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
