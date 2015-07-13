namespace Lib.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var parser = new Calculator("2 + 4 / 2 * 5");

            var result = parser.Calculate();

            Assert.AreEqual(result, 12m);
        }
    }
}