using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NirDobovizki.MvvmMonkey
{
    public class EnumValueIsCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter == null)
            {
                System.Diagnostics.Trace.WriteLine("MvvmMonkey.EnumValueIsCheckedConverter: ConverterParameter must be set to an enum value");
            }
            if (value == null) return Binding.DoNothing;
            return (value.Equals(parameter));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool)value) return parameter;
            return Binding.DoNothing;
        }
    }
}
