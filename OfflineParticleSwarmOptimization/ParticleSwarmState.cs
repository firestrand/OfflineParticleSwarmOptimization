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
        public int D { get; set; }
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


    }
}