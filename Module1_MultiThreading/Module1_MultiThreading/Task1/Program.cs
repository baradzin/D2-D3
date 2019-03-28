using System;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new Task[100];
            for (int i = 0; i < 100; i++) {
                tasks[i] = new Task((object param) => {
                    var taskNumber = param;
                    for (var j = 1; j <= 10; j++) {
                        Console.WriteLine($"Task #{param} - {j}");
                    }
                }, i);
            }

            Parallel.ForEach(tasks, (t) => { t.Start(); });
            Task.WaitAll(tasks);

            Console.ReadKey();
        }
    }
}
