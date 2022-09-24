using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace GuessNumber___ParallelForEach.GuessNumber_ParallelForEach
{

    public class StartGame
    {
        private static ServiceProvider _serviceProvider;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private static readonly object _locker = new();

        private static string[] _nameGamers;
        private static List<BaseGamer> gamersList;

        private const int posY = 6;
        private int posX1 = 2;

        public StartGame(string[] nameGamers)
        {
            _nameGamers = new string[nameGamers.Length];

            for (var i = 0; i < nameGamers.Length; i++)
                _nameGamers[i] = nameGamers[i];
        }

        static BaseGamer GamersSet(int numbGamer, string[] nameGamers)
        {
            var setProvsder = _serviceProvider.GetService<IRules>();

            return numbGamer switch
            {
                0 => new GamerQueue1(ConsoleColor.Magenta, setProvsder) { Name = nameGamers[0] },
                1 => new GamerRand2(ConsoleColor.DarkRed, setProvsder) { Name = nameGamers[1] },
                2 => new GamerRandMemory3(ConsoleColor.Red, setProvsder) { Name = nameGamers[2] },
                3 => new GamerRandSly4(ConsoleColor.DarkYellow, setProvsder) { Name = nameGamers[3] },
                4 => new GamerRandMemorySly5(ConsoleColor.DarkCyan, setProvsder) { Name = nameGamers[4] },
                _ => null
            };
        }

        public void Start()
        {
            gamersList = new List<BaseGamer>(_nameGamers.Length);
            _serviceProvider = new ServiceCollection().AddSingleton<IRules, Rules>().BuildServiceProvider();

            int lengthCell = Helper.GetMaxLength(_nameGamers);
            int posX = lengthCell + 5;

            var numb = new Rules();
            numb.PrintHSco();

            for (var i = 0; i < _nameGamers.Length; i++)
            {
                gamersList.Add(GamersSet(i, _nameGamers));

                $"{gamersList[i].Name}".PrintAtWihtColor(posX, 5, ConsoleColor.Blue);

                posX += lengthCell + 3;
            }

            var parallelCount = new ParallelOptions()
            {
                MaxDegreeOfParallelism = gamersList.Count
            };

            Parallel.ForEach(gamersList, parallelCount, gamer =>
            {
                StartGuess(gamer, posX1 += lengthCell + 3, posY, _cts);
                _cts.Cancel();
            });
        }

        static void StartGuess(BaseGamer gamer, int posX, int posY, CancellationTokenSource cts)
        {
            var sw = Stopwatch.StartNew();

            if (!cts.IsCancellationRequested)
            {
                int gamerAnsw = gamer.GenerationAnswer();

                if (gamerAnsw == Rules.GuessNumb)
                {
                    cts.Cancel();
                    sw.Stop();

                    lock (_locker)
                    {
                        $"{Rules.GuessNumb}".PrintAtWihtColor(posX, posY, ConsoleColor.White);
                        $"   ==>  Winner is {gamer.Name} - {Rules.GuessNumb}  <==".PrintAtWihtColor(0, 2, ConsoleColor.Yellow);
                        $"\tTime: {((sw.Elapsed.TotalSeconds))} sec.".PrintAtWihtColor(0, 3, ConsoleColor.White);
                    }
                    return;
                }
                else
                {
                    Rules.AnswerSet.Add(gamerAnsw);

                    lock (_locker)
                    {
                        $"{gamerAnsw}".PrintAtWihtColor(posX, posY, gamer.Color);
                        posY++;
                    }

                    Thread.Sleep(50);

                    StartGuess(gamer, posX, posY, cts);
                }
            }
            else
            {
                cts.Cancel();
                return;
            }
        }

       
    }
}
