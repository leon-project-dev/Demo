using System;
using System.Threading;

namespace 線程池等待超時
{
    internal class Program
    {
        static void Woker(CancellationToken token, ManualResetEvent resetEvent)
        {
            for (int i = 0; i < 6; i++)
            {
                if (token.IsCancellationRequested)
                    break;

                Thread.Sleep(1000);
            }

            resetEvent.Set();
        }

        static void TimeOut(bool isCancel, CancellationTokenSource source)
        {
            if (isCancel)
            {
                source.Cancel();
                Console.WriteLine("任務已經取消");
            }
            else
                Console.WriteLine("任務已經完成");
        }

        static void StartWorker(int timeout)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ManualResetEvent resetEvent = new ManualResetEvent(false);
            var waitHandle = ThreadPool.RegisterWaitForSingleObject(resetEvent, (s, timeout) => TimeOut(timeout, cancellationTokenSource), null, timeout, true);
            ThreadPool.QueueUserWorkItem((s) => { Woker(cancellationTokenSource.Token, s as ManualResetEvent); }, resetEvent);

            Thread.Sleep(timeout + 2000);
            waitHandle.Unregister(resetEvent);
        }

        static void Main(string[] args)
        {
            StartWorker(5000);
            StartWorker(7000);
            Console.WriteLine("Hello World!");
        }
    }
}
