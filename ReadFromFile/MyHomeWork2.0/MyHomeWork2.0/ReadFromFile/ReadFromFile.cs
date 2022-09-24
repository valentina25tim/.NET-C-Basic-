using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MyHomeWork2._0.ReadFromFile
{
    public class ReadAllFromFile : ReadFromFile
    {

        public Dictionary<string, int> _wordDictionary = new Dictionary<string, int>();

        private string _fullTextFromFile;

        public ReadAllFromFile(string LinkToFile) : base(LinkToFile)
        {
            _fullTextFromFile = ReadText;
        }
        public void PrintText()
        {
            Console.WriteLine(_fullTextFromFile);
        }

        public void AddToDictionary()
        {
            _fullTextFromFile = ReadText.ToLower();

            string[] allWordsArray = _fullTextFromFile.Split(new char[] { ' ', ',', '.', ':', ';', '!', '?', '/', '\n', '\t', '"' },
                StringSplitOptions.RemoveEmptyEntries);


            foreach (var i in allWordsArray)// in Dictionary<string, int> _wordDictionary
            {
                if (_wordDictionary.TryGetValue(i, out int value))
                {
                    _wordDictionary[i] += 1;
                }
                else
                {
                    _wordDictionary.Add(i, 1);
                }
            }

            foreach (KeyValuePair<string, int> kv in _wordDictionary)
                Console.WriteLine($"{kv.Key}  -  {kv.Value}");
        }

        public void AddDictionaryToNewFile(string LinkToFile)
        {
            foreach (KeyValuePair<string, int> kv in _wordDictionary)
            {
                string addText = ($"{kv.Key}  -  {kv.Value}") + Environment.NewLine;

                File.AppendAllText(@LinkToFile, addText, Encoding.UTF8);
            }
        }






        // public HashSet<string> hashSet = new HashSet<string>();


        //public void AddToHashSet()
        //{_fullTextFromFile = ReadText.ToLower();
        //    string[] wordArray = _fullTextFromFile.Split(new char[]
        //    { ' ', ',', '.', ':', ';', '!', '?', '/', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        //    for (var i = 0; i < wordArray.Length; i++)
        //    {hashSet.Add(wordArray[i]);}
        //    foreach (var wordInSet in hashSet) Console.WriteLine(wordInSet);}
    }
}


