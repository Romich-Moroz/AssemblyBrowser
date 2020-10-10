using DisassemblerLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace DisassemblerTest
{

    class Foo
    {
        public static Foo CreateFromFuncs<T1, T2>(Func<T1, T2> f1, Func<T2, T1> f2)
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

    [TestClass]
    public class UnitTest
    {

        


        private static AssemblyInfo actual;

        [ClassInitialize]
        public static void TestInit(TestContext tc)
        {
            actual = new Disassembler().Disassemble(Assembly.LoadFrom("DisassemblerTest.dll"));
        }

        [TestMethod]
        public void AssemblyTest()
        {
            Assert.AreNotEqual(null, actual);
        }

        [TestMethod]
        public void NamespaceTest()
        {
            CollectionAssert.AllItemsAreNotNull(actual.Namespaces);
            CollectionAssert.AllItemsAreUnique(actual.Namespaces);
        }
        [TestMethod]
        public void ClassTest()
        {
            foreach(NamespaceInfo n in actual.Namespaces)
            {
                CollectionAssert.AllItemsAreNotNull(n.Classes);
                CollectionAssert.AllItemsAreUnique(n.Classes);
            }

            
        }
        [TestMethod]
        public void MembersTest()
        {
            foreach(NamespaceInfo n in actual.Namespaces)
            {
                foreach(ClassInfo c in n.Classes)
                {
                    CollectionAssert.AllItemsAreNotNull(c.Methods);
                    CollectionAssert.AllItemsAreUnique(c.Methods);
                    CollectionAssert.AllItemsAreNotNull(c.Fields);
                    CollectionAssert.AllItemsAreUnique(c.Fields);
                    CollectionAssert.AllItemsAreNotNull(c.Properties);
                    CollectionAssert.AllItemsAreUnique(c.Properties);
                }
            }            
        }

        [TestMethod]
        public void MembersValuesTest()
        {
            foreach (NamespaceInfo n in actual.Namespaces)
            {
                foreach (ClassInfo c in n.Classes)
                {
                    foreach(FieldInfo m in c.Fields)
                    {
                        Assert.IsNotNull(m.Name);
                        Assert.IsNotNull(m.FieldType);
                    }
                    foreach (PropertyInfo p in c.Properties)
                    {
                        Assert.IsNotNull(p.Name);
                        Assert.IsNotNull(p.PropertyType);
                    }
                    foreach (DisassemblerLib.MethodInfo m in c.Methods)
                    {
                        Assert.IsNotNull(m.Info);
                    }
                }
            }
        }
    }
}
