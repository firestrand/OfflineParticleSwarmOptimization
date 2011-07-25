using System;

namespace OfflineParticleSwarmOptimization
{
    public struct SwarmParameters
    {
        public int D;
        public int S;
        public int K;
        public double P;
        public double W;
        public double C;
        public void Initialize(int dimensions, int numInformed)
        {
            /*
             * S := swarm size
             * K := maximum number of particles _informed_ by a given one
             * p := probability threshold of random topology, typically calculated from K
             * w := first cognitive/confidence coefficient
             * c := second cognitive/confidence coefficient
             */
            D = dimensions;
            S = (int)(10 + 2 * Math.Sqrt(dimensions));	// Swarm size
            K = numInformed; //number of informed particles
            P = 1 - Math.Pow(1 - (double)1 / (S), K); //Probability threshold of random topology
            // (to simulate the global best PSO, set p=1)

            // According to Clerc's Stagnation Analysis
            W = 1 / (2 * Math.Log(2.0)); // 0.721
            C = 0.5 + Math.Log(2.0); // 1.193
        }
    }
}