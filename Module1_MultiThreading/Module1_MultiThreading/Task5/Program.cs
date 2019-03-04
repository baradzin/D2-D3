using System;
using System.Threading;

namespace Task4
{
    class Program
    {
        public static Semaphore semaphore = new Semaphore(10, 10);
        static void Main(string[] args)
        {
            var state = new ThreadState();
            Console.WriteLine("First Task starting...");
            semaphore.WaitOne();
            var task = ThreadPool.QueueUserWorkItem(state.CreateThread, state);
            Console.WriteLine("First Task ending...");
        }
    }
}
