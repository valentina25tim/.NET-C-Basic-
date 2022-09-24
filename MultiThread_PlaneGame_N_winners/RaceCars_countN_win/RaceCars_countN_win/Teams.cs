using System;
using System.Collections.Generic;

namespace RaceCars_countN_win.RaceCars_countN_win
{
    sealed class Team_1 : BasePlane
    {
        public Team_1(ConsoleColor colorPlane, int speedPlane, char direction) : base(colorPlane, speedPlane, direction) { }

        int positionXLelt = 1;
        int step = 0;
        protected override void MovePrint(int positionY)
        {
            ". ".PrintAtWihtColor(positionXLelt, positionY, Color);
            Look.PrintAtWihtColor(positionXLelt + 2, positionY, Color);

            Rules.StepTeam_1 = Rules.AddStepToTeam(NumberPlane - 1, step, Rules.StepTeam_1);

            positionXLelt++;
            step++;
        }
    }
    sealed class Team_2 : BasePlane
    {
        public Team_2(ConsoleColor colorPlane, int speedPlane, char direction) : base(colorPlane, speedPlane, direction) { }

        int positionXRight = 1 + (Game.wayPlane * 2);
        int step = 0;
        protected override void MovePrint(int positionY)
        {
            Look.PrintAtWihtColor(positionXRight - Game.lengthPlane * 2, positionY, Color);
            " .".PrintAtWihtColor(positionXRight - Game.lengthPlane, positionY, Color);

            Rules.StepTeam_2 = Rules.AddStepToTeam(NumberPlane - 1, step, Rules.StepTeam_2);

            positionXRight--;
            step++;
        }
    }
}
