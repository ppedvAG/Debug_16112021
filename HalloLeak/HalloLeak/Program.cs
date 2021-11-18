using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HalloLeak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hallo");
            FillLongs();

            Thread.Sleep(1000);


            Console.WriteLine("Mache pferde:");
            Console.ReadLine();
            var p1 = new PferdeManager();
            Console.WriteLine("Pferde gemacht");
            Console.ReadLine();
            p1 = null;
            Console.WriteLine("Pferde = NULL");
            Console.ReadLine();


            //Console.WriteLine(wr.IsAlive);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            //Console.WriteLine(wr.IsAlive);

            Console.WriteLine("GC zeug fertig");


            Thread.Sleep(1000);
            var p2 = new PferdeManager();

            Console.WriteLine("Ende");
            Console.ReadKey();
        }

        private static void FillLongs()
        {
            List<List<long>> list = new List<List<long>>();
            for (int i = 0; i < 1000000; i++)
            {
                var l = new List<long>();
                for (int j = 0; j < 1000000; j++)
                {
                    l.Add(i + j);
                }
                list.Add(l);
            }
        }


        static WeakReference wr = null;

    }

    class PferdeManager
    {
        List<Pferd> list = new List<Pferd>();
        public PferdeManager()
        {


            //wr = new WeakReference(list);
            for (int i = 0; i < 100000; i++)
            {
                var p = new Pferd() { Name = $"Pferd #{i:00000000}" };
                list.Add(p);
                //Console.WriteLine($"{p.Name} added");
                //Thread.Sleep(10);

            }
        }
    }


    public class Pferd
    {
        public string Name { get; set; }
    }
}
