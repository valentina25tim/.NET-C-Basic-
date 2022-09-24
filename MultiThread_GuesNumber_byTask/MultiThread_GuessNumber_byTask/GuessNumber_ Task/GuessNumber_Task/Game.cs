using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuessNumber__Task.GuessNumber_Task
{
    public class Game
    {
        public static CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly List<IGamer> _gamerList;

        public Game(string[] nameGamers)
        {
            _gamerList = new List<IGamer>(nameGamers.Length);

            _gamerList.Add(new GamerQueue1(ConsoleColor.Magenta) { Name = nameGamers[0] });
            _gamerList.Add(new GamerRand2(ConsoleColor.DarkRed) { Name = nameGamers[1] });
            _gamerList.Add(new GamerRandMemory3(ConsoleColor.Red) { Name = nameGamers[2] });
            _gamerList.Add(new GamerRandSly4(ConsoleColor.DarkYellow) { Name = nameGamers[3] });
            _gamerList.Add(new GamerRandMemorySly5(ConsoleColor.DarkGreen) { Name = nameGamers[4] });
        }

        public void Start(string[] nameGamers)
        {
            int lengthCell = Helper.GetMaxLength(nameGamers);
            int posX = lengthCell + 5;

            Rules.PrintHSco();

            foreach (var gamer in _gamerList)
            {
                var _posX = posX;
                $"{gamer.Name}".PrintAtWihtColor(_posX, 5, ConsoleColor.Blue);

                new Task(() => gamer.GamerStart(_posX, Game._cts)).Start();
                posX += lengthCell + 3;
            }
        }
    }
}
