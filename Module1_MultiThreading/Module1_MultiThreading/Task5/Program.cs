using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        public static Semaphore semaphore = new Semaphore(1, 1);
        static void Main(string[] args)
        {
            var state = new ThreadState();
            Console.WriteLine("First Task starting...");
            ThreadPool.QueueUserWorkItem(state.CreateThread, state);            
            Console.WriteLine("First Task ending...");
            Console.ReadKey();
        }
    }
}
