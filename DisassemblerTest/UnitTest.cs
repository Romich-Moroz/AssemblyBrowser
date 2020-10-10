using DisassemblerLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace DisassemblerTest
{
    [TestClass]
    public class UnitTest
    {
        private static AssemblyInfo actual;

        [ClassInitialize]
        public static void TestInit(TestContext tc)
        {
            actual = new Disassembler().Disassemble(Assembly.LoadFrom("Disassembler.dll"));
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
                    foreach(DisassemblerLib.FieldInfo m in c.Fields)
                    {
                        Assert.IsNotNull(m.FieldName);
                        Assert.IsNotNull(m.FieldType);
                    }
                    foreach (DisassemblerLib.PropertyInfo p in c.Properties)
                    {
                        Assert.IsNotNull(p.PropertyName);
                        Assert.IsNotNull(p.PropertyType);
                    }
                    foreach (DisassemblerLib.MethodInfo m in c.Methods)
                    {
                        Assert.IsNotNull(m.Signature);
                    }
                }
            }
        }
    }
}
