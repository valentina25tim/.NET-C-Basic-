using System;


namespace RaceCars_countN_win.RaceCars_countN_win
{
    public static class Init
    {
        public const int
            minSpeed = 10,
            maxSpeed = 200,
            distance = 30;
        
        public static string[] Name = new[] { "Tom", "Jack", "Rick", "Billy", "Simon", "Sam", "Tom", "Jack", "Tom", "Jack", "Rickу", "Billy" };
        public static string[] LookP = new[] { "=}>", "<[+", "=)>", "<{+", "=]>", "<(+", "=}>", "<[+", "=}>", "<[+", "=)>", "<{+" };
        public static string[] teamName = new string[] { "TEAM 1", "TEAM 2" };

        public static void Print()
        {
            try
            {
                new Game().Start();
            }
            catch
            {
                Console.WriteLine("Something happend  =( ");
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
