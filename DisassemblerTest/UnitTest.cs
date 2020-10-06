using DisassemblerLib;
using Microsoft.VisualBasic;
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
        public void MethodsTest()
        {
            foreach(NamespaceInfo n in actual.Namespaces)
            {
                foreach(ClassInfo c in n.Classes)
                {
                    CollectionAssert.AllItemsAreNotNull(c.Methods);
                    CollectionAssert.AllItemsAreUnique(c.Methods);                    
                }
            }
            
        }
        [TestMethod]
        public void MembersTest()
        {
            foreach (NamespaceInfo n in actual.Namespaces)
            {
                foreach (ClassInfo c in n.Classes)
                {
                    CollectionAssert.AllItemsAreNotNull(c.Members);
                    CollectionAssert.AllItemsAreUnique(c.Members);
                }
            }
        }
    }
}
