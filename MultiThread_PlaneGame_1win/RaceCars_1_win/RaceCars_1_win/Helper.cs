using System;
using System.Text;

namespace RaceCars_1_win.RaceCars_1_win
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
        public static string GetStrWithLength(this string ch, int length)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < length; i++) sb.Append(ch);

            return sb.ToString();
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
