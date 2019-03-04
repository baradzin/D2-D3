using System;
using System.Threading;

namespace Task4
{
    internal class ThreadState
    {
        public int _depth;
        public int _value;

        public ThreadState(int recursionDepth = 10, int value = 20)
        {
            this._value = value;
            this._depth = recursionDepth;
        }

        public void CreateThread(object threadState)
        {
            Console.WriteLine("Taking Task2....");
            Program.semaphore.WaitOne();
            var state = (ThreadState)threadState;
            if (state._depth > 0) {
                state._value--;
                state._depth--;
                Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} value: {state._value}");
                ThreadPool.QueueUserWorkItem(state.CreateThread, state);
              //  Thread.Sleep(1000);
                Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} value: {state._value} finished");
            }
            Console.WriteLine("Releasing Task2....");
            Program.semaphore.Release();
        }
    }
}
