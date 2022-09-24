using System;

namespace RaceCars_countN_win.RaceCars_countN_win
{
    public static class Rules
    {
        public static int[] StepTeam_1 { get; set; }
        public static int[] StepTeam_2 { get; set; }

        public static int[] CreateEmptyAddaySteps(int countGamer)
        {
            int[] stepList = new int[countGamer];

            for (var i = 0; i < countGamer; i++)
            {
                stepList[i] = 0;
            }
            return stepList;
        }
        public static int[] AddStepToTeam(int index, int countSteps, int[] array)
        {
            array[index] = countSteps;

            return array;
        }
    }
}
