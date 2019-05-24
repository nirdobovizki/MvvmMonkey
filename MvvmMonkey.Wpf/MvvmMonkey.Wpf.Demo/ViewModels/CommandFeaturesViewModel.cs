using NirDobovizki.MvvmMonkey.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NirDobovizki.MvvmMonkey.Wpf.Demo.ViewModels
{
    [DisplayName("Command Features")]
    public class CommandFeaturesViewModel : INotifyPropertyChanged
    {
        private bool _enabled = true;

        public bool Enabled
        {
            get { return _enabled; }
            set { PropertyChange.Set(this, ref _enabled, value, PropertyChanged); }
        }

        private DelegateCommand _enableCommand;
        public DelegateCommand EnableCommand
        {
            get
            {
                return _enableCommand ?? (_enableCommand = (DelegateCommand)(new DelegateCommand(_ => Enabled = true, _ => !Enabled)
                    .RaiseCanExecuteChangedOn(()=>Enabled)));
            }
        }

        private DelegateCommand _disableCommand;
        public DelegateCommand DisableCommand
        {
            get
            {
                return _disableCommand ?? (_disableCommand = (DelegateCommand)new DelegateCommand(_ => Enabled = false, _ => Enabled)
                    .RaiseCanExecuteChangedOn(() => Enabled));
            }
        }

        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = (DelegateCommand)new DelegateCommand(_ =>
                    {
                        EnableCommand.RaiseCanExecuteChanged();
                        DisableCommand.RaiseCanExecuteChanged();
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
