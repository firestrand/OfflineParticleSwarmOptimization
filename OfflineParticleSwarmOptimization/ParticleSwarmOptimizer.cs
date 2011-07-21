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
        private readonly int _dimensions;
        public ParticleSwarmOptimizer(int dimensions)
        {
            if(dimensions <= 0) throw new ArgumentOutOfRangeException("dimensions");
            _dimensions = dimensions;
        }
        public double[] ReportFitness(double fitness)
        {
            var result = new double[_dimensions];
            return result;
        }
        
    }
}
