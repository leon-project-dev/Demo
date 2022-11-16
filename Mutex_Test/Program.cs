using System;
using System.Threading;

namespace Mutex_Test
{
    internal class Program
    {
        public delegate int Job(int threadID);
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello World!");

            Thread thread = new Thread(()=> { Test(1); });
            thread.Start();

            Job job = Test;
            var result = job.BeginInvoke(10, (e) => { }, null);
            if(!result.IsCompleted)
                result.AsyncWaitHandle.WaitOne();

            int s = job.EndInvoke(result);
            Console.WriteLine($"{s}");

            Console.WriteLine($"main Thread: {Thread.CurrentThread.ManagedThreadId}");
        }

        private static int Test(int threadID)
        {
            Thread.Sleep(2000);
            Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}");
            return threadID;
        }

    }
}
