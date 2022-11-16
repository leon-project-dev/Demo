using System;
using System.Threading;

namespace InterLockedTest
{
    public interface CounterBase
    {
        int Increment();
        int Decrement();
    }

    public class Counter : CounterBase
    {
        public int CounterVal => counter;
        private int counter;
        public int Decrement()
        {
            counter--;
            return counter;
        }

        public int Increment()
        {
            counter++;
            return counter;
        }

        public Counter(int start = 0)
        {
            counter = start;
        }
    }

    public class InterLockedCounter : CounterBase
    {
        public int Counter => counter;
        private int counter;
        public int Decrement()
        {
            return Interlocked.Decrement(ref counter);
        }

        public int Increment()
        {
            return Interlocked.Increment(ref counter);            
        }

        public InterLockedCounter(int start = 0)
        {
            counter = start;
        }

        
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i < 10; i++)
            {
                Thread thread1 = new Thread(() =>
                {
                    Counter counter = new Counter();
                    for (int i = 0; i < 1000000; i++)
                    {
                        counter.Increment();
                        counter.Decrement();
                    }
                    Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, ThreadPoolThread：{Thread.CurrentThread.IsThreadPoolThread}, Counter: {counter.CounterVal}");                    
                });

                Thread thread2 = new Thread(() =>
                {
                    InterLockedCounter counter = new InterLockedCounter();
                    for (int i = 0; i < 1000000; i++)
                    {
                        counter.Increment();
                        counter.Decrement();
                    }
                    Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, ThreadPoolThread：{Thread.CurrentThread.IsThreadPoolThread} Counter: {counter.Counter}");                    
                });

                thread1.Start();
                thread2.Start();
            }
            
        }
    }
}
