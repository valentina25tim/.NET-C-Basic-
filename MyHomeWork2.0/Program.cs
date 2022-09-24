using System;

namespace MyHomeWork2._0.SeaBattle_2._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //seaBat2.MapShip();//*** ДЛЯ ПРОВЕРКИ ГЕНЕРАЦИИ КАРТЫ <-- 
            // НА СТРОЧКЕ 214 ЗАКРЕПЛЕНА КАРТА КОМПА // *** ЕЁ МОЖНО СКРЫТЬ ОТ ПОЛЬЗОВАТЕЛЯ  <--

            SeaBat2 seaBat2 = new SeaBat2();

            seaBat2.Intro();
            seaBat2.MainMenu();
        }
    }
}
