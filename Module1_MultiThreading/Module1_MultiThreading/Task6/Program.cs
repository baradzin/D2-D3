using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task6
{
    class Program
    {
        static void Main(string[] args)
        {
            int collectionLength = 1000;
            int threadCount = 50;
            string threadElapsedTime = RunThreads(collectionLength, threadCount);
            string taskElapsedTime = RunTasks(collectionLength, threadCount);
            Console.WriteLine(threadElapsedTime);
            Console.WriteLine(taskElapsedTime);
        }

        static string RunThreads(int collectionLength, int threadCount)
        {
            var list = new ThreadSafeCollection(numberElements: collectionLength);
            Stopwatch watch = new Stopwatch();
            Thread[] threadList = new Thread[threadCount];
            for (int i = 0; i < threadCount; i+=2) {
                threadList[i] = new Thread(() => list.AddRandomInts(list));
                threadList[i+1] = new Thread(() => list.PrintCollection(list));
            }

            watch.Start();
            //Parallel.ForEach(threadList, (t) => { t.Start(); });
            foreach (var thread in threadList) {
                thread.Start();
            }
            foreach (var thread in threadList) {
                thread.Join();
            }
            watch.Stop();

            return $"Elapsed time for Threads: {watch.ElapsedMilliseconds}";
        }

        static string RunTasks(int collectionLength, int threadCount)
        {
            var list = new ThreadSafeCollection(numberElements: collectionLength);
            Stopwatch watch = new Stopwatch();
            Task[] tasksList = new Task[threadCount];
            for (int i = 0; i < threadCount; i +=2) {
                tasksList[i] = new Task(() => list.AddRandomInts(list));
                tasksList[i+1] = new Task(() => list.PrintCollection(list));
            }

            watch.Start();
            Parallel.ForEach(tasksList, (t) => { t.Start(); });
            //foreach (var task in tasksList) {
            //    task.Start();
            //}
            Task.WaitAll(tasksList);
            watch.Stop();

            return $"Elapsed time for Tasks: {watch.ElapsedMilliseconds}";
        }
    }
}
