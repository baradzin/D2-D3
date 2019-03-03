using System;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        const int MIN_VALUE = 0;
        const int MAX_VALUE = 20;
        const int ARRAY_ELEMENTS_COUNT = 10;

        static void Main(string[] args)
        {
            var helper = new IntegerArrayHelper(MIN_VALUE, MAX_VALUE);
            Task.Factory.StartNew(() => helper.CreateArray(ARRAY_ELEMENTS_COUNT))
                .ContinueWith((x) => helper.MultiplyArray(x.Result))
                .ContinueWith((x) => helper.SortArrayAscending(x.Result))
                .ContinueWith((x) => helper.AverageValue(x.Result)).Wait();
            Console.ReadKey();
        }
    }
}
