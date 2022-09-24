using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuessNumber__Task.GuessNumber_Task
{
    public interface IGamer
    {
        public string Name { get; init; }
        public ConsoleColor Color { get; set; }
        public Task GamerStart(int posX, CancellationTokenSource token);
    }

    public interface IRules
    {
        public static int Answer { get; set; }

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
