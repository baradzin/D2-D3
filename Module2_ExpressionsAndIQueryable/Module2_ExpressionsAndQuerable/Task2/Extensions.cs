using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public static class Extensions
    {
        public static Entity<T> Unit<T>(this T source) { return new Entity<T>(source); }
    }
}
