using System;
using System.Threading;
using System.Threading.Tasks;

namespace RaceCars_countN_win.RaceCars_countN_win
{
    public interface IPlane
    {
        public string Name { get; set; }
        public string Look { get; init; }
        public int NumberPlane { get; set; }
        public int Speed { get; init; }
        public ConsoleColor Color { get; init; }

        Task Fly(int positionY, CancellationToken cts = default);
        Task NamePrint(int posY, int posX, string nameTeam);
    }
}
