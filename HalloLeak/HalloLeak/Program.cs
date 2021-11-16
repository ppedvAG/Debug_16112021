﻿using System;
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
            //FillLongs();

            Thread.Sleep(1000);

            PferdeErstellen();

            Thread.Sleep(1000);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Thread.Sleep(1000);
            PferdeErstellen();

            Console.WriteLine("Ende");
            Console.ReadKey();
        }

        private static void FillLongs()
        {
            List<DateTime> list = new List<DateTime>();
            var dd = DateTime.Now;
            while (true)
            {
                list.Add(dd);
            }
        }

        private static void PferdeErstellen()
        {
            List<Pferd> list = new List<Pferd>();
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
