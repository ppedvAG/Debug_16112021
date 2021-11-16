using System;

namespace HalloDebugger
{
    internal class Program
    {
        static void Main(string[] args)
        {

#if DEBUG
            Console.WriteLine("Debug version");
#else
            Console.WriteLine("Evtl. Release");
#endif


            Console.WriteLine("Hallo Welt");
            IsCool();
            StarteCoolesZeug();

            Console.WriteLine("Ich warte...");
            Console.ReadKey();

            new MyClass2().Lalala();
            new MyClass().Lalala();

            Console.WriteLine("Ende");
            Console.ReadKey();
        }

        private static void StarteCoolesZeug()
        {
            StarteZähler();
        }

        private static void StarteZähler()
        {
            for (int i = 0; i < 10; i++)
            {

                Console.WriteLine($"{i}");
            }
        }

        static bool IsCool()
        { return DateTime.Now.Day > 5; }
    }


    /// <summary>
    /// die beste klasse der welt
    /// </summary>
    public class MyClass
    {


        public virtual void Lalala()
        {
            Console.WriteLine("base lala");
        }
    }

    public class MyClass2 : MyClass
    {
        public override void Lalala()
        {
            Console.WriteLine("besseres lalala");

            var result = 12 / int.Parse("0");
        }
    }
}
