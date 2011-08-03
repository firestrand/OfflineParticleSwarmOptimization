using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineParticleSwarmOptimization
{
    public static partial class Tools
    {
        /// <summary>
        /// Allocate and return a new matrix double[dim1][dim2].
        /// </summary>
        public static double[][] NewMatrix(int dim1, int dim2)
        {
            var matrix = new double[dim1][];

            for (int i = 0; i < dim1; i++)
            {
                matrix[i] = new double[dim2];
            }

            return matrix;
        }
        public static T[,] ArrayToMatrix<T>(T[] array, int m, int n)
        {
            var matrix = new T[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = array[i * n + j];
                }
            }
            return matrix;
        }

        public static bool ValuesEqual<T>(this T[,] matrix, T[,] compare)
        {
            if (matrix.Rank != compare.Rank || matrix.Length != compare.Length)
                return false;
            for (int i = 0; i <= matrix.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= matrix.GetUpperBound(1); j++)
                {
                    if (!matrix[i, j].Equals(compare[i, j]))
                        return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Clamp the position and velocity vectors setting velocity to 0 if the position is outside of the solution space.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="v"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        public static void Clamp(double[] x, double[] v, double[] lower, double[] upper)
        {
            if (x.Length != v.Length || x.Length != lower.Length || x.Length != upper.Length)
                throw new ArgumentOutOfRangeException("arrays", "Array lengths are not equal.");
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] < lower[i])
                {
                    x[i] = lower[i];
                    v[i] = 0.0;
                }
                if (x[i] > upper[i])
                {
                    x[i] = upper[i];
                    v[i] = 0.0;
                }
            }
        }
        public static double NextDouble(this Random rand, double lowerBound, double upperBound)
        {
            return lowerBound + rand.NextDouble() * (upperBound - lowerBound);
        }

        public static void Quantize(double[] x, double[] q)
        {
            
            if (q == null) return;
            if (x == null) throw new ArgumentNullException("x");
            /*
             Quantisatition of a position
             Only values like x+k*q (k integer) are admissible 
             */
            if (x.Length != q.Length)
                throw new ArgumentOutOfRangeException("x,q", "Arrays are not the same length.");

            for (int d = 0; d < q.Length; d++)
            {
                if (q[d] > 0.0)	// Note that qd can't be < 0
                {
                    x[d] = q[d] * Math.Floor(0.5 + x[d] / q[d]);
                }
            }
        }
        public static int GetIndex(int a, int b, int cols=2)
        {
            return a * cols + b;
        }
    }
}
