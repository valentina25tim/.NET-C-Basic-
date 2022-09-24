using System;
using System.Threading;
using System.Threading.Tasks;

namespace RaceCars_1_win.RaceCars_1_win
{
    public interface IPlane
    {
        public string NamePilote { get; set; }
        public string TypePlane { get; init; }
        public int SpeedPlane { get; init; }
        public ConsoleColor ColorPlane { get; init; }

        Task Fly(int positionY, int distance, int way, int lengthPlane,
            CancellationToken token = default);

        Task Name(int positionY);
    }
}
