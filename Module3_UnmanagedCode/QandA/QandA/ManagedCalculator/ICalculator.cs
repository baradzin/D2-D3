using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ManagedCalculator
{
    [ComVisible(true)]
    [Guid("E1753DD8-00E0-4DF7-93EB-B509B7C8F4BF")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface ICalculator
    {
        double Add(double a, double b);
        double Sum(double[] a);
    }
}
