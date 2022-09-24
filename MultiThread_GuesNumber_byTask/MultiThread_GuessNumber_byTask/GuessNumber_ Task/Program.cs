using System;

namespace GuessNumber__Task.GuessNumber_Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] nameGamers = { "Trainee", "Junior", "Middle", "Senior", "Lead" };

            try
            {
                new Game(nameGamers).Start(nameGamers);
            }
            catch
            {
                Console.WriteLine("ooops...");
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
