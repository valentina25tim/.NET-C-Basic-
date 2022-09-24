using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Concurrent;

namespace WineStorage_12._05
{
    public enum Colors { White, Red, Rose }
    public enum Strong { dry, semisweet, sweet, sparkling, cuvee, brut }
  

    public class Wine
    {
        public string[] years = new string[] { "1985", "2019", "2008", "1997", "1989", "2019", "2001" };
        public string Color { get; set; }// properties
        public string StrongBody { get; set; }
        public string Year { get; set; }

        List<Wine> wine = new List<Wine>();

        string key;

        public void List()
        {
            wine.Add(new Wine() { Color = Convert.ToString(Colors.White), StrongBody = Convert.ToString(Strong.dry), Year = years[0] });
            wine.Add(new Wine() { Color = Convert.ToString(Colors.Rose), StrongBody = Convert.ToString(Strong.sparkling), Year = years[1] });
            wine.Add(new Wine() { Color = Convert.ToString(Colors.Red), StrongBody = Convert.ToString(Strong.semisweet), Year = years[2] });
            wine.Add(new Wine() { Color = Convert.ToString(Colors.Rose), StrongBody = Convert.ToString(Strong.cuvee), Year = years[3] });
            wine.Add(new Wine() { Color = Convert.ToString(Colors.Red), StrongBody = Convert.ToString(Strong.brut), Year = years[4] });
            wine.Add(new Wine() { Color = Convert.ToString(Colors.White), StrongBody = Convert.ToString(Strong.dry), Year = years[5] });
            wine.Add(new Wine() { Color = Convert.ToString(Colors.White), StrongBody = Convert.ToString(Strong.dry), Year = years[6] });
        }

        public void Serch()
        { bool serch = false;
            do
            {
                Console.WriteLine("\t== Wine List ==\n");
                foreach (Wine item in wine) Console.WriteLine(" " + item.Color + " - " + item.StrongBody + " - " + item.Year);

                Console.Write("\n\tSerch in Wine List a bottle. Input in a search string -- >");
                
                serch = true;
                SerItem(); 

            } while (serch != false);
        }
        private void SerItem()
        {
            key = Convert.ToString(Console.ReadLine());
            
            List<Wine> found = wine.FindAll(w => w.Color == key || w.StrongBody == key || w.Year == key);
            Console.WriteLine();

            if (found != null)
            {
                foreach (Wine w in found)
                    Console.WriteLine(" " + w.Color + " - " + w.StrongBody + " - " + w.Year);
                Console.ReadKey(); Console.Clear();
            }
        }
    }
}
