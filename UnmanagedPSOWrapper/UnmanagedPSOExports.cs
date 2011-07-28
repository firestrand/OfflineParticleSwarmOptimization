using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using OfflineParticleSwarmOptimization;
using RGiesecke.DllExport;

namespace UnmanagedPSOWrapper
{
   internal static class UnmanagedPSOExports
   {
       private static ParticleSwarmState _swarmState;
       private static readonly object SwarmLock = new object();
 
       //Initialize
      [DllExport("initializeswarmstate", CallingConvention = CallingConvention.StdCall)]
      static void InitializeSwarmState(int dimensions, int swarmSize, double[] lowerBound, double[] upperBound, double[] quantization)
      {
         lock(SwarmLock)
         {
             _swarmState = ParticleSwarmOptimizer.InitializeParticleSwarmOptimizer(dimensions, swarmSize,
                                                                                   lowerBound, upperBound, lowerBound,
                                                                                   upperBound, quantization);
         }
      }
       //ReportFitness
       [DllExport("reportfitness", CallingConvention = CallingConvention.StdCall)]
       static void ReportFitness(int particle,double fitness)
       {
           lock(SwarmLock)
           {
               _swarmState = ParticleSwarmOptimizer.ReportFitness(particle, fitness, _swarmState);
           }
       }
       //Get Values
       [DllExport("getvalues", CallingConvention = CallingConvention.StdCall)]
       static double[] GetValues(int particle)
       {
           return ParticleSwarmOptimizer.GetValues(particle, _swarmState);
       }
   }
}
