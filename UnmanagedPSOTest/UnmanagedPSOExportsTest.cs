using UnmanagedPSOWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OfflineParticleSwarmOptimization;

namespace UnmanagedPSOTest
{
    
    
    /// <summary>
    ///This is a test class for UnmanagedPSOExportsTest and is intended
    ///to contain all UnmanagedPSOExportsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UnmanagedPSOExportsTest
    {
        private const string _path = @"C:\Program Files\MetaTrader 5\MQL5\Files\PSOTest.xml";

        /// <summary>
        ///A test for SerializePSOState
        ///</summary>
        [TestMethod()]
        public void SerializePSOStateTest()
        {
            int dim = 3;
            double[] lowerBound = new double[]{-1,-1,-1};
            double[] upperBound = new double[]{1,1,1};

            ParticleSwarmState swarmState = new ParticleSwarmState(dim,3,lowerBound,upperBound,lowerBound,upperBound);
            UnmanagedPSOExports.SerializePSOState(swarmState);
        }

        /// <summary>
        ///A test for DeserializePSOState
        ///</summary>
        [TestMethod()]
        public void DeserializePSOStateTest()
        {
            ParticleSwarmState expected = new ParticleSwarmState();
            ParticleSwarmState actual;
            actual = UnmanagedPSOExports.DeserializePSOState();
            Assert.AreEqual(expected, actual);
        }
    }
}
