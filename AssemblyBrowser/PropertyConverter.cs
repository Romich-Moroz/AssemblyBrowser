using DisassemblerLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AssemblyBrowser
{
    [ValueConversion(typeof(PropertyInfo), typeof(string))]
    class PropertyConverter : IValueConverter
    {        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PropertyInfo v)
                return v.PropertyType.Name + ' ' + v.PropertyName;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
