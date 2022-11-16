using System;
using System.Threading;

namespace CancelLocalTokenTest
{
    
    internal class Program
    {
        static void CaneclToken1(CancellationToken token)
        {
            for (int i = 0; i < 5; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine($"CaneclToken1 Task Cancel {i}");
                    return;
                }
                // do something
                Thread.Sleep(1000);
                Console.WriteLine($"CancelToken1 --->Task {i} oK");
            }
        }

        static void CancelToken2(CancellationToken token)
        {
            for(int i = 0;i < 5; i++)
            {
                try
                {
                    if (token.IsCancellationRequested)
                        throw new InvalidOperationException("任務已經取消");

                    Console.WriteLine($"CancelToken2 --->Task {i} oK");
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"CaneclToken2 Task Cancel {i}");
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
        }
        
        static void CancelToken3(CancellationToken token)
        {
            var isCanel = false;
            token.Register(() => { isCanel = true;  });

            for(int i = 0; i < 5; i++)
            {
                if (isCanel)
                {
                    Console.WriteLine($"CaneclToken3 Task Cancel {i}");
                    break;
                }

                Thread.Sleep(1000);
                Console.WriteLine($"CancelToken3 --->Task {i} oK");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CancellationTokenSource cts = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem((e) => { CaneclToken1(cts.Token); }); 
            Thread.Sleep(2000);
            cts.Cancel();

            cts = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem((e) => { CancelToken2(cts.Token);  });            
            Thread.Sleep(3000);
            cts.Cancel();

            cts = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem((e) => { CancelToken3(cts.Token); });
            Thread.Sleep(4000);
            cts.Cancel();

            ManualResetEvent resetEvent = new ManualResetEvent(false);
            resetEvent.WaitOne();
           // ThreadPool.RegisterWaitForSingleObject(resetEvent, )

        }
    }
}
