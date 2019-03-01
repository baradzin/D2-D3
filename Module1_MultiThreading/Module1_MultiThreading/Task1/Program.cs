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
                    var taskNumber = (int)param;
                    for(var j = 0; j < 1000; j++)
                    {
                        Console.WriteLine($"Task #{taskNumber} - {j}");
                    }                    
                }, i);
            }

            Parallel.ForEach<Task>(tasks, (t) => { t.Start(); });
            Task.WaitAll(tasks);

            Console.ReadKey();
        }
    }
}
