using System;
using System.Threading;

namespace Task4
{
    internal class ThreadState
    {
        private int _depth;
        private int _value;

        public ThreadState(int recursionDepth = 10, int value = 20)
        {
            this._value = value;
            this._depth = recursionDepth;
        }

        public void CreateThread()
        {
            if(_depth > 0) {
                _value--;
                _depth--;
                Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} value: {_value}");
                new Thread(CreateThread).Start();
            }
        }
    }
}
