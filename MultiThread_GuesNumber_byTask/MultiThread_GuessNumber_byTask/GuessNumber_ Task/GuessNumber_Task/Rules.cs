using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessNumber__Task.GuessNumber_Task
{
    public class Rules : IRules
    {
        private const int _minNumb = 1;
        private const int _maxNumb = 111;
        private const int _countNumb = _maxNumb - _minNumb;

        public static HashSet<int> NumberSet;
        public static int GuessedNumb;

        static Rules()
        {
            NumberSet = AllNumberSet(_minNumb, _maxNumb, _countNumb);
            AnswersSet = AddToAnswSet(Answer);
            GuessedNumb = GuessNumb(_minNumb, _maxNumb);
        }
        public static HashSet<int> AnswersSet { get; set; }
        public static int Answer { get; set; }

        public static void PrintHSco()
        {
            $"\tMy Secret Number is = {GuessedNumb} =".PrintAtWihtColor(0, 0, ConsoleColor.White);

            int n = 1;
            int posYco = 6;
            "My HSet".PrintAtWihtColor(1, posYco - 1, ConsoleColor.White);

            foreach (var num in NumberSet)
            {
                var _n = n;
                var _posYco = posYco;
                $"{n} - {num}".PrintAtWihtColor(1, _posYco, ConsoleColor.White);
                posYco++;
                n++;
            }
        }

        public static HashSet<int> AllNumberSet(int min, int max, int countNumb)
        {
            HashSet<int> numbSet = new HashSet<int>();
            do
            {
                numbSet.Add(new Random().Next(min, max));
            }
            while (numbSet.Count != countNumb);
            return numbSet;
        }

        public static HashSet<int> AddToAnswSet(int answ)
        {
            HashSet<int> answSet = new HashSet<int>();
            answSet.Add(answ);
            return answSet;
        }

        public static int GuessNumb(int min, int max)
        {
            int guessNumb = new Random().Next(min, max);
            return guessNumb;
        }
    }
}
