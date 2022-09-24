using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace RaceCars_1_win.RaceCars_1_win
{
    public class Plane : IPlane
    {
        private static readonly object _syncLock = new();
        public string NamePilote { get; set; }
        public string TypePlane { get; init; }
        public char DirectionPlane { get; init; }
        public int NumberPilot { get; set; }
        public int CountPilots { get; set; }
        public int LongerName { get; set; }
        public int SpeedPlane { get; init; }

        public ConsoleColor ColorPlane { get; init; }

        public Plane(ConsoleColor colorPlane, int speedPlane)
        {
            ColorPlane = colorPlane;
            SpeedPlane = speedPlane;
        }
        public Task Name(int positionY)
        {
            int positionXLelt = 1;
            int positionVS = positionXLelt + LongerName + 7;

            "TEAM 1".PrintAtWihtColor(1, 2, ConsoleColor.White);
            "TEAM 2".PrintAtWihtColor(positionVS + 5, 2, ConsoleColor.White);

            lock (_syncLock)
            {
                string initName = $"{NumberPilot} - {NamePilote}";

                switch (DirectionPlane)
                {
                    case '+':
                        initName.PrintAtWihtColor(1, positionY / 2, ColorPlane);
                        "VS".PrintAtWihtColor(positionVS, positionY / 2, ConsoleColor.White);
                        break;

                    case '-':
                        initName.PrintAtWihtColor(positionVS + 5, positionY / 2, ColorPlane);
                        break;
                }
            }
            return Task.CompletedTask;
        }

        public Task Fly(int positionY, int distance, int way, int lengthPlane, CancellationToken token = default)
        {
            var sw = Stopwatch.StartNew();

            int positionXLelt = 1;
            int positionXRight = positionXLelt + (way * 2);
            int _positionLine = positionY + 1;

            while (!token.IsCancellationRequested)
            {
                lock (_syncLock)
                {
                    switch (DirectionPlane)
                    {
                        case '+':
                            ". ".PrintAtWihtColor(positionXLelt, _positionLine, ColorPlane);
                            TypePlane.PrintAtWihtColor(positionXLelt + 2, _positionLine, ColorPlane);
                            break;

                        case '-':
                            TypePlane.PrintAtWihtColor(positionXRight - lengthPlane * 2, _positionLine - 1, ColorPlane);
                            " .".PrintAtWihtColor(positionXRight - lengthPlane, _positionLine - 1, ColorPlane);
                            break;
                    }

                    if (positionXLelt == distance - 2)
                    {
                        sw.Stop();
                        if (NumberPilot % 2 != 0)
                        {
                            $"Winner: == TEAM 1 ==  ({NumberPilot} - {NamePilote} )".PrintAtWihtColor(LongerName * 3 + 15, 2, ColorPlane);
                        }
                        else if (NumberPilot % 2 == 0)
                        {
                            $"Winner: == TEAM 2 ==  ({NumberPilot} - {NamePilote} )".PrintAtWihtColor(LongerName * 3 + 15, 2, ColorPlane);
                        }
                        $"Time: {((sw.Elapsed.TotalSeconds))} sec.".PrintAtWihtColor(LongerName * 3 + 15, 3, ColorPlane);
                        break;
                    }

                    else if (positionXLelt < distance)
                    {
                        positionXLelt++;
                        positionXRight--;
                    }
                }
                Thread.Sleep(SpeedPlane);
            }
            return Task.CompletedTask;
        }
    }
}
