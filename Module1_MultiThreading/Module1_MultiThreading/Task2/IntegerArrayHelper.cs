using System;

namespace Task2
{
    internal class IntegerArrayHelper
    {
        public Random Random { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public IntegerArrayHelper(int minValue, int maxValue)
        {
            this.Random = new Random();
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public int[] CreateArray(int numberOfElements)
        {
            var arrayInts = new int[numberOfElements];
            Console.WriteLine("Created random array:");
            for (int i = 0; i < 10; i++) {
                var number = this.Random.Next(MinValue, MaxValue);
                arrayInts[i] = number;
                Console.WriteLine(number);
            }
            Console.WriteLine();
            return arrayInts;
        }

        public int[] MultiplyArray(int[] array)
        {
            int a = this.Random.Next(MinValue, MaxValue);
            Console.WriteLine($"Multiplying scalar and vector \nScalar: {a} \nVector:");
            for (int i = 0; i < array.Length; i++) {
                array[i] = array[i] * a;
                Console.WriteLine(array[i]);
            }
            Console.WriteLine();
            return array;
        }

        public int[] SortArrayAscending(int[] array)
        {
            Array.Sort(array);
            Console.WriteLine("SortedList: ");
            foreach (var ar in array) {
                Console.WriteLine(ar);
            }
            Console.WriteLine();
            return array;
        }

        public double AverageValue(int[] array)
        {
            double sum = 0;
            for (int i = 0; i < array.Length; i++) {
                sum += array[i];
            }
            double averageValue = sum / array.Length;
            Console.WriteLine($"Average value: {averageValue}");
            return averageValue;
        }
    }
}
