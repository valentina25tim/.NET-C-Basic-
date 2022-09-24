using System;
using System.Collections.Generic;


namespace GuessNumber___ParallelForEach.GuessNumber_ParallelForEach
{
    public class Rules : IRules
    {
        private const int _minNumb = 1;
        private const int _maxNumb = 101;
        private const int _countNumb = _maxNumb - _minNumb;

        private int _n = 1;
        private int _posYco = 6;

        public static int GuessNumb;
        public static HashSet<int> NumberSet;

        static Rules()
        {
            NumberSet = AllNumberSet(_minNumb, _maxNumb, _countNumb);
            GuessNumb = GuessedNumb(_minNumb, _maxNumb);
            AnswerSet = new HashSet<int>();
        }
        public static HashSet<int> AnswerSet { get; set; }

        public void PrintHSco()
        {
            $"\tMy Secret Number is = {Rules.GuessNumb} =".PrintAtWihtColor(0, 0, ConsoleColor.White);
            "My HSet".PrintAtWihtColor(1, _posYco - 1, ConsoleColor.White);

            foreach (var num in NumberSet)
            {
                $"{_n} - {num}".PrintAtWihtColor(1, _posYco, ConsoleColor.White);
                _posYco++; _n++;
            }
        }
        public static HashSet<int> AllNumberSet(int min, int max, int countNumb)
        {
            HashSet<int> numbSet = new HashSet<int>();
            do
            {
                numbSet.Add(new Random().Next(min, max));
            } while (numbSet.Count != countNumb);

            return numbSet;
        }
        public static HashSet<int> AddToAnswSet(int answ)
        {
            HashSet<int> answSet = new HashSet<int>();
            answSet.Add(answ);
            return answSet;
        }
        public static int GuessedNumb(int min, int max)
        {
            int guessNumb = new Random().Next(min, max);
            return guessNumb;
        }

    }
}
