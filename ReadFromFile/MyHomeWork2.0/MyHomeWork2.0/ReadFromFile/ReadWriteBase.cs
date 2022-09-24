using System;
using System.Text;
using System.IO;

namespace MyHomeWork2._0.ReadFromFile
{
    public class ReadFromFile
    {
        public string ReadText { get; set; }
        public string LinkToFile { get; set; }
        public ReadFromFile(string LinkToFile)
        {
            ReadText = System.IO.File.ReadAllText(@LinkToFile);
        }
    }

    public class WriteToFile
    {
        public string AddText { get; set; }

        public WriteToFile(string LinkToFile, string AddText)
        {
            string addText = AddText + Environment.NewLine;
            File.AppendAllText(@LinkToFile, addText, Encoding.UTF8);
        }
    }
}
