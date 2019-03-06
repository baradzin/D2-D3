using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Task7;

namespace Task7.Tests
{
    [TestFixture]
    public class NUnitTest
    {
        TaskMethods TaskHelper = new TaskMethods();
        /// <summary>
        /// 1. Continuation task should be executed regardless of the result of the parent task.
        /// </summary>
        [Test]
        public void Test1()
        {
            var task = Task.Factory.StartNew(TaskHelper.RunTaskWithException)
                .ContinueWith((x) => TaskHelper.RunTaskWithoutException());
            task.Wait();
        }
        //ExecutionResults 1:
        //ThreadId: 7 this task will throw exception
        //ThreadId: 7 throwing exception...
        //ThreadId: 7 this task doesn't throw exceptions, complete

        //ExecutionResults 2:
        //ThreadId: 7 this task will throw exception
        //ThreadId: 7 throwing exception...
        //ThreadId: 10 this task doesn't throw exceptions, complete

        /// <summary>
        /// 2. Continuation task should be executed when the parent task finished without success.
        /// </summary>
        [Test]
        public void Test2()
        {
            var task1 = Task.Factory.StartNew(TaskHelper.RunTaskWithException);
            var task2 = task1.ContinueWith((x) => TaskHelper.RunTaskWithoutException(), TaskContinuationOptions.OnlyOnFaulted);
            try {
                task1.Wait();
            }
            catch(AggregateException exc) {
                //TaskContinuationOptions.OnlyOnFaulted throws exception 
                //if parent task didn't throw exception. That's why we should 
                //catch exception to synchronize tasks. Or we should catch CancellationException. 
            }
            finally {
                if (!task2.IsCanceled) { //This property isn't set if first task wasn't complete yet.
                    task2.Wait(); // There will be exception if first task wasn't failed
                }
            }
        }

        //ExecutionResults if not failed:
        //ThreadId: 9 this task will throw exception
        //ThreadId: 9 throwing exception...

        //ExecutionResults if failed
        //ThreadId: 10 this task will throw exception
        //ThreadId: 10 throwing exception...
        //ThreadId: 10 this task doesn't throw exceptions, complete


        /// <summary>
        /// 3. Continuation task should be executed when the parent task would be
        /// finished with fail and parent task thread should be reused for continuation
        /// </summary>
        [Test]
        public void Test3()
        {
            var task = Task.Factory.StartNew(TaskHelper.RunTaskWithException)
                .ContinueWith((x) => TaskHelper.RunTaskWithoutException(), TaskContinuationOptions.ExecuteSynchronously);
            task.Wait();
        }
        //But it's not 100% result, there is very low possibility to execute second task in another thread
        //https://devblogs.microsoft.com/pfxteam/when-executesynchronously-doesnt-execute-synchronously/
        //ExecutionResults:
        //ThreadId: 8 this task will throw exception
        //ThreadId: 8 throwing exception...
        //ThreadId: 8 this task doesn't throw exceptions, complete

        /// <summary>
        /// 4. Continuation task should be executed outside 
        /// of the thread pool when the parent task would be cancelled
        /// </summary>
        [Test]
        public void Test4()
        {
            var task = Task.Factory.StartNew(TaskHelper.RunTaskWithException, TaskCreationOptions.RunContinuationsAsynchronously)
                .ContinueWith((x) => TaskHelper.RunTaskWithoutException());
            task.Wait();
        }
        //ExecutionResults:
        //ThreadId: 9 this task will throw exception
        //ThreadId: 9 throwing exception...
        //ThreadId: 10 this task doesn't throw exceptions, complete
    }
}
