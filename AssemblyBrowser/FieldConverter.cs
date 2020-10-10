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
    [ValueConversion(typeof(FieldInfo), typeof(string))]
    class FieldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {       
            if (value is FieldInfo f)
                return f.FieldType.Name + ' ' + f.FieldName;
            return null;                
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
