using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ManagedCalculator
{
    [ComVisible(true)]
    [Guid("604E3E41-2658-4CD6-A13B-015F293FD563")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Calculator : ICalculator
    {
        public double Add(double a, double b)
        {
            return NativeCalculatorInterop.Add(a, b);
        }

        public double Sum(double[] a)
        {
            return NativeCalculatorInterop.Summ(a, a.Length);
        }
    }
}
