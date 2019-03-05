using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task6
{
    internal class ThreadSafeCollection
    {
        object lockObject = new object();
        Random rand = new Random();
        int maxValue;
        int minValue;
        int numberElements;
        public List<int> List { get; set; }

        public ThreadSafeCollection(int minValue = 0, int maxValue = 20, int numberElements = 10)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.numberElements = numberElements;
            List = CreateSharedCollection();
        }
        public List<int> CreateSharedCollection()
        {
            var list = new List<int>();
            for (int i = 0; i < numberElements; i++) {
                list.Add(rand.Next(minValue, maxValue));
            }

            return list;
        }

        public void AddRandomInts(ThreadSafeCollection list)
        {
            lock (lockObject) {
                for(int i = 0; i < 10; i++) {
                    var value = rand.Next(minValue, maxValue);
                    Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} adding: {value}");
                    list.List.Add(value);
                }               
                Thread.Sleep(20);
            }
        }

        public void PrintCollection(ThreadSafeCollection list)
        {
            lock (lockObject) {
                foreach (var item in list.List) {
                    Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} print: {item}");
                }
                Thread.Sleep(200);
            }
        }
    }
}
