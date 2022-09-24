using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MyHomeWork2._0.ReadFromFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string thisLinkFrrom = "C:\\C#project\\.NET C# Basic\\.NET-C-Basic-\\ReadFromFile\\MyHomeWork2.0\\MyHomeWork2.0\\ReadFromFile\\FileFrom.txt";

            char n = '"';
            string thisTextAdd = $"{n}Sherlock Holmes{n}, Sir Arthur Conan Doyle.";


            string thisLinkAddDoctionaryTo = "C:\\C#project\\.NET C# Basic\\.NET-C-Basic-\\ReadFromFile\\MyHomeWork2.0\\MyHomeWork2.0\\ReadFromFile\\FileTo.txt";


            WriteLinesToFile writeNewLines = new WriteLinesToFile(thisLinkFrrom, thisTextAdd);


            ReadAllFromFile read = new ReadAllFromFile(thisLinkFrrom);

            read.PrintText();

            read.AddToDictionary();

            read.AddDictionaryToNewFile(thisLinkAddDoctionaryTo);








            //read.AddToHashSet();
        }
    }
}
