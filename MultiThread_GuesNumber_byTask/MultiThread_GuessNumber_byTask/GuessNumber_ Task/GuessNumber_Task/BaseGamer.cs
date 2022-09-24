using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace GuessNumber__Task.GuessNumber_Task
{
    public abstract class BaseGamer : IGamer
    {
        private static readonly object _locker = new();
        private int posY = 6;

        public BaseGamer(ConsoleColor color)
        {
            Color = color;
        }

        public string Name { get; init; }
        public ConsoleColor Color { get; set; }
        public int TryGuessGamer { get; set; }

        public abstract int GenerationAnswer();

        public Task GamerStart(int posX, CancellationTokenSource cst)
        {
            var sw = Stopwatch.StartNew();
            TryGuessGamer = GenerationAnswer();

            while (!Game._cts.IsCancellationRequested)
            {
                Rules.Answer = TryGuessGamer;
                Rules.AnswersSet.Add(Rules.Answer);

                if (TryGuessGamer == Rules.GuessedNumb)
                {
                    Game._cts.Cancel();
                    sw.Stop();

                    lock (_locker)
                    {
                        $"{TryGuessGamer}".PrintAtWihtColor(posX, posY, ConsoleColor.White);
                        $"   ==>  Winner is {Name} - {Rules.GuessedNumb}  <==".PrintAtWihtColor(0, 2, ConsoleColor.Yellow);
                        $"\tTime: {((sw.Elapsed.TotalSeconds))} sec.".PrintAtWihtColor(0, 3, ConsoleColor.White);
                    }
                    break;
                }
                else
                {
                    Thread.Sleep(100);
                    //Task.Delay(100);

                    lock (_locker)
                    {
                        $"{TryGuessGamer}".PrintAtWihtColor(posX, posY, Color);
                    }
                    TryGuessGamer = GenerationAnswer();
                    posY++;
                }
            }
            return Task.CompletedTask;
        }
    }
}
