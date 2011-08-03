using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using OfflineParticleSwarmOptimization;
using RGiesecke.DllExport;

namespace UnmanagedPSOWrapper
{
   internal static class UnmanagedPSOExports
   {
       private const string _statePath = @"C:\Program Files\MetaTrader 5\MQL5\Files\PSOState.xml";
       private const string _historyPath = @"C:\Program Files\MetaTrader 5\MQL5\Files\PSOHistory.csv";
       //Initialize
      [DllExport("InitializeSwarmState", CallingConvention = CallingConvention.StdCall)]
       static void InitializeSwarmState(int dimensions, int swarmSize,
          [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] lowerBound,
          [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] upperBound,
          [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] quantization)
      {

          var swarmState = ParticleSwarmOptimizer.InitializeParticleSwarmOptimizer(dimensions, swarmSize,
                                                                                    lowerBound, upperBound, lowerBound,
                                                                                    upperBound, quantization);
          SerializePSOState(swarmState);
          CreateHistoryFile(swarmState);
      }
       //ReportFitness
       [DllExport("ReportFitness", CallingConvention = CallingConvention.StdCall)]
       static void ReportFitness(int particle,double fitness)
       {
           //Get Current State from File
           var swarmState = DeserializePSOState();
           if (particle >= swarmState.Parameters.S) return;//Do nothing

           WriteHistoryLine(particle, swarmState, fitness);
           ParticleSwarmOptimizer.ReportFitness(particle, fitness, swarmState);
           SerializePSOState(swarmState);

       }
       //Get Values
       [DllExport("GetValues", CallingConvention = CallingConvention.StdCall)]
       static void GetValues(int dimensions, int particle,
           [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] values)
       {
           //Get Current State from File
           var swarmState = DeserializePSOState();
           if (particle >= swarmState.Parameters.S) particle = swarmState.G;//Get current best with invalid particle request
           ParticleSwarmOptimizer.GetValues(particle, swarmState).CopyTo(values,0);
       }
       [DllExport("GetBestValues", CallingConvention = CallingConvention.StdCall)]
       static void GetBestValues(int dimensions,
           [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] double[] values)
       {
           //Get Current State from File
           var swarmState = DeserializePSOState();
           ParticleSwarmOptimizer.GetValues(swarmState.G, swarmState).CopyTo(values,0);
       }
       [DllExport("GetInitialized", CallingConvention = CallingConvention.StdCall)]
       static bool GetInitialized()
       {
           //Get Current State from File
           var swarmState = DeserializePSOState();
           return swarmState.Initialized;
       }
       public static void WriteHistoryLine(int particle, ParticleSwarmState swarmState, double fitness)
       {
           var sb = new StringBuilder();
           sb.AppendFormat("{0},{1},{2}", particle,swarmState.EvaluationCount,fitness);
           foreach(var v in swarmState.Particles[particle])
           {
               sb.AppendFormat(",{0}", v);
           }
           var fi = new FileInfo(_historyPath);
           using (var sw = fi.AppendText())
           {
               sw.WriteLine(sb.ToString());
           }
       }
       public static void CreateHistoryFile(ParticleSwarmState swarmState)
       {
           var sb = new StringBuilder();
           sb.Append("Particle,FunctionEvaluation,Fitness");
           for (int i = 0; i < swarmState.Parameters.D; i++)
           {
               sb.AppendFormat(",Parameter{0}", i);
           }
           sb.AppendLine();
           var fi = new FileInfo(_historyPath);
           using(var sw = fi.CreateText())
           {
               sw.WriteLine(sb.ToString());
           }
       }
       public static ParticleSwarmState DeserializePSOState()
       {
           string swarmStateXml;
           if (!File.Exists(_statePath))
           {
               Console.WriteLine("{0} does not exist.", _statePath);
               return new ParticleSwarmState();
           }
           var fi = new FileInfo(_statePath);
           using (var sr = fi.OpenText())
           {
               swarmStateXml = sr.ReadToEnd();
           }

           ParticleSwarmState result;
           var xmlSerializer = new XmlSerializer(typeof(ParticleSwarmState));
           using (var reader = new StringReader(swarmStateXml))
           {
               result = xmlSerializer.Deserialize(reader) as ParticleSwarmState;
               if (result == null)
                   throw new SerializationException("Deserialization of ParticleSwarmState failed.");
           }
           return result;
       }
       public static void SerializePSOState(ParticleSwarmState swarmState)
       {
           var xmlSerializer = new XmlSerializer(typeof(ParticleSwarmState));
           var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
           var ns = new XmlSerializerNamespaces();
           ns.Add("", "");
           using (var stringWriter = new StringWriter())
           {
               using (var writer = XmlWriter.Create(stringWriter, settings))
               {
                   xmlSerializer.Serialize(writer, swarmState, ns);
                   File.WriteAllText(_statePath,stringWriter.ToString());
               }
           }
       }
   }
}
