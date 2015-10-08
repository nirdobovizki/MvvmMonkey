using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NirDobovizki.MvvmMonkey
{
    public class ViewLocator : Decorator
    {
        private static Dictionary<Type, Type> _viewByViewModel = new Dictionary<Type, Type>();

        public ViewLocator()
        {
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            Child = null;
            if (e.NewValue == null)
            {
                return;
            }
            Type viewModelType = e.NewValue.GetType();
            Type viewType;
            if(!_viewByViewModel.TryGetValue(viewModelType, out viewType))
            {
                var viewModelName = viewModelType.Name;
                if (!viewModelName.EndsWith("ViewModel", StringComparison.Ordinal)) throw new InvalidOperationException("View model type name must contain ViewModel to use ViewLocator");
                var viewName = viewModelName.Substring(0, viewModelName.Length - 5); // "ViewModel" -> "View"
                viewType = viewModelType.Assembly.GetTypes().SingleOrDefault(x => string.CompareOrdinal(x.Name, viewName)==0);
                if(viewType == null)
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("MvvmMonkey.ViewLocator: can't find view class {0} for view model {1}", viewName, e.NewValue.ToString()));
                    return;
                }
                _viewByViewModel.Add(viewModelType, viewType);
            }
            Child = (UIElement)Activator.CreateInstance(viewType);
        }

    }
}
