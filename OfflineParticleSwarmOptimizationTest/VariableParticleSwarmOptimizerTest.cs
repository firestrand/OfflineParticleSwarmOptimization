using OfflineParticleSwarmOptimization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace OfflineParticleSwarmOptimizationTest
{
    
    
    /// <summary>
    ///This is a test class for VariableParticleSwarmOptimizerTest and is intended
    ///to contain all VariableParticleSwarmOptimizerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class VariableParticleSwarmOptimizerTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DimensionOfZeroThrowsAnException()
        {
            int dimensions = 0;
            var target = new VariableParticleSwarmOptimizer(dimensions);
        }
        [TestMethod]
        public void ReportFitnessReturnsArraySizeEqualToDimensions()
        {
            int dimensions = 1;
            var target = new VariableParticleSwarmOptimizer(dimensions);
            Assert.AreEqual(dimensions,target.ReportFitness(0.0).Length);
        }
    }
}
