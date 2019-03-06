using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculateNumber(10).Wait();
        }

        public static async Task CalculateNumber(int N)
        {
            double result = await Task.Run<double>(() => GetSum(N));
            Console.WriteLine("Sum Result :: " + result);
        }

        public static double GetSum(int N)
        {
            double res = 0;
            for(int i = 1; i < N; i++) {
                res += i;
            }
            return res;
        }
    }
}
