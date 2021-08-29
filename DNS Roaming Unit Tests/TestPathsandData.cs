using DNS_Roaming_Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace DNS_Roaming_Unit_Tests
{
    [TestClass]
    public class TestPathsAndData
    {
        [TestMethod]
        public void BaseApplicationPath_Exists()
        {
            // Arrange
            PathsandData pathsandData = new PathsandData();
            
            // Act
            
            // Assert
            Assert.IsTrue(Directory.Exists(pathsandData.BaseApplicationPath));
        }
    }
}
