﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace DisassemblerLib
{
    public class MemberInfo
    {
        public string TypeName { get; set; }

        public MemberInfo(string TypeName)
        {
            this.TypeName = TypeName;
        }
    }

    public class MethodInfo
    {
        public string Signature { get; set; }

        public MethodInfo(string Signature)
        {
            this.Signature = Signature;
        }
    }

    public class ClassInfo
    {
        public string Name { get; set; }

        public ObservableCollection<MemberInfo> Members { get; set; } = new ObservableCollection<MemberInfo>();

        public ObservableCollection<MethodInfo> Methods { get; set; } = new ObservableCollection<MethodInfo>();

        public ClassInfo(string Name) => this.Name = Name;

    }
    public class NamespaceInfo
    {
        public string Name { get; set; }
        public ObservableCollection<ClassInfo> Classes { get; set; } = new ObservableCollection<ClassInfo>();

        public NamespaceInfo(string Name) => this.Name = Name;
    }
    public class AssemblyInfo
    {
        public string Name { get; set; }
        public ObservableCollection<NamespaceInfo> Namespaces { get; set; } = new ObservableCollection<NamespaceInfo>();

        public AssemblyInfo(string Name) => this.Name = Name;

    }

    public class Disassembler
    {
        public AssemblyInfo Disassemble(Assembly asm)
        {
            AssemblyInfo ai = new AssemblyInfo(asm.FullName);
            foreach (Type t in asm.GetTypes())
            {
                NamespaceInfo ni = new NamespaceInfo(t.Namespace ?? "global");               
                if (!ai.Namespaces.Any(n => n.Name == ni.Name) && !IsExtensionClass(t))
                    ai.Namespaces.Add(ni);
                else
                    ni = ai.Namespaces.First(n => n.Name == ni.Name);
                ClassInfo ci = new ClassInfo(t.Name);
                ni.Classes.Add(ci);
                foreach (System.Reflection.MemberInfo mi in t.GetMembers())
                    ci.Members.Add(new MemberInfo(mi.DeclaringType.ToString() +' ' + mi.Name));
                foreach (System.Reflection.MethodInfo mi in t.GetMethods())
                {
                    if (IsExtensionMethod(mi))
                    {
                        Type extType = mi.GetParameters()[0].ParameterType;
                        NamespaceInfo extNi = new NamespaceInfo(extType.Namespace ?? "global");
                        if (!ai.Namespaces.Any(n => n.Name == extNi.Name) && !IsExtensionClass(t))
                            ai.Namespaces.Add(extNi);
                        else
                            extNi = ai.Namespaces.First(n => n.Name == extNi.Name);
                        ClassInfo extCi = new ClassInfo(extType.Name);
                        if (!extNi.Classes.Any(n => n.Name == extCi.Name))
                            extNi.Classes.Add(extCi);
                        else
                            extCi = extNi.Classes.First(n => n.Name == extCi.Name);
                        extCi.Methods.Add(new MethodInfo("(extension) " + mi.ToString()));
                    }
                    else
                        ci.Methods.Add(new MethodInfo(mi.ToString()));
                }
                    
            }
            return ai;
        }

        private bool IsExtensionClass(Type t)
        {
            return t.IsAbstract && t.IsSealed && t.GetMethods().All(m => IsExtensionMethod(m));
        }

        private bool IsExtensionMethod(System.Reflection.MethodInfo m)
        {
            return m.IsDefined(typeof(ExtensionAttribute), true);
        }
    }

}