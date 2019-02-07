using BertScout2019XmlData;
using System;
using System.IO;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream stream = EmbeddedData.XmlDataStoreEventTeams();
            using (var reader = new StreamReader(stream))
            {
                Console.WriteLine(reader.ReadToEnd());
            }
            Console.ReadLine();
        }
    }
}
