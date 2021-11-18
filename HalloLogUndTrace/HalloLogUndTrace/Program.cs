using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloLogUndTrace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            //        Log.Logger = new LoggerConfiguration().Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
            //.WithDefaultDestructurers()
            //.WithRootName("Exception"))

            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            Log.Logger = new LoggerConfiguration().Enrich.WithAssemblyName()
                                              //.Enrich.WithExceptionDetails(new DestructuringOptionsBuilder())
                                              .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder())
                                              .Enrich.WithMachineName()
                                              .WriteTo.Debug()
                                              .WriteTo.File(formatter: new JsonFormatter(renderMessage: true), "log.txt", rollingInterval: RollingInterval.Day)
                                              .WriteTo.File("log.log", rollingInterval: RollingInterval.Day)
                                              .WriteTo.File(new CompactJsonFormatter(), "logComp.log", rollingInterval: RollingInterval.Day)
                                              .WriteTo.File(new RenderedCompactJsonFormatter(), "logRendComp.log", rollingInterval: RollingInterval.Day)
                                              .WriteTo.Console()
                                              .WriteTo.Console(new JsonFormatter(renderMessage: true))
                                              .WriteTo.Seq("http://localhost:5341")
                                              .CreateLogger();

            //Trace.AutoFlush = true;
            //Trace.Listeners.Add(new TextWriterTraceListener("log.txt"));
            //Trace.Listeners.Add(new EventLogTraceListener("Application"));

            //Trace.WriteLine("Hallo Trace");
            //Debug.WriteLine("Hallo Debug");

            Log.Information("OS Version {0}", Environment.OSVersion);

            for (int i = 0; i < 5; i++)
            {
                Log.Information("Zahl ist bei {i}", i);
                Log.Information("Zahl ist bei {doppelt}", i * 2);
                var p = new Pferd();
                Log.Information("Da steht ein Pferd auf der Flur {@pferd}", p);
            }

            Console.WriteLine("EX?");
            Console.ReadKey();
            Log.Error(new AggregateException(new OutOfMemoryException()), "Schade ");
            Log.Error("Schade {@error}", new AggregateException(new OutOfMemoryException()));

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }

    class Pferd
    {
        public string Name { get; set; } = "Ed";
        public int Gewicht { get; set; } = 456;

    }
}
