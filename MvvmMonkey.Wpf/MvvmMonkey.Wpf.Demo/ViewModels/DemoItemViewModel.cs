using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NirDobovizki.MvvmMonkey.Wpf.Demo.ViewModels
{
    [TypeDescriptionProvider(typeof(MethodBinding))]
    public class DemoItemViewModel
    {
        DemoSelectionViewModel _owner;
        string _name;
        Type _viewModelType;

        public DemoItemViewModel(DemoSelectionViewModel owner, string name, Type viewModelType)
        {
            _owner = owner;
            _name = name;
            _viewModelType = viewModelType;
        }

        public string Name
        {
            get { return _name; }
        }

        public void ChooseThis()
        {
            _owner.Child = Activator.CreateInstance(_viewModelType);
        }
    }
}
