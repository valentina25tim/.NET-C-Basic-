using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessNumber__Task.GuessNumber_Task
{
    public static class Helper
    {
        public static void PrintAtWihtColor(this string message, int x, int y, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(message);
            Console.ResetColor();
        }
        public static HashSet<int> DifBetween(HashSet<int> fullNumb, HashSet<int> answGamer)
        {
            var diff = new HashSet<int>();

            for (var i = 0; i < fullNumb.Count; i++)
            {
                if (!answGamer.Contains(fullNumb.ElementAt(i)))
                {
                    diff.Add(fullNumb.ElementAt(i));
                }
            }
            return diff;
        }

        public static int GetRandomBetween(int min, int max)
        {
            return new Random().Next(min, max);
        }
        public static int GetMaxLength(string[] array)
        {
            int tmp1 = 0;
            int indLongerString = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Length > tmp1)
                {
                    tmp1 = array[i].Length;
                    indLongerString = i;
                }
            }
            int longerString = array[indLongerString].Length;
            return longerString;
        }
    }
}
