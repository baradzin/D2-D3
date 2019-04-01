using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ManagedCalculator
{
    struct ComplexNumber
    {
        public double Real;
        public double Imaginary;
    };

    internal class NativeCalculatorInterop
    {
        [DllImport("NativeCalculator.dll", EntryPoint = "?Add@@YANNN@Z",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double Add(double a, double b);

        [DllImport("NativeCalculator.dll", EntryPoint = "?Summ@@YANQANH@Z",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern double Summ(
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]double[] a, 
            int count);

        [DllImport("NativeCalculator.dll", EntryPoint = "?Add@@YA?AUComplexNumber@@U1@0@Z",
            CallingConvention = CallingConvention.Cdecl)]
        public static extern ComplexNumber Add(ComplexNumber a, ComplexNumber b);
    }
}
