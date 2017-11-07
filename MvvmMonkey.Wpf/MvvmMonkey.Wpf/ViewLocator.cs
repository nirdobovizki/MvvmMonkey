using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NirDobovizki.MvvmMonkey
{
    public static class ViewLocator 
    {

        public static void RegisterViews(ResourceDictionary resources, Assembly assembly)
        {
            foreach(var currentType in assembly.GetTypes())
            {
                var viewModelName = currentType.Name;
                if (!viewModelName.EndsWith("ViewModel", StringComparison.Ordinal)) continue;
                var viewName = viewModelName.Substring(0, viewModelName.Length - 5); // "ViewModel" -> "View"
                var viewType = assembly.GetTypes().SingleOrDefault(x => string.CompareOrdinal(x.Name, viewName) == 0);
                if (viewType == null)
                {
                    System.Diagnostics.Trace.WriteLine(string.Format("MvvmMonkey.ViewLocator: can't find view class {0} for view model {1}", viewName, viewModelName));
                    continue;
                }
                var template = new DataTemplate
                {
                    DataType = currentType,
                    VisualTree = new FrameworkElementFactory(viewType)
                };
                System.Diagnostics.Trace.WriteLine(string.Format("{0} => {1}", viewName, viewModelName));
                resources.Add(new DataTemplateKey(currentType), template);
            }
        }


    }
}
