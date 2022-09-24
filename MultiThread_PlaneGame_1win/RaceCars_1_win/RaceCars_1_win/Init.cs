using System;


namespace RaceCars_1_win.RaceCars_1_win
{
    public class Init
    {
        public void Print()
        {
            string[] Name = new[] { "Tom", "Jack", "Rick", "Billy", "Simon", "Sam", "Tom", "Jack", "Tom", "Jack", "Rick", "Billy" };
            string[] Type = new[] { "=}>", "<[+", "=)>", "<{+", "=]>", "<(+", "=}>", "<[+", "=}>", "<[+", "=)>", "<{+" };
            char[] Direction = new[] { '+', '-', '+', '-', '+', '-', '+', '-', '+', '-', '+', '-', '+', '-', '+' };

            try
            {
                new SkyLine(Name, Type, Direction, Name.Length).Start(Type);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something happend  =( ");
            }
            Console.ReadLine();

        }
    }
}
