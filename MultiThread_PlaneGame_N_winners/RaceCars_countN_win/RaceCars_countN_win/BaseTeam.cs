using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;


namespace RaceCars_countN_win.RaceCars_countN_win
{
    public abstract class BasePlane : IPlane
    {
        public BasePlane(ConsoleColor colorPlane, int speedPlane, char direction)
        {
            Color = colorPlane;
            Speed = speedPlane;
        }
        public string Name { get; set; }
        public string Look { get; init; }
        public int NumberPlane { get; set; }
        public int Speed { get; init; }
        public ConsoleColor Color { get; init; }

        protected abstract void MovePrint(int positionY);

        private  void WinNamePrint(int positionY, Stopwatch sw)
        {
            if (Rules.StepTeam_1[NumberPlane - 1] > Rules.StepTeam_2[NumberPlane - 1])
            {
                $"Winner: == T_1 - {Game.Team_1.ElementAt(NumberPlane-1).Name} ==".PrintAtWihtColor(Game.wayPlane * 2 + 4, positionY, Game.Team_1.ElementAt(NumberPlane - 1).Color);
            }
            else if (Rules.StepTeam_1[NumberPlane - 1] < Rules.StepTeam_2[NumberPlane - 1])
            {
                $"Winner: == T_2 - {Game.Team_2.ElementAt(NumberPlane - 1).Name} ==".PrintAtWihtColor(Game.wayPlane * 2 + 4, positionY, Game.Team_2.ElementAt(NumberPlane - 1).Color);
            }
            else if (Rules.StepTeam_1[NumberPlane - 1] == Rules.StepTeam_2[NumberPlane - 1])
            {
                $"Winner: == 0 : 0 ==".PrintAtWihtColor(Game.wayPlane * 2 + 4, positionY, ConsoleColor.White);
            }
        }

        public Task NamePrint(int posY, int posX, string nameTeam)
        {
            int positionVS = Game.lengthMaxName + Game.posTeam_Y;

            lock (Game.locker)
            {
                nameTeam.PrintAtWihtColor(posX, 2, ConsoleColor.White);

                $"{NumberPlane} - {Name}".PrintAtWihtColor(posX, posY, Color);

                "VS".PrintAtWihtColor(positionVS, posY, ConsoleColor.White);
            }
            return Task.CompletedTask;
        }

        public async Task Fly(int positionY, CancellationToken cts)
        {
            var sw = Stopwatch.StartNew();

            while (!Game.cts.ElementAt(NumberPlane - 1).IsCancellationRequested)
            {
                lock (Game.locker)
                {
                    MovePrint(positionY);
                }
                if ((Init.distance - Game.lengthPlane) * 2 == Rules.StepTeam_1[NumberPlane - 1] + Rules.StepTeam_2[NumberPlane - 1])
                {
                    sw.Stop();
                    lock (Game.locker)
                    {
                        WinNamePrint(positionY, sw);
                    }

                    $"Time: {((sw.Elapsed.TotalSeconds))} sec.".PrintAtWihtColor((Game.wayPlane + Game.lengthMaxName * 2) * 2 + 10, positionY, ConsoleColor.White);
                    Game.cts.ElementAt(NumberPlane - 1).Cancel();
                }

                await Task.Delay(Speed);
            }
           
        }
    }
}