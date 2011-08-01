using System;

namespace OfflineParticleSwarmOptimization
{
    public class ParticleSwarmState
    {
        public SwarmParameters Parameters { get; set; }
        public double[] LowerBound { get; set; }
        public double[] UpperBound { get; set; }
        
        public double[][] Particles { get; set; }
        public double[][] Velocities { get; set; }
        public double[][] BestParticle { get; set; }
        public int[] Links { get; set; }
        public int G { get; set; } //Global best index
        public double[] ParticleFitness { get; set; }
        public double[] BestParticleFitness { get; set; }
        public bool ShouldInitializeLinks { get; set; }
        public int EvaluationCount { get; set; }
        public double[] Quantization { get; set; }
        public bool Initialized { get; set; }

        //Default parameterless constructor for serialization / deserialization
        public ParticleSwarmState(){}
        public ParticleSwarmState(int dimensions, int swarmSize, int numberInformed, double[] lowerInit, double[] upperInit, double[] lowerBound, double[] upperBound, double[] quantization = null)
        {
            if (dimensions <= 0) throw new ArgumentOutOfRangeException("dimensions");
            if (swarmSize <= 0) throw new ArgumentOutOfRangeException("swarmSize");
            if (numberInformed <= 0) throw new ArgumentOutOfRangeException("numberInformed");
            if (lowerInit == null || lowerInit.Length != dimensions) throw new ArgumentOutOfRangeException("lowerInit");
            if (upperInit == null || upperInit.Length != dimensions) throw new ArgumentOutOfRangeException("upperInit");
            if (lowerBound == null || lowerBound.Length != dimensions) throw new ArgumentOutOfRangeException("lowerBound");
            if (upperBound == null || upperBound.Length != dimensions) throw new ArgumentOutOfRangeException("upperBound");

            Parameters = SwarmParameters.Initialize(dimensions, swarmSize, numberInformed);
            Initialize(dimensions, lowerInit, upperInit, lowerBound, upperBound, quantization);
        }

        public ParticleSwarmState(int dimensions, int numberInformed, double[] lowerInit, double[] upperInit, double[] lowerBound, double[] upperBound, double[] quantization = null )
        {
            if (dimensions <= 0) throw new ArgumentOutOfRangeException("dimensions");
            if (numberInformed <= 0) throw new ArgumentOutOfRangeException("numberInformed");
            if(lowerInit == null || lowerInit.Length != dimensions) throw new ArgumentOutOfRangeException("lowerInit");
            if (upperInit == null || upperInit.Length != dimensions) throw new ArgumentOutOfRangeException("upperInit");
            if (lowerBound == null || lowerBound.Length != dimensions) throw new ArgumentOutOfRangeException("lowerBound");
            if (upperBound == null || upperBound.Length != dimensions) throw new ArgumentOutOfRangeException("upperBound");

            
            Parameters = SwarmParameters.Initialize(dimensions, numberInformed);
            Initialize(dimensions, lowerInit, upperInit, lowerBound, upperBound, quantization);
        }

        private void Initialize(int dimensions, double[] lowerInit, double[] upperInit, double[] lowerBound, double[] upperBound,
                                double[] quantization)
        {
            var rand = new Random();
            var s = Parameters.S;

            Particles = Tools.NewMatrix(s, dimensions);
            Velocities = Tools.NewMatrix(s, dimensions);
            BestParticle = Tools.NewMatrix(s, dimensions);
            LowerBound = lowerBound;
            UpperBound = upperBound;
            Quantization = quantization;

            Links = new int[s*s];
            BestParticleFitness = new double[s];
            ParticleFitness = new double[s];
            for (int i = 0; i < s; i++)
            {
                for (int d = 0; d < dimensions; d++)
                {
                    Particles[i][d] = rand.NextDouble(lowerInit[d], upperInit[d]);
                    Velocities[i][d] = (rand.NextDouble(lowerBound[d], upperBound[d]) - Particles[i][d])/2;
                }
                Tools.Quantize(Particles[i], Quantization);
            }
            ShouldInitializeLinks = true;
            EvaluationCount = 0;
            Initialized = true;
            //InitializeFitness
            for (int i = 0; i < BestParticleFitness.Length; i++)
            {
                BestParticleFitness[i] = Double.MaxValue;
            }
            G = 0;
        }

        public void InitializeLinks()
        {
            if (ShouldInitializeLinks)	// Random topology
            {
                var rand = new Random();
                // Who informs who, at random
                for (int s = 0; s < Parameters.S; s++)
                {
                    for (int m = 0; m < Parameters.S; m++)
                    {
                        if (rand.NextDouble() < Parameters.P) Links[Tools.GetIndex(m, s)] = 1;	// Probabilistic method
                        else Links[Tools.GetIndex(m, s)] = 0;
                    }
                    Links[Tools.GetIndex(s, s)] = 1;
                }
            }
        }
        /// <summary>
        /// Set the globalBest to a new best if one was found
        /// </summary>
        /// <returns>True if a new best was found</returns>
        public bool SetGlobalBest()
        {
            int oldG = G;
            for(int i = 0; i < Parameters.S; i++)
            {
                if (BestParticleFitness[i] < BestParticleFitness[G])
                    G = i;
            }
            return oldG != G;
        }
        public void UpdateEpochValues()
        {
            InitializeLinks();
            ShouldInitializeLinks = SetGlobalBest();
        }
    }
}