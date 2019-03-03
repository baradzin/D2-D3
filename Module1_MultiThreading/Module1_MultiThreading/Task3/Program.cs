using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix1 = new RectangleMatrix(5, 10);
            matrix1.FillMatrixWithRandomValues(0, 20);
            Console.WriteLine("Matrix A: ");
            matrix1.Print();
            var matrix2 = new RectangleMatrix(10, 5);
            matrix2.FillMatrixWithRandomValues(0, 20);
            Console.WriteLine("Matrix B: ");
            matrix2.Print();
            var matrix3 = matrix1 * matrix2;
            Console.WriteLine("Matrix C: ");
            matrix3.Print();
        }
    }
}
