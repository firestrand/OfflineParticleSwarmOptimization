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
        public SwarmParameters Parameters { get; set; }
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
        public bool InitializeLinks { get; set; }

        //Default parameterless constructor for serialization / deserialization
        public ParticleSwarmState(){}
        public ParticleSwarmState(int dimensions, int numberInformed, double[] lowerInit, double[] upperInit, double[] lowerBound, double[] upperBound )
        {
            var rand = new Random();
            var parameters = new SwarmParameters();
            parameters.Initialize(dimensions,numberInformed);
            var s = Parameters.S;

            Particles = Tools.NewMatrix(s, dimensions);
            Velocities = Tools.NewMatrix(s, dimensions);
            BestParticle = Tools.NewMatrix(s, dimensions);
            Links = new int[s, s];
            Index = new int[s];
            BestParticleFitness = new double[s];
            ParticleFitness = new double[s];
            for (int i = 0; i < s; i++)
            {
                for (int d = 0; d < dimensions; d++)
                {
                    Particles[i][d] = rand.NextDouble(lowerInit[d], upperInit[d]);
                    Velocities[i][d] = (rand.NextDouble(lowerBound[d], upperBound[d]) - Particles[i][d]) / 2;
                }
            }
            InitializeLinks = true;
        }
        
        
    }
}