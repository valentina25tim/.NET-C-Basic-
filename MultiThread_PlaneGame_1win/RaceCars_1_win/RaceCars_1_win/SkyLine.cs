using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RaceCars_1_win.RaceCars_1_win
{
    public class SkyLine
    {
        private static readonly object _syncLock = new();
        private const int distance = 30;
        private const int minSpeed = 50;
        private const int maxSpeed = 200;

        private static readonly object _syncLock1 = new();
        private readonly List<IPlane> _planeList;
        private readonly CancellationTokenSource _cts = new();

        public SkyLine(string[] _namePilote, string[] _typePlane, char[] _direction, int countPlane)
        {
            _planeList = new List<IPlane>(_namePilote.Length);

            for (var i = 0; i < _namePilote.Length; i++)
            {
                _planeList.Add(new Plane((ConsoleColor)Helper.GetRandomBetween(1, 15), Helper.GetRandomBetween(minSpeed, maxSpeed))
                {
                    NamePilote = _namePilote[i],
                    TypePlane = _typePlane[i],
                    DirectionPlane = _direction[i],
                    NumberPilot = i + 1,
                    CountPilots = _namePilote.Length,
                    LongerName = Helper.GetMaxLength(_namePilote),
                });
            }
        }

        public void Start(string[] _typePlane)
        {

            $"\t== Sky Battle ==".PrintAtWihtColor(0, 0, ConsoleColor.White);


            var task1 = new List<Task>();
            var task2 = new List<Task>();

            int posNameY = 8;
            int posPlaneY = 8 + 4;

            int lengthPlane = Helper.GetMaxLength(_typePlane);
            int wayPlane = distance + Helper.GetMaxLength(_typePlane);
            int emptySpace = Helper.GetMaxLength(_typePlane) + 2;

            "=".GetStrWithLength(wayPlane * 2 + 1)
                .PrintAtWihtColor(0, posPlaneY, ConsoleColor.White);
            "=".GetStrWithLength(wayPlane * 2 + 1)
                .PrintAtWihtColor(0, posPlaneY + (_typePlane.Length) + 1, ConsoleColor.White);


            foreach (var plane in _planeList)
            {
                var _posNameY = posNameY;
                task1.Add(new Task(() => plane.Name(_posNameY)));
                posNameY += 1;
            }

            foreach (var plane in _planeList)
            {
                var _posPlaneY = posPlaneY;

                task2.Add(new Task(() => plane.Fly(_posPlaneY, distance, wayPlane, lengthPlane, _cts.Token)));

                posPlaneY += 1;

                "|".PrintAtWihtColor(0, posPlaneY, ConsoleColor.White);
                "|".PrintAtWihtColor((wayPlane * 2), posPlaneY, ConsoleColor.White);

                if (posPlaneY % 2 != 0)
                    "+".PrintAtWihtColor(wayPlane, posPlaneY, ConsoleColor.White);
            }

            task1.ForEach(t => t.Start());
            Task.WaitAll(task1.ToArray());


            task2.ForEach(t => t.Start());
            Task.WaitAny(task2.ToArray());


            _cts.Cancel();
        }


       
    }
}
