using DisassemblerLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AssemblyBrowser
{
    class Model
    {
        public AssemblyInfo GetAssemblyInfo()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select assembly to view";
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            ofd.Filter = ".Net assembly files (*.exe, *.dll) |*.exe;*.dll";
            Assembly asm;
            try
            {
                if (ofd.ShowDialog() == true)
                    asm = Assembly.LoadFrom(ofd.FileName);
                else
                    return null;
            }
            catch
            {
                return null;
            }
            Disassembler d = new Disassembler();
            return d.Disassemble(asm);
        }
        
    }
    class Foo
    {
        public static Foo CreateFromFuncs<T1, T2>(Func<T1, T2> f1, Func<T2, T1> f2)
        {
            return null;
        }
        public static List<List<List<T1>>> CreateFromFuncs<T1, T2>(List<List<List<T1>>> f1, List<List<List<T2>>> f2)
        {
            return null;
        }
    }

    public static class MyExtensions
    {
        public static int WordCount(this string str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }
        public static int Asd()
        {
            return 0;
        }
    }
}
