using System;

namespace Calcul
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calc();

        }
        private static void Calc()
        {
            bool useCalc = true; //РАБОЧИЙ КАЛЬКУЛЯТОР

            var date = DateTime.Now;
            var g = "Good ";

            if (date.Hour >= 5 && date.Hour <= 11)  //определ время дня
            { Console.Write(g + " morning!"); }
            else if (date.Hour >= 12 && date.Hour <= 17)
            { Console.Write(g + " day!"); }
            else if (date.Hour >= 18 && date.Hour <= 22)
            { Console.Write(g + " everning!"); }
            else if (date.Hour >= 23 && date.Hour <= 24 || date.Hour >= 1 && date.Hour <= 4)
            { Console.Write(g + " night!"); }

            void ClearLine(int line)
            {
                Console.MoveBufferArea(0, line, Console.BufferWidth, 1, Console.BufferWidth, line, ' ', Console.ForegroundColor, Console.BackgroundColor);
            }
            double GetNumber(string message = null)
            {
                double i = double.MaxValue; // ПЛАВАЮЩАЯ ЗАПЯТАЯ
                bool noExit = true;
                Console.WriteLine(message ?? "Input operant ");

                do
                {
                    string usinpnum = Console.ReadLine();
                    if (!double.TryParse(usinpnum, out i))
                    {
                        ClearLine(Convert.ToInt32(usinpnum)); //УДАЛЯЕТ!!! НУЖНО РАЗОБРАТЬСЯ КАК)))
                        Console.WriteLine($"Input was not in correct format. Example: 45  67,08  0,60 etc. \n Please try again. ");
                    }
                    else
                    {
                        noExit = false;
                    }
                } while (noExit);
                return i;
            }
            do/////ЕСЛИ НЕ ПРАВИЛЬНО ВВЕДЕНА ОПЕРАЦИЯ

            {
                Console.Write(" Welcome to Primitive Calculator. \nPlease choose one action below: \n"); // ПРИВЕТСТВИЕ
                string[] act = { "absent", "a + b\n", "a - b\n", "a * b\n", "a / b\n", "aⁿ\n", "ⁿ√a\n", "exit from program\n" };//МАССИВ ВОЗМОЖНЫХ ОПЕРАЦИЙ

                for (int numact = 1; numact <= 7; numact++)
                {
                    Console.Write(numact + "-->  " + act[numact]);// ВЫВОД ВОЗМОЖНЫХ ОПЕРАЦИЙ или просто в одну строчку))
                }
                Console.WriteLine("Input action number : ");

                string anybutton = "Tab anyone button to try again.";
                try // ПРОВЕРКА НА ВВОД ВЫБОРА ОПЕРАЦИИ
                {
                    int useransw1 = int.Parse(Console.ReadLine());// ВВОД РАБОЧЕЙ ОПЕРАЦИИ 
                    Console.WriteLine("Your choose is --> " + act[useransw1]);

                    string a = "Input  a";
                    string b = "Input  b";
                    string n = "Input  n";
                    double a1, n1, res;

                    switch (useransw1)
                    {
                        case 0:
                            Console.WriteLine(anybutton);
                            break;

                        case 1: //"a + b\n",
                            Console.Write(GetNumber(a) + GetNumber(b));
                            break;

                        case 2: //"a - b\n", 
                            Console.Write(GetNumber(a) - GetNumber(b));
                            break;

                        case 3:  //"a * b\n",
                            Console.Write(GetNumber(a) * GetNumber(b));
                            break;

                        case 4:  //"a / b\n",
                            Console.Write(GetNumber(a) / GetNumber(b));
                            break;

                        case 5: //"aⁿ\n", 
                            res = Math.Pow(Convert.ToDouble(GetNumber(a)), Convert.ToDouble(GetNumber(n)));
                            Console.WriteLine("Result =  " + res);
                            break;

                        case 6: //"ⁿ√a\n",
                            a1 = Convert.ToDouble(GetNumber(a));
                            n1 = Convert.ToDouble(GetNumber(n));
                            res = Math.Pow(a1, 1 / n1);
                            Console.WriteLine("Result =  " + res);
                            break;

                        case 7: //"exit from program\n"
                            useCalc = false;
                            Console.Clear();
                            Console.WriteLine("Ok, without Math) Tab Enter to exit from program. " + anybutton);
                            Console.ReadLine();
                            break;

                        default:
                            useCalc = false;
                            Console.Clear();
                            Console.WriteLine("Don't worry! Something went wrong. You need to restart the application.) ");
                            Console.ReadLine();
                            break;
                    }
                }
                catch (DivideByZeroException)
                { Console.WriteLine("You cannot divide by zero(0), surprise!" + anybutton); }
                catch (ArgumentNullException argnull)
                { Console.WriteLine("I cannot find a result! Your choose is " + act[0] + "! " + anybutton); }
                catch (FormatException fe)
                { Console.WriteLine("You didn't input a number! I wait only digits." + anybutton); }
                catch (OverflowException ofe)
                { Console.WriteLine("Imput number was a bigger than need." + anybutton); }
                catch (Exception ex)
                { Console.WriteLine("Pay attention, this number isn't mention under!" + anybutton); }


                Console.WriteLine("Tab a button to continue.");
                Console.ReadKey();

                Console.Clear();

            } while (useCalc);/////ЕСЛИ НЕ ПРАВИЛЬНО ВВЕДЕНА ОПЕРАЦИЯ

            Console.ReadLine();
        }
    }
}
