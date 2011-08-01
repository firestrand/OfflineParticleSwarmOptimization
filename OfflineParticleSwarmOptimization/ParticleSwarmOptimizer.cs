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
    public static class ParticleSwarmOptimizer
    {
        const int NumberOfInformedParticles = 3;
        public static ParticleSwarmState InitializeParticleSwarmOptimizer(int dimensions, int swarmSize, int numberOfInformed, double[] lowerInit, double[] upperInit, double[] lowerBound, double[] upperBound, double[] quantization = null)
        {
            return new ParticleSwarmState(dimensions, swarmSize, numberOfInformed, lowerInit, upperInit, lowerBound, upperBound, quantization);
        }
        public static ParticleSwarmState InitializeParticleSwarmOptimizer(int dimensions, int swarmSize, double[] lowerInit, double[] upperInit, double[] lowerBound, double[] upperBound, double[] quantization = null)
        {
            return new ParticleSwarmState(dimensions, swarmSize, NumberOfInformedParticles, lowerInit, upperInit, lowerBound, upperBound, quantization);
        }
        public static ParticleSwarmState InitializeParticleSwarmOptimizer(int dimensions, double[] lowerInit, double[] upperInit, double[] lowerBound, double[] upperBound, double[] quantization = null)
        {
            return new ParticleSwarmState(dimensions, NumberOfInformedParticles, lowerInit, upperInit, lowerBound, upperBound, quantization);
        }

        public static ParticleSwarmState ReportFitness(int particle, double fitness, ParticleSwarmState swarmState)
        {
            int S = swarmState.Parameters.S;

            var rand = new Random();
            //Set the particles fitness
            swarmState.ParticleFitness[particle] = fitness;

            // ... update the best previous position
            if (swarmState.ParticleFitness[particle] < swarmState.BestParticleFitness[particle])	// Improvement
            {
                swarmState.Particles[particle].CopyTo(swarmState.BestParticle[particle], 0);
                swarmState.BestParticleFitness[particle] = swarmState.ParticleFitness[particle];
            }

            //Check if epoch values should be updated
            if(particle == S || swarmState.EvaluationCount == 0)
            {
                swarmState.UpdateEpochValues();
            }
            
            int s = particle;
            // ... find the first informant
            int s1 = 0;
            while (swarmState.Links[Tools.GetIndex(s1, s)] == 0) s1++;
            if (s1 >= S) s1 = s;
            // Find the best informant			
            int g = s1;
            for (int m = s1; m < S; m++)
            {
                if (swarmState.Links[Tools.GetIndex(m, s)] == 1 && swarmState.BestParticleFitness[m] < swarmState.BestParticleFitness[g])
                    g = m;
            }
            //.. compute the new velocity, and move
            // Exploration tendency
            for (int d = 0; d < swarmState.Parameters.D; d++)
            {
                swarmState.Velocities[s][d] = swarmState.Parameters.W * swarmState.Velocities[s][d];
                //If Quantization is in effect set vmin magnitude to Quantization value
                if (swarmState.Quantization != null &&
                    Math.Abs(swarmState.Velocities[s][d]) < swarmState.Quantization[d])
                {
                    swarmState.Velocities[s][d] = swarmState.Quantization[d]*
                                                    Math.Sign(swarmState.Velocities[s][d]);
                }
                double px = swarmState.BestParticle[s][d] - swarmState.Particles[s][d];
                swarmState.Velocities[s][d] += rand.NextDouble(0.0, swarmState.Parameters.C) * px;
                if (g != s)
                {
                    double gx = swarmState.BestParticle[g][d] - swarmState.Particles[s][d];
                    swarmState.Velocities[s][d] += rand.NextDouble(0.0, swarmState.Parameters.C) * gx;
                }
                swarmState.Particles[s][d] = swarmState.Particles[s][d] + swarmState.Velocities[s][d];

                //Bound Velocity
                if (swarmState.Particles[s][d] < swarmState.LowerBound[d])
                {
                    swarmState.Particles[s][d] = swarmState.LowerBound[d];
                    swarmState.Velocities[s][d] = 0;
                }

                if (swarmState.Particles[s][d] > swarmState.UpperBound[d])
                {
                    swarmState.Particles[s][d] = swarmState.UpperBound[d];
                    swarmState.Velocities[s][d] = 0;
                }
            }
            //Respect Quantization
            Tools.Quantize(swarmState.Particles[s],swarmState.Quantization);
            swarmState.EvaluationCount++;
            return swarmState;
        }
        public static double[] GetValues(int particle, ParticleSwarmState swarmState)
        {
            return swarmState.Particles[particle];
        }
        
    }
}
