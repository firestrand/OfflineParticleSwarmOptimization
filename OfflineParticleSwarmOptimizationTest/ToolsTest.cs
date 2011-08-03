using OfflineParticleSwarmOptimization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace OfflineParticleSwarmOptimizationTest
{
    
    
    /// <summary>
    ///This is a test class for ToolsTest and is intended
    ///to contain all ToolsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ToolsTest
    {
        /// <summary>
        ///A test for GetIndex
        ///</summary>
        [TestMethod()]
        public void GetIndexTest()
        {
            int a = 1;
            int b = 1;
            int expected = 6; // TODO: Initialize to an appropriate value
            int actual;
            actual = Tools.GetIndex(a, b, 5);
            Assert.AreEqual(expected, actual);
        }
    }
}
