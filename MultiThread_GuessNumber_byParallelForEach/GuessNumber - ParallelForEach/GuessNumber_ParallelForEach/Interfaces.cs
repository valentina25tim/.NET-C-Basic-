using System;
using System.Collections.Generic;


namespace GuessNumber___ParallelForEach.GuessNumber_ParallelForEach
{
    public interface IGamer
    {
        public string Name { get; init; }
        public ConsoleColor Color { get; set; }
    }

    public interface IRules
    {
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

        public static int GuessedNumb(int min, int max)
        {
            int guessNumb = new Random().Next(min, max);
            return guessNumb;
        }
    }
}
