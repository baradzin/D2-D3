using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task7
{
    internal class TaskMethods
    {
        public void RunTaskWithException()
        {
            Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} this task will throw exception");
            Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} throwing exception...");
            throw new Exception();
        }

        public void RunTaskWithoutException()
        {
            Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} this task doesn't throw exceptions, complete");
        }
    }
}
