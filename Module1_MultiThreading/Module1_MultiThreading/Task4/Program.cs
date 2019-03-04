﻿using System.Threading;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            var state = new ThreadState();
            var thread = new Thread(state.CreateThread);
            thread.Start();
            thread.Join();
        }
    }
}
