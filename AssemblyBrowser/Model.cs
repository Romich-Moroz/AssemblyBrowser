using DisassemblerLib;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
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
