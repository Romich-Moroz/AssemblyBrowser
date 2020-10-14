using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace AssemblyBrowser
{
    class FieldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {            
            string typeName = (value as FieldInfo)?.FieldType.Name ?? (value as PropertyInfo)?.PropertyType.Name;                       
            string fieldName = (value as FieldInfo)?.Name ?? (value as PropertyInfo)?.Name;

            Type[] genArguments = (value as FieldInfo)?.FieldType.GetGenericArguments() ?? (value as PropertyInfo)?.PropertyType.GetGenericArguments();

            return TypenameBuilder.BuildTypename(typeName, genArguments,true) +' ' + fieldName;               
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
