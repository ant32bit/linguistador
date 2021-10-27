using System;
using System.IO;
using Linguistador.Models;
using Linguistador.Repositories;
using Linguistador.Factories;


namespace Linguistador
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = (uint)1000000;
            var seed = new Random().Next();
            
            if (args.Length > 1)
                size = uint.Parse(args[1]);

            if (args.Length > 2)
                seed = int.Parse(args[2]);
            
            var nounBank = new NounBank();
            var verbBank = new VerbBank();
            var paragraphFactory = new ParagraphFactory(nounBank, verbBank);
            var selector = new RandomSelector(seed);

            var dataset = new DataSet(paragraphFactory, selector, size);

            var start = DateTime.UtcNow;
            
            using (var fh = File.OpenWrite("linguistador.txt"))
            {
                foreach(var d in dataset) 
                {
                    var line = $"{d.Key.ToString()}: {d.Value}\n";
                    fh.Write(System.Text.Encoding.ASCII.GetBytes(line));
                }
            }

            var end = DateTime.UtcNow;

            var duration = end - start;
            Console.WriteLine($"{size} paragraphs - {duration.TotalSeconds}s");
        }
    }
}
