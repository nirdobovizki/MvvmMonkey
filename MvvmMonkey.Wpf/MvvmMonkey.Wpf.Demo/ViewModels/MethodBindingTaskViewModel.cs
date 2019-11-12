using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey.Wpf.Demo.ViewModels
{
    [TypeDescriptionProvider(typeof(MethodBinding))]
    [DisplayName("Method Binding with Tasks")]
    class MethodBindingTaskViewModel : INotifyPropertyChanged
    {
        public Task CallDelay()
        {
            return Task.Delay(2000);
        }

        public async Task DoSomethingParam(string p)
        {
            await CallDelay();
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
