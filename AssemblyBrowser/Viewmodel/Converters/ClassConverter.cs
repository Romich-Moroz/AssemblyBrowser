using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using DisassemblerLib;

namespace AssemblyBrowser
{
    class ClassConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ClassInfo c = value as ClassInfo;
            return TypenameBuilder.BuildTypename(c.ClassType.Name, c.ClassType.GetGenericArguments());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
