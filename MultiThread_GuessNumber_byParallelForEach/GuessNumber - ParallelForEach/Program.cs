using System;

namespace GuessNumber___ParallelForEach.GuessNumber_ParallelForEach
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] nameGamers = { "Trainee", "Junior", "Middle", "Senior", "Lead" };

            try
            {
                new StartGame(nameGamers).Start();
            }
            catch
            {
                Console.WriteLine("ooops...");
            }
            finally
            {
                Console.ReadLine();
            }
            Console.ReadLine();
        }
    }
}
