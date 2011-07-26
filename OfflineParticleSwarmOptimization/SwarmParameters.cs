using System;

namespace OfflineParticleSwarmOptimization
{
    public struct SwarmParameters
    {
        /// <summary>
        /// D := dimensions of particles
        /// </summary>
        public int D;
        /// <summary>
        /// S := swarm size
        /// </summary>
        public int S;
        /// <summary>
        /// K := maximum number of particles _informed_ by a given one
        /// </summary>
        public int K;
        /// <summary>
        /// p := probability threshold of random topology, typically calculated from K
        /// </summary>
        public double P;
        /// <summary>
        /// w := first cognitive/confidence coefficient
        /// </summary>
        public double W;
        /// <summary>
        /// c := second cognitive/confidence coefficient
        /// </summary>
        public double C;
        public static SwarmParameters Initialize(int dimensions, int numInformed)
        {
            var s = (int) (10 + 2*Math.Sqrt(dimensions));
            return Initialize(dimensions,s,numInformed);
        }
        public static SwarmParameters Initialize(int dimensions, int swarmSize, int numInformed)
        {
            /*
             * S := swarm size
             * K := maximum number of particles _informed_ by a given one
             * p := probability threshold of random topology, typically calculated from K
             * w := first cognitive/confidence coefficient
             * c := second cognitive/confidence coefficient
             */
            var parameters = new SwarmParameters();
            parameters.D = dimensions;
            parameters.S = swarmSize;	// Swarm size
            parameters.K = numInformed; //number of informed particles
            parameters.P = 1 - Math.Pow(1 - (double)1 / (parameters.S), parameters.K); //Probability threshold of random topology
            // (to simulate the global best PSO, set p=1)

            // According to Clerc's Stagnation Analysis
            parameters.W = 1 / (2 * Math.Log(2.0)); // 0.721
            parameters.C = 0.5 + Math.Log(2.0); // 1.193
            return parameters;
        }
    }
}