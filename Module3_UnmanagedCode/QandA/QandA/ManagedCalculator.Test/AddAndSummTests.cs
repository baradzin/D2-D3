using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ManagedCalculator.Test
{
    [TestClass]
    public class AddAndSummTests
    {
        [TestMethod]
        public void AddDouble()
        {
            var result = NativeCalculatorInterop.Add(-1, -2);
            Assert.AreEqual(-3, result);
        }

        [TestMethod]
        public void SumDouble()
        {
            var result = NativeCalculatorInterop.Summ(new double[] { -1, -2 }, 2);
            Assert.AreEqual(-3, result);
        }

        [TestMethod]
        public void AddComplex()
        {
            var result = NativeCalculatorInterop.Add(
                new ComplexNumber { Real = -1, Imaginary = -1 },
                new ComplexNumber { Real = -2, Imaginary = -2 });
            Assert.AreEqual(new ComplexNumber { Real = -3, Imaginary = -3 }, result);
        }
    }
}
