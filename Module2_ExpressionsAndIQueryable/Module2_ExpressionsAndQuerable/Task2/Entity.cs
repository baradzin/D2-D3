using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public struct Entity<T>
    {
        public readonly T Value;
        public Entity(T value) { Value = value; }
        public U MapTo<U>() { return MapFunc<T, U>.Instance(Value); }
    }
}
