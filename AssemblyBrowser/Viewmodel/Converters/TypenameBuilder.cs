using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    public static class TypenameBuilder
    {
        public static string BuildTypename(string name, Type[] genericArgs, bool hasDecoratedName)
        {
            if (genericArgs.Length != 0)
            {
                if (hasDecoratedName)
                    name = name?.Remove(name.Length - 2);
                name += '<' + string.Join(",", genericArgs.Select(a => a.GetGenericArguments().Length == 0 ? a.Name : BuildTypename(a.Name, a.GetGenericArguments(),true))) + '>';
            }
            return name;
        }
    }
}
