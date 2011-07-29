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

 
       //Initialize
      [DllExport("InitializeSwarmState", CallingConvention = CallingConvention.StdCall)]
       static void InitializeSwarmState(int dimensions, int swarmSize, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] lowerBound, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] upperBound, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] quantization)
      {

              _swarmState = ParticleSwarmOptimizer.InitializeParticleSwarmOptimizer(dimensions, swarmSize,
                                                                                    lowerBound, upperBound, lowerBound,
                                                                                    upperBound, quantization);

      }
       //ReportFitness
       [DllExport("ReportFitness", CallingConvention = CallingConvention.StdCall)]
       static void ReportFitness(int particle,double fitness)
       {
           if (particle >= _swarmState.Parameters.S) return;//Do nothing
           _swarmState = ParticleSwarmOptimizer.ReportFitness(particle, fitness, _swarmState);

       }
       //Get Values
       [DllExport("GetValues", CallingConvention = CallingConvention.StdCall)]
       static void GetValues(int dimensions, int particle, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] values)
       {
           if (particle >= _swarmState.Parameters.S) particle = _swarmState.G;//Get current best with invalid particle request
           ParticleSwarmOptimizer.GetValues(particle, _swarmState).CopyTo(values,0);
       }
       [DllExport("GetBestValues", CallingConvention = CallingConvention.StdCall)]
       static void GetBestValues(int dimensions, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] values)
       {
           ParticleSwarmOptimizer.GetValues(_swarmState.G, _swarmState).CopyTo(values,0);
       }
       [DllExport("GetInitialized", CallingConvention = CallingConvention.StdCall)]
       static bool GetInitialized()
       {
           return _swarmState.Initialized;
       }
   }
}
