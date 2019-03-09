using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static int depth = 10;
        
        static void Main(string[] args)
        {
            Run();
        }

        public static void Run()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            Console.WriteLine("If you want to stop application, type q");
            ConsoleReadLine(cancelTokenSource).Wait();
        }

        public static Task CalculateNumber(int N, CancellationToken token)
        {
            return Task.Run(() => {
                double res = 0;
                for (int i = 1; i < N; i++) {
                    if (token.IsCancellationRequested) { return; }
                    Thread.Sleep(500);
                    res += i;
                }
                Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} SUM: {res}");
            });
        }

        public static async Task ConsoleReadLine(CancellationTokenSource cancelTokenSource, Task calculatingTask = null)
        {
            Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId} : Enter Number or q");
            string number = Console.ReadLine();
            if (number.Equals("q")) {
                if(calculatingTask != null) {
                    calculatingTask.Wait();
                }
                return;
            }

            cancelTokenSource.Cancel();
            CancellationTokenSource  newCancellationSource = new CancellationTokenSource();
            int n = int.Parse(number);
            var task = CalculateNumber(n, newCancellationSource.Token);
            if(depth > 0) {
                depth--;
                await ConsoleReadLine(newCancellationSource, task);
            }            
        }
    }
}
