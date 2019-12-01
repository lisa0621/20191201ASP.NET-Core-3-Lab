using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace classlib1.test
{
    [TestClass]
    public class UnitTest1
    {
       [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var o = new Class1();
            var expected = "Go!!!!!!!!";
            
            // Act
            var actual = o.Go();
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
