using System;

namespace MyHomeWork2._0.ReadFromFile
{
    public class WriteLinesToFile : WriteToFile
    {
        private string _addTextToFile;

        public WriteLinesToFile(string LinkToFile, string AddText) : base(LinkToFile, AddText)
        {
            _addTextToFile = AddText;
        }
    }
}
