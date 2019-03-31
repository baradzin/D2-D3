using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Task2.Tests
{ 
    public class Foo
    {
        public string Bar { get; set; }
        public string Baz { get; set; }
    }

    public class Boo
    {
        public string Bar { get; set; }
        public string Baz { get; set; }
    }

    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Mapper_Test()
        {
            var obj = new { Bar = "Bar obj", Baz = "Baz obj" };
            var boo = new Boo() { Bar = "Bar Boo", Baz = "Baz Boo" };
            var foo1 = obj.Unit().MapTo<Foo>();
            var foo2 = boo.Unit().MapTo<Foo>();

            Assert.IsTrue(foo1 is Foo);
            Assert.IsTrue(foo2 is Foo);

            Assert.AreEqual(foo1.Bar, obj.Bar);
            Assert.AreEqual(foo1.Baz, obj.Baz);
            Assert.AreEqual(foo2.Bar, boo.Bar);
            Assert.AreEqual(foo2.Baz, boo.Baz);
        }
    }
}
