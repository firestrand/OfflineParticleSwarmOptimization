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
        public ParticleSwarmState InitializeParticleSwarmOptimizer(int dimensions, double[] lowerInit, double[] upperInit, double[] lowerBound, double[] upperBound)
        {
            const int numberOfInformedParticles = 3;
            var swarmState = new ParticleSwarmState(dimensions,numberOfInformedParticles,lowerInit, upperInit, lowerBound, upperBound);
            return swarmState;
        }
        public ParticleSwarmState ReportFitness(int particle, double fitness, ParticleSwarmState swarmState)
        {
            int S = swarmState.Parameters.S;
            double p = swarmState.Parameters.P;

            var rand = new Random();
            //Set the particles fitness
            swarmState.ParticleFitness[particle] = fitness;
            
            //Check if epoch values should be updated
            if(particle == S)
            {
                swarmState.UpdateEpochValues();
            }
            
            //check if better
            //move particle
        }
        public double[] GetValues(int particle, ParticleSwarmState swarmState)
        {
            return swarmState.Particles[particle];
        }
        
    }
}
