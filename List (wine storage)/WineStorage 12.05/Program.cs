using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;


namespace WineStorage_12._05
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Wine wine = new Wine();// ПРОСТО LIST

            wine.List();
            wine.Serch();

        }
      
    }
}
