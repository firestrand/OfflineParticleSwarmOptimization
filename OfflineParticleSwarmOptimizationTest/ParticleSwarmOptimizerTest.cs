using System.Linq;
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
    public class ParticleSwarmOptimizerTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DimensionOfZeroThrowsAnException()
        {
            var swarmState = ParticleSwarmOptimizer.InitializeParticleSwarmOptimizer(0, new[] { 0.0 }, new[] { 0.0 }, new[] { 0.0 }, new[] { 0.0 });
        }
        [TestMethod]
        public void CanOptimizeSphere()
        {
            var dimensionality = 20;
            var swarmState = ParticleSwarmOptimizer.InitializeParticleSwarmOptimizer(dimensionality, Enumerable.Repeat(-100.0, dimensionality).ToArray(), Enumerable.Repeat(100.0, dimensionality).ToArray(), Enumerable.Repeat(-10000.0, dimensionality).ToArray(), Enumerable.Repeat(10000.0, dimensionality).ToArray());
            for(int i = 0; i < 15000; i++)
            {
                for (int s = 0; s < swarmState.Parameters.S; s++)
                {
                    var fitness = SphereFitness(ParticleSwarmOptimizer.GetValues(s, swarmState));
                    swarmState = ParticleSwarmOptimizer.ReportFitness(s, fitness, swarmState);
                }
            }
            Assert.IsTrue(swarmState.BestParticleFitness[swarmState.G] < 1.0);
        }
        private double SphereFitness(double[] coordinates)
        {
            double value = 0;

            for (int i = 0; i < coordinates.Length; i++)
            {
                double elm = coordinates[i];
                value += elm * elm;
            }

            return value;
        }
    }
}
