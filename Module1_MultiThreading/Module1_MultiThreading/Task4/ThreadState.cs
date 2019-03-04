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

        public void CreateThread(ThreadState state)
        {
            if(state._depth > 0) {
                state._value--;
                state._depth--;
                Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} value: {state._value}");
                var thread = new Thread(() => CreateThread(state));
                thread.Start();
                thread.Join();
                Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} value: {state._value} finished");
            }
        }
    }
}
