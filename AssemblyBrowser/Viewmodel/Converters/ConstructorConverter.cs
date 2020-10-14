using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using DisassemblerLib;

namespace AssemblyBrowser
{
    class ConstructorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ConstructorInfo c = value as ConstructorInfo;
            string nameGenArgs = TypenameBuilder.BuildTypename(c.DeclaringType.Name, c.DeclaringType.GetGenericArguments(),true);

            string parameters = '(' + string.Join(",", c.GetParameters().Select(p => TypenameBuilder.BuildTypename(p.ParameterType.Name, p.ParameterType.GetGenericArguments(),true) + ' ' + p.Name)) + ')';
            return nameGenArgs + parameters;           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
