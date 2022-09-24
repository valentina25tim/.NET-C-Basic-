using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Concurrent;


namespace WineStorage_12._05
{
    internal class Program
    {
        static void Main(string[] args)
        {

            DateTime[] years = new DateTime[]
            {
              new (1985,1,1),  new (2019,1,1),  new (2008,1,1),  new (1997,1,1),  new (1989,1,1),  new (2019,1,1),  new (2001,1,1)
            };

            Wine[] wineArray = new Wine[] //  1 - данные для массива
            {
                new Wine(Colors.White, Strong.dry,  years[0]),
                new Wine(Colors.Rose, Strong.sparkling, years[1]),
                new Wine(Colors.Red, Strong.semisweet, years[2]),
                new Wine(Colors.Rose, Strong.cuvee, years[3]),
                new Wine(Colors.Red, Strong.brut, years[4]),
                new Wine(Colors.White, Strong.dry, years[5]),
                new Wine(Colors.White, Strong.dry, years[6]),
            };
            WineStorage wineList = new WineStorage(wineArray);// 4 - получение перезаписанного массива
            
            bool serch = false;

            do
            {
                Console.WriteLine("\t == Wine List ==\n");

                foreach (Wine w in wineList) // 5 - переход на GetEnumerator // 12,14... - получение 1 ого эл-та wineList с нов массива и вывод на консоль 
                    Console.WriteLine($" {w.colors} - {w.strong} - {w.year.Year}");// ****************   .YEAR !!

#region SERCH 
 ////////////Bubble SORT YEAR

                //    for (var t = 0; t < wineArray.Length; t++) { for (var i = 0; i < wineArray.Length; i++){ for (var j = 0; j < wineArray.Length - 1; j++){ DateTime tmp = years[j]; if (years[j + 1] < tmp) continue; years[j] = years[j + 1]; years[j + 1] = tmp; }}Console.WriteLine((years[t]).Year);}

                WineStorage wineSort = new WineStorage(wineArray);

                Console.WriteLine("\n\t == Wine List (sorted) ==\n"); 
                

                for (var t = 0; t < wineArray.Length; t++) //  ЧТОБ ИЗБЕЖАТЬ ПРОБЛЕМ ПРИ ПОВТОРЕ ГОДА
                {
                    for (var i = 0; i < wineArray.Length; i++)
                    {
                        for (var j = 0; j < wineArray.Length - 1; j++)
                        {
                            DateTime tmp = years[j];

                            if (years[j + 1] < tmp) continue;
                            years[j] = years[j + 1];
                            years[j + 1] = tmp;
                        }
                    }
                    string sortYear = Convert.ToString((years[t]).Year);

                    foreach (Wine r in wineList)
                    {
                        if (sortYear == r.colors || sortYear == r.strong || sortYear == Convert.ToString(r.year.Year))
                        {
                            Console.WriteLine($" {r.colors} - {r.strong} - {r.year.Year}");
                        }
                    }
                    //Console.WriteLine((years[t]).Year); ВЫВОД СОРТИРОВАННОГО ГОДА
                }

              
////////////  ПОИСК ПО ВВЕДЕН ЗНАЧ
                Console.Write("\n\tSerch in Wine List a bottle. Input in a search string -- >");

                serch = true;

                string key = Convert.ToString(Console.ReadLine());

                foreach (Wine r in wineList)
                {
                    if (key == r.colors || key == r.strong || key == Convert.ToString(r.year.Year))
                    {
                        Console.WriteLine($" {r.colors} - {r.strong} - {r.year.Year}");
                    }
                }
                Console.ReadKey(); Console.Clear();
            } while (serch != false);

#endregion SERCH

        }

        
        public enum Colors { White, Red, Rose }
        public enum Strong { dry, semisweet, sweet, sparkling, cuvee, brut }


        public class Wine // base clss
        {
            public Wine(Colors Color, Strong StongBody, DateTime x)// 2 - конструктор для массива, полученного в мэине
            {
                colors = Convert.ToString(Color);
                strong = Convert.ToString(StongBody);
                year = Convert.ToDateTime(x);

            }
            public string colors;
            public string strong;
            public DateTime year;
        }

        public class WineStorage : IEnumerable
        {
            private Wine[] _bottle;
            public WineStorage(Wine[] wArray) // 3 - перезапись массива с мэйна в новый 
            {
                _bottle = new Wine[wArray.Length];
                for (int i = 0; i < wArray.Length; i++)
                {
                    _bottle[i] = wArray[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)GetEnumerator();
            }
            public WineEnum GetEnumerator() // 6 - поиск запроса foreach или любой другой в Inumerator
            {
                return new WineEnum(_bottle);  // 11 - возврат нового массива
            }

        }
        public class WineEnum : IEnumerator
        {
            public Wine[] _wineRes; // 7 - полученый  массив 
            int position = -1; // 8 - начальное 0

            public WineEnum(Wine[] list) // 9 - вложение полученного массива в новый
            { _wineRes = list; }

            public bool MoveNext()
            {
                position++; //13,15... - шаг на след эл-т 
                return position < _wineRes.Length;
            }

            public void Reset()
            { position = -1; }

            object IEnumerator.Current
            {
                get
                { return Current; }
            }

            public Wine Current
            {
                get
                {
                    try
                    { return _wineRes[position]; }

                    catch (IndexOutOfRangeException)
                    { throw new InvalidOperationException(); }
                }
            }
        }
    }
}