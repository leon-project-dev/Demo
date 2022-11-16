using System;
using System.Diagnostics;
using System.Threading;

namespace ThreadsTest
{
    internal class Program
    {
        static void Printf()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            long total = 0;
            for(int i = 0; i < 2; i++)
            {
                total += i;
                Console.WriteLine(total);
                Thread.Sleep(5000);
            }
            stopwatch.Stop();
            Console.WriteLine($"ThreadID: { Thread.CurrentThread.ManagedThreadId} - { Thread.CurrentThread.Name} use Time: {stopwatch.ElapsedMilliseconds }");
        }
        static void Main(string[] args)
        {
            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(1);
            Thread thread = new Thread(Printf);
            thread.Name = "Thread1 - STA";
            thread.Priority = ThreadPriority.Lowest;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            Thread thread2 = new Thread(Printf);
            thread2.Name = "Thread2 - MTA";
            thread.Priority = ThreadPriority.Highest;
            thread2.SetApartmentState(ApartmentState.MTA);
            thread2.Start();

            //Barrier

            Printf();
            
            Console.WriteLine("Hello World!");

            //thread.Join();
            //thread2.Join();
        }
    }
}
