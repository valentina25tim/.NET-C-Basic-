using System;
using System.Threading;

namespace MyHomeWork2._0.SeaBattle_2._0
{
    public class SeaBat2
    {
        #region Peremenyi

        bool usegame = true; // работа игры до победы USERa (count = 20 true)
        bool compgame = true; // работа игры до победы COMPa (count = 20 true)

        bool userNext = true;
        bool userWinner = true;
        bool coDefault = true;
        bool CoWinner = true;


        string name = "SEA-BATTLE";
        public string usname;
        private string _anybutton = "Tab anyone button to try again.";

        private bool _optionMenu = false; // работа меню

        private string[] _letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        private int[] _numberstr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };

        static string empty = "[ ]";//null
        static string crowd = "[X]";//true
        static string miss = "[*]";//false

        bool[,] poolShipBack = new bool[10, 10];   // поле с генерацией комп карты 
        bool?[,] poolShipFront = new bool?[10, 10];// поле вывода комп карты (изначально пустое)

        int strCo;
        int columCo;
        int CountShipUser;

        Random rand = new Random();
        bool notMentionIndex = true; // для исключения повтора индекса при рандомности 
        bool?[,] poolShipBackUser = new bool?[10, 10]; //поле с вычетом рандомных попаданий (для исключения повторов)
        bool?[,] poolShipFrontUser = new bool?[10, 10];// поле вывода комп карты USERa (изначально пустое)

        bool map = false; // для генерации карты 

        bool[,] printMapCo = new bool[10, 10]; //вывод попаданий комп карты

        #endregion Peremenyi
        public void Intro()
        {
             Console.Write("\nWelcome to " + name + "!\nInput your name -->");
            usname = Console.ReadLine().ToUpper();
            Thread.Sleep(500);
        }
        public void MainMenu()
        {
            string[] act = { "Instruction", "Play game", "Exit" };//МАССИВ ЗАГОЛОВОК
            Console.WriteLine(usname + ", choose one option that you see below: ");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine((i + 1) + "-->  " + act[i]);// ВЫВОД ВОЗМОЖНЫХ ОПЕРАЦИЙ или просто в одну строчку))
            }

            Console.Write("Input option number --> ");

