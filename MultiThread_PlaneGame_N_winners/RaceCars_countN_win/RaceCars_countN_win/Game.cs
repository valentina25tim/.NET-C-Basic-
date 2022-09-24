using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RaceCars_countN_win.RaceCars_countN_win
{
    public class Game
    {
        public static readonly object locker = new();

        public static List<IPlane> Team_1;
        public static List<IPlane> Team_2;

        public static List<CancellationTokenSource> cts;
        private CancellationTokenSource _cts;

        private List<List<Task>> _taskFly;        
        private List<Task> _taskFly_gr;

        private readonly char[] _direction = new char[] { '+', '-' };

        public static int
            lengthPlane,
            lengthMaxName,
            wayPlane,
            posTeam_Y = 8,
            posPlane_Y = 13;

        private int
            posField_Y = 12,
            posName_Y = 4,
            posName_X_1 = 1,
            posName_X_2;
       
        public Game()
        {
            if (Init.Name.Length == Init.LookP.Length && Init.Name.Length % 2 == 0)
            {
                lengthPlane = Helper.GetMaxLength(Init.LookP);
                lengthMaxName = Helper.GetMaxLength(Init.Name);
                wayPlane = Init.distance + lengthPlane;
                posName_X_2 = (lengthMaxName + posTeam_Y) + 6;
            }
            else
            {
                Console.WriteLine("name.Length != look.Length  &&  name.Length %2 == 0\n Init.cs -> [...]");
            }
        }
        public void Start()
        {
            MkTeams();

            PrintField();

            MkTaskName();

            MkTaskFly();
        }
        private void MkTeams()
        {
            Team_1 = new List<IPlane>();
            Team_2 = new List<IPlane>();

            int numb_1 = 1, numb_2 = 1;

            for (var i = 0; i < Init.Name.Length; i++)
            {
                int a = i + 1;

                if (a % 2 != 0)
                    Team_1.Add(new Team_1((ConsoleColor)Helper.GetRandomBetween(1, 15), Helper.GetRandomBetween(Init.minSpeed, Init.maxSpeed), _direction[0])
                    {
                        Name = Init.Name[i],
                        Look = Init.LookP[i],
                        NumberPlane = numb_1++
                    });

                if (a % 2 == 0)
                    Team_2.Add(new Team_2((ConsoleColor)Helper.GetRandomBetween(1, 15), Helper.GetRandomBetween(Init.minSpeed, Init.maxSpeed), _direction[1])
                    {
                        Name = Init.Name[i],
                        Look = Init.LookP[i],
                        NumberPlane = numb_2++,
                    });
            }
        }
        private void MkTaskName()
        {
            var taskName_1 = new List<Task>();
            var taskName_2 = new List<Task>();

            Helper.CreateTaskName(taskName_1, Team_1, posName_Y, posName_X_1, Init.teamName[0]);
            Helper.CreateTaskName(taskName_2, Team_2, posName_Y, posName_X_2, Init.teamName[1]);
        }
        private void PrintField()
        {
            $"\t== Sky Battle ==".PrintAtWihtColor(0, 0, ConsoleColor.White);

            "=".GetStrWithLength(wayPlane * 2 + 1)
                .PrintAtWihtColor(0, posField_Y, ConsoleColor.White);

            "=".GetStrWithLength(wayPlane * 2 + 1)
                .PrintAtWihtColor(0, posField_Y + (Init.LookP.Length) + 1, ConsoleColor.White);

            for (var i = 0; i < 2; i++)
                foreach (var plane in Team_2)
                {
                    var _posPlaneY = posField_Y;

                    posField_Y += 1;

                    "|".PrintAtWihtColor(0, posField_Y, ConsoleColor.White);
                    "|".PrintAtWihtColor((wayPlane * 2), posField_Y, ConsoleColor.White);

                    if (posField_Y % 2 != 0)
                        "+".PrintAtWihtColor(wayPlane, posField_Y + 1, ConsoleColor.White);
                }
        }
        private void GroupCompetition()
        {
            _taskFly = new List<List<Task>>(Team_1.Count);
            cts = new List<CancellationTokenSource>(_taskFly.Count);

            for (var i = 0; i < Team_1.Count; i++)
            {
                int a = i;
                var _posPlaneY = posPlane_Y;

                _cts = new();
                _taskFly_gr = new List<Task>();
                
                cts.Add(_cts);

                _taskFly_gr.Add(new Task(() => Team_1.ElementAt(a).Fly(_posPlaneY, cts.ElementAt(a).Token)));
                _taskFly_gr.Add(new Task(() => Team_2.ElementAt(a).Fly(_posPlaneY, cts.ElementAt(a).Token)));
                _taskFly.Add(_taskFly_gr);

                posPlane_Y += 2;
            }
        }
        private void MkTaskFly()
        {
            Rules.StepTeam_1 = Rules.CreateEmptyAddaySteps(Team_1.Count);
            Rules.StepTeam_2 = Rules.CreateEmptyAddaySteps(Team_1.Count);

            GroupCompetition();

            for (var i = 0; i < _taskFly.Count; i++)
            {
                _taskFly.ElementAt(i).ForEach(t => t.Start());
            }
        }
    }
}
