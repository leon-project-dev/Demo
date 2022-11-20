using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace TaskDemo
{
    public class CustomAwitable : INotifyCompletion, ICriticalNotifyCompletion
    {
        public bool IsCompleted { get; set; }
        public string GetResult()
        {

            return "";
        }

        public CustomAwitable GetAwaiter()
        {
            
            return this;
        }

        public CustomAwitable()
        {

        }

        public void OnCompleted(Action continuation)  //  这个是调用 await 后续的工作
        {

            Task.Run(() => 
            {
                Thread.Sleep(2000);
                if (continuation != null)
                    continuation();
            });
         //   IsCompleted = true;
            
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            
        }
    }
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TaskCompletionSource tcs = new TaskCompletionSource();
            var task = Task.Factory.StartNew(() => { });
            var data = await new CustomAwitable();       // awit 会创建一个状态机， 然后调用 GetAwiter 然后判断是否完成
            
            Thread.Sleep(10000);
        }
    }
}
