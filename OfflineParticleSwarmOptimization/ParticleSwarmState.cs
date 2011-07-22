using System;

namespace OfflineParticleSwarmOptimization
{
    public class ParticleSwarmState
    {
        /* S := swarm size
        * K := maximum number of particles _informed_ by a given one
        * p := probability threshold of random topology, typically calculated from K
        * w := first cognitive/confidence coefficient
        * c := second cognitive/confidence coefficient
         */
        public int S { get; set; }
        public double P { get; set; }
        public double W { get; set; }
        public double C { get; set; }
        public int Dimensions { get; set; }
        public double[] LowerBound { get; set; }
        public double[] UpperBound { get; set; }
        
        public double[][] Particles { get; set; }
        public double[][] Velocities { get; set; }
        public double[][] BestParticle { get; set; }
        public int[,] Links { get; set; }
        public int[] Index { get; set; }
        public int G { get; set; } //Global best index
        public double[] ParticleFitness { get; set; }
        public double[] BestParticleFitness { get; set; }
        //Default parameterless constructor for serialization / deserialization
        public ParticleSwarmState(){}
        public ParticleSwarmState(int dimensions, int swarmSize, double[] lowerInit, double[] upperInit, double[] lowerBound, double[] upperBound )
        {
            Random rand = new Random();

            Dimensions = dimensions;
            S = swarmSize;

            Particles = Tools.NewMatrix(swarmSize, dimensions);
            Velocities = Tools.NewMatrix(swarmSize, dimensions);
            BestParticle = Tools.NewMatrix(swarmSize, dimensions);
            Links = new int[swarmSize, swarmSize];
            Index = new int[swarmSize];
            BestParticleFitness = new double[swarmSize];
            ParticleFitness = new double[swarmSize];
            for (int s = 0; s < S; s++)
            {
                for (int d = 0; d < dimensions; d++)
                {
                    Particles[s][d] = rand.NextDouble(lowerInit[d], upperInit[d]);
                    Velocities[s][d] = (rand.NextDouble(lowerBound[d], upperBound[d]) - Particles[s][d]) / 2;
                }
            }
        }

    }
}