using System;
using System.Threading;
using System.Threading.Tasks;

namespace HalloTPL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            //Parallel.Invoke(Zähle, Zähle, Zähle, Zähle, () => Zähle(), Zähle);

            //Parallel.For(0, 1000000, i => Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}"));

            var cts = new CancellationTokenSource();
            cts.Cancel();

            var t1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("T1 gestartet");
                Thread.Sleep(300);
                //throw new ExecutionEngineException();

                //if (cts.IsCancellationRequested)
                //{
                //    cts.Token.ThrowIfCancellationRequested();
                //}

                Console.WriteLine("T1 fertig");
            },cts.Token);

            t1.ContinueWith(t => Console.WriteLine("T1 continue (immer)"), TaskContinuationOptions.None);
            t1.ContinueWith(t => Console.WriteLine("T1 continue (OK)"), TaskContinuationOptions.OnlyOnRanToCompletion);
            t1.ContinueWith(t => Console.WriteLine($"T1 continue (ERROR) {t.Exception.InnerException.Message}"), TaskContinuationOptions.OnlyOnFaulted);
            t1.ContinueWith(t => Console.WriteLine($"T1 continue (cancel) "), TaskContinuationOptions.OnlyOnCanceled);
            
            //t1.Start();

            Console.WriteLine("Ende");
            Console.ReadLine();
        }

        private static void Zähle()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}");
            }
        }
    }
}
