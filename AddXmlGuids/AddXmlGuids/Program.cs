using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AddXmlGuids
{
    class Program
    {
        static void Main(string[] args)
        {
            bool fixFlag = false;
            string basePath = "D:\\Projects\\BertScout2019\\BertScout2019XmlData\\EmbeddedResources";
            foreach (string filename in Directory.GetFiles(basePath))
            {
                fixFlag = false;
                if (filename.EndsWith(".xml"))
                {
                    string[] lines = File.ReadAllLines(filename);
                    for (int i = 0; i < lines.GetLength(0); i++)
                    {
                        if (lines[i] == "    <Id>0</Id>")
                        {
                            lines[i] = $"    <Uuid>{Guid.NewGuid()}</Uuid>";
                            Console.WriteLine(lines[i]);
                            fixFlag = true;
                        }
                    }
                    if (fixFlag)
                    {
                        File.Move(filename, filename + ".old");
                        File.WriteAllLines(filename, lines);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
