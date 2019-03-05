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
            Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} Taking resource...");
            Program.semaphore.WaitOne();
            var state = (ThreadState)threadState;
            if (state._depth > 0) {
                state._value--;
                state._depth--;
                Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} value: {state._value}");
                ThreadPool.QueueUserWorkItem(state.CreateThread, state);
                Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} value: {state._value} finished");
            } else {
                Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} Unable to create new thread");
            }
            Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} Releasing resource...");
            Program.semaphore.Release();
        }
    }
}
