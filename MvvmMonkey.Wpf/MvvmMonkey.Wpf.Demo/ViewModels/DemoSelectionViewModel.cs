using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey.Wpf.Demo.ViewModels
{
    [TypeDescriptionProvider(typeof(MethodBinding))]
    public class DemoSelectionViewModel : INotifyPropertyChanged
    {
        public IList<DemoItemViewModel> Demos { get; private set; }

        public DemoSelectionViewModel()
        {
            Demos = GetType().
                Assembly.
                GetTypes().
                Select(o => new { Type = o, NameAttr = (DisplayNameAttribute)o.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() }).
                Where(o => o.NameAttr != null).
                Select(o => new DemoItemViewModel(this, o.NameAttr.DisplayName, o.Type)).
                ToList();
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
