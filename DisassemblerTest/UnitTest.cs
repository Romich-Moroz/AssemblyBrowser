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
                    CollectionAssert.AllItemsAreNotNull(c.Constructors);
                    CollectionAssert.AllItemsAreUnique(c.Constructors);
                }
            }            
        }

        [TestMethod]
        public void MembersValuesTest()
        {
            Assert.AreEqual("<global>", actual.Namespaces[0].Name);
            Assert.AreEqual("DisassemblerTest", actual.Namespaces[1].Name);
            Assert.AreEqual("System", actual.Namespaces[2].Name);
            Assert.AreEqual(typeof(Foo), actual.Namespaces[1].Classes[0].ClassType);
            Assert.AreEqual(typeof(MyExtensions), actual.Namespaces[1].Classes[1].ClassType);
            Assert.AreEqual("CreateFromFuncs", actual.Namespaces[1].Classes[0].Methods[0].Info.Name);
        }
    }
}
