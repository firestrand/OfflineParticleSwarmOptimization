using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace OfflineParticleSwarmOptimization
{
    public class ParticleSwarmOptimizer
    {
        public ParticleSwarmState InitializeParticleSwarmOptimizer(int dimensions, int swarmSize, double[] lowerInit, double[] upperInit, double[] lowerBound, double[] upperBound)
        {
            if(dimensions <= 0) throw new ArgumentOutOfRangeException("dimensions");
            var swarmState = new ParticleSwarmState(dimensions,swarmSize,lowerInit, upperInit, lowerBound, upperBound);

            return swarmState;
        }
        public ParticleSwarmState ReportFitness(double fitness, ParticleSwarmState swarmState)
        {
            throw new NotImplementedException();
        }
        
    }
}
