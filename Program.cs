using ABP_Dal.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP_Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser parsing = new Parser();
            parsing.PrintAtConsole();
        }
    }
}
