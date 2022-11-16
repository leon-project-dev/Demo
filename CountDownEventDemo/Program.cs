using System;
using System.Threading;

namespace CountDownEventDemo
{
    internal class Program
    {
        public static void Worker(int jobs)
        {
            CountdownEvent countdownEvent = new CountdownEvent(jobs);
            for(int i = 0; i < jobs; i++)
            {
                ThreadPool.QueueUserWorkItem((stat) => 
                {
                    Thread.Sleep(100);
                    countdownEvent.Signal();
                });
            }

            Console.WriteLine("Wait");
            countdownEvent.Wait();      // 等待 count 全部用完
        }
        static void Main(string[] args)
        {
            Worker(100);

            Thread.Sleep(10000);
        }
    }
}
