using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    internal class RectangleMatrix
    {
        public int N { get; set; }
        public int M { get; set; }
        public int[][] Matrix { get; set; }

        public RectangleMatrix(int n, int m)
        {
            this.N = n;
            this.M = m;
            this.Matrix = CreateMatrix(n, m);
        }

        private int[][] CreateMatrix(int n, int m)
        {
            int[][] result = new int[n][];
            for (int i = 0; i < n; ++i)
                result[i] = new int[m];
            return result;
        }

        public void FillMatrixWithRandomValues(int minValue, int maxValue)
        {
            var random = new Random();
            for(int i = 0; i < N; i++) {
                for(int j = 0; j < M; j++) {
                    Matrix[i][j] = random.Next(minValue, maxValue);
                }
            }
        }

        public static RectangleMatrix operator *(RectangleMatrix m1, RectangleMatrix m2)
        {
            int aRows = m1.Matrix.Length;
            int aCols = m1.Matrix[0].Length;
            int bRows = m2.Matrix.Length;
            int bCols = m2.Matrix[0].Length;
            if (aCols != bRows)
                throw new Exception("Number of columns matrix A not equal to number of rows Matrix B." +
                    " Multiplication is impossible");

            var result = new RectangleMatrix(aRows, bCols);

            Parallel.For(0, aRows, i => {
                for (int j = 0; j < bCols; ++j) {
                    for (int k = 0; k < aCols; ++k) {
                        result.Matrix[i][j] += m1.Matrix[i][k] * m2.Matrix[k][j];
                    } 
                }
            });

            return result;
        }

        public void Print()
        {
            for(int i = 0; i < N; i++) {
                for(int j = 0; j < M; j++) {
                    Console.Write($"{Matrix[i][j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