            StartOption(); // ** ПЕРЕХОД К ВЫБОРУ ОПЦИЙ
        }
        private void StartOption()
        {
            do
            {
                try
                {
                    var stOption = int.Parse(Console.ReadLine());

                    switch (stOption)
                    {
                        case 1:
                            _optionMenu = true;

                            Instruction();
                            break;
                        case 2:
                            _optionMenu = true;

                            Game();
                            break;
                        case 3:
                            _optionMenu = true;

                            Exit();
                            break;
                        default:
                            Console.WriteLine("Pay attention, this number isn't mention under!" + _anybutton);
                            Console.ReadKey();
                            Console.Clear();

                            MainMenu(); // ** ВОЗВРАТ К МЕНЮ

                            break;
                    }
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("You didn't input a number! I wait only digits. " + _anybutton); _optionMenu = false;
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu(); // ** ВОЗВРАТ К МЕНЮ
                }
                catch (OverflowException ofe)
                {
                    Console.WriteLine("Imput number was a bigger than need. " + _anybutton); _optionMenu = false;
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu(); // ** ВОЗВРАТ К МЕНЮ
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Pay attention, this number isn't mention under! " + _anybutton); _optionMenu = false;
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu();  // ** ВОЗВРАТ К МЕНЮ
                }

            } while (!_optionMenu);

        }
        private void Instruction()
        {
            Console.Clear();

            Console.WriteLine("I have instruction how play in game - " + name + ".");
            Thread.Sleep(500);

            Console.WriteLine("INSTRUCTION");
            Thread.Sleep(500);
            Console.Write("1 - " + usname + ", draw your tables with 10 strings and 10 columns\n" +
            "2 - place your ships across and down.\nNumber and description below.\n");

            var a = 4;
            for (var h = 1; h <= 4; h = h + 1)
            {
                {
                    for (var i = a - h; i > 0; i--)
                    {
                        var t = "   ";
                        Console.Write(t);
                    }
                    for (var j = h; 0 < j; j--)
                    {
                        Console.Write(crowd);
                    }
                    string shipInstr = " - " + (a + 1 - h) + " ship(s); ";
                    Console.Write(shipInstr);
                }
                Console.WriteLine();
            }
            Thread.Sleep(500);
            Console.WriteLine("Example of map:");
            Thread.Sleep(500);

            MapShipCompBack();// ** ВЫВОД ГЕНЕРАЦИИ КАРТЫ

            Console.Write("3 - There we have celles:  " + empty + " - means 'is EMPTY' and " + crowd + "- means 'here is SHIP!'.\n" +
                               "4 - When game started, you gess and input my ship coordinate . For example: string --> 5 ,column --> A \n" +
                               "5 - If you gessed, you will try again while I wrote: 'MISTAKE!'. \n" +
                               "6 - After your mistake I will gess your ship location  while I'll made a misstake and you wrote '-'.\n" +
                               "7 - Game " + name + " is continue while all of your ship or mine are destroyed.");
            Thread.Sleep(500);
            Console.WriteLine("\n\n" + usname + ", do next action to return to the main menu " + _anybutton);
            Console.ReadKey();
            Console.Clear();

            MainMenu(); // ** ВОЗВРАТ К МЕНЮ
        }
        private void Game()
        {
            for (int y = 0; y < poolShipFrontUser.GetLength(0); y++)        // ОЧИСТИЛА КАРТУ ЮЗЕРА 
            {
                for (int x = 0; x < poolShipFrontUser.GetLength(0); x++)
                {
                    poolShipFrontUser[y, x] = false;
                }
            }
            for (int y = 0; y < poolShipFront.GetLength(0); y++)        // ОЧИСТИЛА КАРТУ КОМПА 
            {
                for (int x = 0; x < poolShipFront.GetLength(0); x++)
                {
                    poolShipFront[y, x] = false;
                }
            }

            Console.Clear();
            Console.WriteLine($"\t== {name} ==");

            MapShipCompBack();// ПОЛУЧ СТАТИЧНАЯ КАРТА С ГЕНЕРАЦИИ(спрятать) ******************** ЕЁ МОЖНО СКРЫТЬ ОТ ЮЗЕРА

            do
            {
                do // УГАДЫВАЕТ ПОЛЬЗОВАТЕЛЬ 
                {
                    do
                    {
                        UserGess();

                    } while (!userNext);
                } while (userWinner);

                do// УГАДЫВАЕТ ПРОГРАМ
                {
                    //int[] toCo = new int[10];   //for (int i = 0; i< toCo.GetLength(0); i++)  //{toCo[i] = rand.Next(0,9);}  //for (int j = 0; j < toCo.GetLength(0); j++) //ПРОВЕРИЛА РАНДОМНОСТЬ ЧИСЕЛ  //{Console.WriteLine(toCo[j]);}


                    Console.ReadKey();
                    Console.Clear();

                    Console.WriteLine("\n I think YOUR SHIP on:");// ГЕНЕРИРУЮ СТРОКУ И КЛЕТКУ

                    NotMentionIndex();

                    Console.WriteLine("\nIf I gessed, input '+'  or  '-' if made a mistake");

                    do
                    {
                        MapShipUserFront();
                        CountShipUser = 0;
                        CompGess();

                    } while (!coDefault);

                } while (CoWinner);

            } while (!usegame || compgame); // ВЫХОД ПОСЛЕ ПОБЕДЫ 

        }
        private void Exit()
        {
            Console.Clear();

            Console.WriteLine("Thank for attantion) Tab anyone button to exit--> ");

        }


        private void ReturnMenuAfterWin()
        {
            Console.WriteLine($" Game is finished!{_anybutton} and return to Main Menu!");
            Console.ReadKey();
            Console.Clear();


            MainMenu(); // ** MAIN MENU RETURN 
        }
        private void UserGess()
        {
            Console.WriteLine("Try to gess my ships location.");
            MapShipCompFront(); // КАРТА КОМП ВЫВОДА ПОПАДАНИЙ И ОШИБОК 

            int CountShipCo = 0; // количество кораблей компа

            try
            {
                Console.Write("string --> ");
                int str = int.Parse(Console.ReadLine());
                Console.Write("column --> ");
                string colum = Console.ReadLine().ToUpper();


                int result1 = IndexOf1(_numberstr, str); //СЧИТЫВАЕТ ПОРЯД НОМЕР В МАССИВЕ СТРОЧКИ
                                                         //Console.WriteLine(result1);

                int result2 = IndexOf2(_letters, colum); //СЧИТЫВАЕТ ПОРЯД НОМЕР В МАССИВЕ КОЛОНКИ
                                                         //Console.WriteLine(result2);

                bool result3 = poolShipBack[result1, result2]; // ВЫВОД ЗНАЧЕНИЯ С КАРТЫ КОМП

                //Console.Clear();

                switch (result3)
                {
                    case false:
                        Console.WriteLine("\n MISTAKE");

                        userWinner = false;
                        userNext = true;

                        poolShipFront[result1, result2] = null; // ************ 'MISS'  when input after bool'?'******************************

                        break;

                    case true:

                        Console.WriteLine("\n GESSED! Input next coordinate:");

                        poolShipBack[result1, result2] = false; //удаление корабля 
                        poolShipFront[result1, result2] = true; //вывод на экран

                        for (int y = 0; y < poolShipBack.GetLength(0); y++)
                        {
                            for (int x = 0; x < poolShipBack.GetLength(0); x++)
                            {
                                if (poolShipFront[y, x] == true)
                                    CountShipCo++;
                            }
                        }
                        Console.WriteLine($"I have {20 - CountShipCo} ships");
                        Console.ReadKey();
                        Console.Clear();

                        switch (CountShipCo) //////////////НУЖНО ПРОВЕРИТЬ****************************************
                        {
                            case 20:
                                Console.Clear();

                                Console.WriteLine("Congratulation!!! you are winner!"); // ПЕРЕХОДИТ ДАЖЕ ПОСЛЕ ПОБЕДЫ К КАРТЕ ЮЗЕРА ??????????????????????????????????????????????????????
                                usegame = false;
                                userNext = true;
                                userWinner = false;
                                MapShipCompBack();// ** ВЫВОД ГЕНЕРАЦИИ КАРТЫ
                                ReturnMenuAfterWin(); // ВОЗВРАТ ПОСЛЕ ПОБЕДЫ В ГЛАВНОЕ МЕНЮ
                                break;
                            default:
                                userNext = false;
                                userWinner = true;
                                break;
                        }
                        break;

                    default:
                        Console.WriteLine("\n ERROR! I don't know what happend(");
                        userNext = false;
                        userWinner = true;
                        Console.Clear();
                        break;
                }
            }
            catch (Exception no)
            {
                Console.Clear();
                Console.WriteLine("\n WHAT it is? Only digits from 0 to 9 and letter from A to J!");
                userNext = false;
                userWinner = true;
            }

        }
        private void CompGess()
        {
            string usership = Console.ReadLine(); // угадал иле нет комп

            switch (usership)
            {
                case "+":
                    Console.WriteLine("\n I'm glad)");
                    CoWinner = true;
                    coDefault = true;
                    compgame = true;

                    poolShipFrontUser[strCo, columCo] = true; //вывод на экран

                    for (int y = 0; y < poolShipBack.GetLength(0); y++)
                    {
                        for (int x = 0; x < poolShipBack.GetLength(0); x++)
                        {
                            if (poolShipFrontUser[y, x] == true)
                                CountShipUser++;
                        }
                    }
                    Console.WriteLine($"{usname}, you have {20 - CountShipUser} ships");

                    switch (CountShipUser) // ** ОБЬЯВЛЯЕТСЯ ПОБЕДИТЕЛЛЬ КОМП 
                    {
                        case 20:
                            Console.Clear();

                            Console.WriteLine("Ahaha)) I'm  winner!");
                            coDefault = true;
                            CoWinner = false;
                            compgame = false;

                            ReturnMenuAfterWin(); // ВОЗВРАТ ПОСЛЕ ПОБЕДЫ В ГЛАВНОЕ МЕНЮ

                            break;
                        default:
                            CoWinner = true; coDefault = true; compgame = true;
                            break;
                    }
                    break;


                    Console.ReadKey();
                    Console.Clear();
                    break;


                case "-":
                    Console.WriteLine("\n Opps( You next.");
                    Console.ReadKey();
                    Console.Clear();
                    CoWinner = false;
                    coDefault = true;
                    poolShipFrontUser[strCo, columCo] = null; //вывод на экран
                    break;

                default:
                    Console.WriteLine("\n Input '+'  or  '-'");
                    CoWinner = true;
                    coDefault = false;
                    break;
            }
        }


        private void MapGeneration()
        {
            bool correct = false;

            Random rand = new Random();
            do
            {
                int Count = 0;
                int Y4, Y3, Y2, Y1;
                int X4, X3, X2, X1;

                // 4 ship x 1
                for (int i = 0; i < 1; i++)
                {
                    Y4 = rand.Next(0, 6);
                    X4 = rand.Next(0, 9);

                    if (Y4 == 0 && X4 == 0)
                    {
                        poolShipBack[Y4, X4] = true;
                        poolShipBack[Y4 + 1, X4] = true;
                        poolShipBack[Y4 + 2, X4] = true;
                        poolShipBack[Y4 + 3, X4] = true;

                        poolShipBack[Y4, X4 + 1] = false;
                        poolShipBack[Y4 + 1, X4 + 1] = false;
                        poolShipBack[Y4 + 2, X4 + 1] = false;
                        poolShipBack[Y4 + 3, X4 + 1] = false;
                        poolShipBack[Y4 + 4, X4] = false;
                    }
                    else if (Y4 == 0)
                    {
                        poolShipBack[Y4, X4] = true;
                        poolShipBack[Y4 + 1, X4] = true;
                        poolShipBack[Y4 + 2, X4] = true;
                        poolShipBack[Y4 + 3, X4] = true;

                        poolShipBack[Y4, X4 - 1] = false;
                        poolShipBack[Y4, X4 + 1] = false;
                        poolShipBack[Y4 + 1, X4 - 1] = false;
                        poolShipBack[Y4 + 1, X4 + 1] = false;
                        poolShipBack[Y4 + 2, X4 - 1] = false;
                        poolShipBack[Y4 + 2, X4 + 1] = false;
                        poolShipBack[Y4 + 3, X4 - 1] = false;
                        poolShipBack[Y4 + 3, X4 + 1] = false;
                        poolShipBack[Y4 + 4, X4] = false;
                    }
                    else if (X4 == 0)
                    {
                        poolShipBack[Y4, X4] = true;
                        poolShipBack[Y4 + 1, X4] = true;
                        poolShipBack[Y4 + 2, X4] = true;
                        poolShipBack[Y4 + 3, X4] = true;

                        poolShipBack[Y4, X4 + 1] = false;
                        poolShipBack[Y4 + 1, X4 + 1] = false;
                        poolShipBack[Y4 + 2, X4 + 1] = false;
                        poolShipBack[Y4 + 3, X4 + 1] = false;
                        poolShipBack[Y4 - 1, X4] = false;
                        poolShipBack[Y4 + 4, X4] = false;
                    }
                    else if (Y4 != 0 || X4 != 0)
                    {
                        poolShipBack[Y4, X4] = true;
                        poolShipBack[Y4 + 1, X4] = true;
                        poolShipBack[Y4 + 2, X4] = true;
                        poolShipBack[Y4 + 3, X4] = true;

                        poolShipBack[Y4, X4 - 1] = false;
                        poolShipBack[Y4, X4 + 1] = false;
                        poolShipBack[Y4 + 1, X4 - 1] = false;
                        poolShipBack[Y4 + 1, X4 + 1] = false;
                        poolShipBack[Y4 + 2, X4 - 1] = false;
                        poolShipBack[Y4 + 2, X4 + 1] = false;
                        poolShipBack[Y4 + 3, X4 - 1] = false;
                        poolShipBack[Y4 + 3, X4 + 1] = false;
                        poolShipBack[Y4 - 1, X4] = false;
                        poolShipBack[Y4 + 4, X4] = false;
                    }
                }

                // 3 ship x 2
                for (int i = 0; i < 2; i++)
                {

                    Y3 = rand.Next(0, 9);
                    X3 = rand.Next(0, 7);

                    if (Y3 == 0 && X3 == 0)
                    {
                        poolShipBack[Y3, X3] = true;
                        poolShipBack[Y3, X3 + 1] = true;
                        poolShipBack[Y3, X3 + 2] = true;

                        poolShipBack[Y3, X3 + 3] = false;
                        poolShipBack[Y3 + 1, X3] = false;
                        poolShipBack[Y3 + 1, X3 + 1] = false;
                        poolShipBack[Y3 + 1, X3 + 2] = false;

                    }
                    else if (Y3 == 0)
                    {
                        poolShipBack[Y3, X3] = true;
                        poolShipBack[Y3, X3 + 1] = true;
                        poolShipBack[Y3, X3 + 2] = true;

                        poolShipBack[Y3, X3 - 1] = false;
                        poolShipBack[Y3, X3 + 3] = false;
                        poolShipBack[Y3 + 1, X3] = false;
                        poolShipBack[Y3 + 1, X3 + 1] = false;
                        poolShipBack[Y3 + 1, X3 + 2] = false;
                    }
                    else if (X3 == 0)
                    {
                        poolShipBack[Y3, X3] = true;
                        poolShipBack[Y3, X3 + 1] = true;
                        poolShipBack[Y3, X3 + 2] = true;

                        poolShipBack[Y3, X3 + 3] = false;
                        poolShipBack[Y3 + 1, X3] = false;
                        poolShipBack[Y3 - 1, X3] = false;
                        poolShipBack[Y3 + 1, X3 + 1] = false;
                        poolShipBack[Y3 - 1, X3 + 1] = false;
                        poolShipBack[Y3 + 1, X3 + 2] = false;
                        poolShipBack[Y3 - 1, X3 + 2] = false;
                    }

                    else if (Y3 != 0 || X3 != 0)
                    {
                        poolShipBack[Y3, X3] = true;
                        poolShipBack[Y3, X3 + 1] = true;
                        poolShipBack[Y3, X3 + 2] = true;

                        poolShipBack[Y3, X3 - 1] = false;
                        poolShipBack[Y3, X3 + 3] = false;
                        poolShipBack[Y3 + 1, X3] = false;
                        poolShipBack[Y3 - 1, X3] = false;
                        poolShipBack[Y3 + 1, X3 + 1] = false;
                        poolShipBack[Y3 - 1, X3 + 1] = false;
                        poolShipBack[Y3 + 1, X3 + 2] = false;
                        poolShipBack[Y3 - 1, X3 + 2] = false;
                    }
                }

                // 2 ship x 3
                for (int i = 0; i < 3; i++)
                {

                    Y2 = rand.Next(0, 8);
                    X2 = rand.Next(0, 9);

                    if (Y2 == 0 && X2 == 0)
                    {
                        poolShipBack[Y2, X2] = true;
                        poolShipBack[Y2 + 1, X2] = true;

                        poolShipBack[Y2, X2 + 1] = false;
                        poolShipBack[Y2 + 1, X2 + 1] = false;
                        poolShipBack[Y2 + 2, X2] = false;
                    }
                    else if (Y2 == 0)
                    {
                        poolShipBack[Y2, X2] = true;
                        poolShipBack[Y2 + 1, X2] = true;

                        poolShipBack[Y2, X2 - 1] = false;
                        poolShipBack[Y2, X2 + 1] = false;
                        poolShipBack[Y2 + 1, X2 - 1] = false;
                        poolShipBack[Y2 + 1, X2 + 1] = false;
                        poolShipBack[Y2 + 2, X2] = false;

                    }
                    else if (X2 == 0)
                    {
                        poolShipBack[Y2, X2] = true;
                        poolShipBack[Y2 + 1, X2] = true;

                        poolShipBack[Y2, X2 + 1] = false;
                        poolShipBack[Y2 + 1, X2 + 1] = false;
                        poolShipBack[Y2 - 1, X2] = false;
                        poolShipBack[Y2 + 2, X2] = false;
                    }
                    else if (Y2 != 0 || X2 != 0)
                    {
                        poolShipBack[Y2, X2] = true;
                        poolShipBack[Y2 + 1, X2] = true;

                        poolShipBack[Y2, X2 - 1] = false;
                        poolShipBack[Y2, X2 + 1] = false;
                        poolShipBack[Y2 + 1, X2 - 1] = false;
                        poolShipBack[Y2 + 1, X2 + 1] = false;
                        poolShipBack[Y2 - 1, X2] = false;
                        poolShipBack[Y2 + 2, X2] = false;
                    }
                }

                // 1 ship x 4
                for (int i = 0; i < 4; i++)
                {
                    Y1 = rand.Next(0, 9);
                    X1 = rand.Next(0, 9);

                    if (Y1 == 0 && X1 == 0)
                    {
                        poolShipBack[Y1, X1] = true;

                        poolShipBack[Y1, X1 + 1] = false;
                        poolShipBack[Y1 + 1, X1] = false;
                    }
                    else if (Y1 == 0)
                    {
                        poolShipBack[Y1, X1] = true;

                        poolShipBack[Y1, X1 + 1] = false;
                        poolShipBack[Y1 + 1, X1] = false;
                        poolShipBack[Y1, X1 - 1] = false;
                    }
                    else if (X1 == 0)
                    {
                        poolShipBack[Y1, X1] = true;

                        poolShipBack[Y1 - 1, X1] = false;
                        poolShipBack[Y1, X1 + 1] = false;
                        poolShipBack[Y1 + 1, X1] = false;
                    }
                    else if (Y1 != 0 || X1 != 0)
                    {
                        poolShipBack[Y1, X1] = true;

                        poolShipBack[Y1 - 1, X1] = false;
                        poolShipBack[Y1, X1 + 1] = false;
                        poolShipBack[Y1 + 1, X1] = false;
                        poolShipBack[Y1, X1 - 1] = false;
                    }
                }


                for (int y = 0; y < poolShipBack.GetLength(0); y++)
                {
                    for (int x = 0; x < poolShipBack.GetLength(0); x++)
                    {
                        if (poolShipBack[y, x] == true)
                            Count++;
                    }
                }

                switch (Count)
                {
                    case 20:
                        correct = true;
                        break;
                    default:
                        for (int y = 0; y < poolShipBack.GetLength(0); y++)
                        {
                            for (int x = 0; x < poolShipBack.GetLength(0); x++)
                            {
                                poolShipBack[y, x] = false;
                            }
                        }
                        correct = false;
                        break;
                }
            }
            while (!correct);
        }//генерация кораблей
        private void MapShipCompBack()
        {
            bool[,] poolMidle()
            {
                Console.Write("    ");
                for (int r = 0; r < poolShipBack.GetLength(0); r++)
                {
                    Console.Write(_letters[r] + "  ");

                }
                for (int y = 0; y < poolShipBack.GetLength(0); y++)
                {
                    Console.Write($"\n {_numberstr[y]} ");
                    for (int x = 0; x < poolShipBack.GetLength(1); x++)
                    {
                        Console.Write($"{(poolShipBack[y, x] == null ? miss : poolShipBack[y, x] == true ? crowd : empty)}");
                    }
                }
                Console.WriteLine();
                return poolShipBack;
            }

            do
            {
                //Console.WriteLine(poolMidle()); // ПУСТОЕ ПОЛЕ
                try
                {
                    MapGeneration(); // ПОДБОРКА КАРТЫ

                    Console.WriteLine(poolMidle());

                    map = true;

                }
                catch (Exception ex)
                {
                    bool map = false;
                }
            } while (!map);


        }//внутренняя карта кораблей компа
        private void MapShipCompFront()
        {
            bool?[,] poolMidle1()
            {
                Console.Write("    ");
                for (int r = 0; r < poolShipFront.GetLength(0); r++)
                {
                    Console.Write(_letters[r] + "  ");

                }
                for (int y = 0; y < poolShipFront.GetLength(0); y++)
                {
                    Console.Write($"\n {_numberstr[y]} ");
                    for (int x = 0; x < poolShipFront.GetLength(1); x++)
                    {
                        Console.Write($"{(poolShipFront[y, x] == null ? miss : poolShipFront[y, x] == true ? crowd : empty)}");
                    }
                }
                Console.WriteLine();
                return poolShipFront;
            }


            Console.WriteLine(poolMidle1()); // ПУСТОЕ ПОЛЕ

        }//внешняя карта кораблей компа
        private void MapShipUserFront()
        {
            bool?[,] poolMidle2()
            {
                Console.Write("    ");
                for (int r = 0; r < poolShipFrontUser.GetLength(0); r++)
                {
                    Console.Write(_letters[r] + "  ");

                }
                for (int y = 0; y < poolShipFrontUser.GetLength(0); y++)
                {
                    Console.Write($"\n {_numberstr[y]} ");
                    for (int x = 0; x < poolShipFrontUser.GetLength(1); x++)
                    {
                        Console.Write($"{(poolShipFrontUser[y, x] == null ? miss : poolShipFrontUser[y, x] == true ? crowd : empty)}");
                    }
                }
                Console.WriteLine();
                return poolShipFrontUser;
            }

            Console.WriteLine(poolMidle2()); // ПУСТОЕ ПОЛЕ
        }//внешняя карта кораблей юзера

        private void NotMentionIndex()
        {

            do
            {
                strCo = rand.Next(0, 10);
                columCo = rand.Next(0, 10);
                if (poolShipFrontUser[strCo, columCo] == false)
                {

                    Console.Write("string --> " + _numberstr[strCo]);
                    Console.Write("\ncolumn --> " + _letters[columCo]);
                    notMentionIndex = true;
                }
                else if (poolShipFrontUser[strCo, columCo] == null || poolShipFrontUser[strCo, columCo] == true)
                {
                    notMentionIndex = false;
                }
            } while (notMentionIndex != true);

        }//логика для исключения повтора индекса
        static int IndexOf1(int[] array, int value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }//сапостав введен значения сроки с индексом numberstr
        static int IndexOf2(string[] array, string value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }//сапостав введен значения колонки с индексом letters


    }
}

