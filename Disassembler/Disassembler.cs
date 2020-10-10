using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DisassemblerLib
{

    public class MethodInfo
    {
        public System.Reflection.MethodInfo Info { get; set; }

        public bool IsExtension { get; set; }

        public MethodInfo(System.Reflection.MethodInfo mInfo, bool IsExtension = false)
        {
            this.Info = mInfo;           
            this.IsExtension = IsExtension;
        }
    }

    public class ClassInfo
    {
        public Type ClassType { get; set; }

        public List<PropertyInfo> Properties { get; set; } = new List<PropertyInfo>();

        public List<FieldInfo> Fields { get; set; } = new List<FieldInfo>();

        public List<MethodInfo> Methods { get; set; } = new List<MethodInfo>();

        public List<ConstructorInfo> Constructors { get; set; } = new List<ConstructorInfo>();

        public ClassInfo(Type t) => this.ClassType = t;

    }
    public class NamespaceInfo
    {
        public string Name { get; set; }
        public List<ClassInfo> Classes { get; set; } = new List<ClassInfo>();
        public NamespaceInfo(string Name) => this.Name = Name;
    }
    public class AssemblyInfo
    {
        public Assembly Asm { get; set; }
        public List<NamespaceInfo>Namespaces { get; set; } = new List<NamespaceInfo>();
        public AssemblyInfo(Assembly Asm) => this.Asm = Asm;

    }

    public class Disassembler
    {
        public AssemblyInfo Disassemble(Assembly asm)
        {
            AssemblyInfo ai = new AssemblyInfo(asm);
            foreach (Type t in asm.GetTypes())
            {
                
                

                ClassInfo ci = new ClassInfo(t);
                if (Attribute.GetCustomAttribute(t, typeof(CompilerGeneratedAttribute)) == null)
                {
                    NamespaceInfo ni = new NamespaceInfo(t.Namespace ?? "<global>");
                    if (!ai.Namespaces.Any(n => n.Name == ni.Name) && !IsExtensionClass(t))
                        ai.Namespaces.Add(ni);
                    else
                        ni = ai.Namespaces.First(n => n.Name == ni.Name);

                    ni.Classes.Add(ci);

                    foreach (ConstructorInfo pi in t.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                        if (Attribute.GetCustomAttribute(pi, typeof(CompilerGeneratedAttribute)) == null)
                            ci.Constructors.Add(pi);

                    foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                        if (Attribute.GetCustomAttribute(pi, typeof(CompilerGeneratedAttribute)) == null)
                            ci.Properties.Add(pi);

                    foreach (FieldInfo fi in t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                        if (Attribute.GetCustomAttribute(fi, typeof(CompilerGeneratedAttribute)) == null)
                                ci.Fields.Add(fi);

                    foreach (System.Reflection.MethodInfo mi in t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                    {
                        if (IsExtensionMethod(mi))
                        {
                            Type extType = mi.GetParameters()[0].ParameterType;
                            NamespaceInfo extNi = new NamespaceInfo(extType.Namespace ?? "<global>");
                            if (!ai.Namespaces.Any(n => n.Name == extNi.Name) && !IsExtensionClass(t))
                                ai.Namespaces.Add(extNi);
                            else
                                extNi = ai.Namespaces.First(n => n.Name == extNi.Name);
                            ClassInfo extCi = new ClassInfo(extType);
                            if (!extNi.Classes.Any(n => n.ClassType.Name == extCi.ClassType.Name))
                                extNi.Classes.Add(extCi);
                            else
                                extCi = extNi.Classes.First(n => n.ClassType.Name == extCi.ClassType.Name);
                            if (Attribute.GetCustomAttribute(mi, typeof(CompilerGeneratedAttribute)) == null)
                                extCi.Methods.Add(new MethodInfo(mi, true));
                        }
                        else
                        {
                            if (Attribute.GetCustomAttribute(mi, typeof(CompilerGeneratedAttribute)) == null)
                                ci.Methods.Add(new MethodInfo(mi));
                        }
                            
                    }
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
