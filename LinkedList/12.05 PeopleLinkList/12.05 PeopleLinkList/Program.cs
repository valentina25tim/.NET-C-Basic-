using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Concurrent;

namespace _12._05_PeopleLinkList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList list = new LinkedList();
            list.AddPeopleList();// количество PEOPLE определяет юзер
            list.PrintList();
            list.RemoveAt();
        }

        class LinkedList
        {
            List<LinkedList> pl = new List<LinkedList>(); // Список 2 
            public string FNane { get; set; } 
            public string LName { get; set; }   
            public int Ages { get; set; }
            public int Count { get; set; }


            int numberPe = 1;
            int count = 0;
            LinkedListNode head = null;
           
            private void AddNodeToFront() //запрос ввода данных и ПОСЛЕДНИЙ СТАНЕТ ПЕРВЫМ
            {
                Console.Write("\tNew people:\n First name -->");
                string firstName1 = Console.ReadLine();

                Console.Write(" Last name -->");
                string lastName1 = Console.ReadLine();

                Console.Write(" Age-->");
                int age1 = Convert.ToInt32(Console.ReadLine());

                LinkedListNode node = new LinkedListNode(firstName1, lastName1, age1);

                node.next = head;
                head = node;

                count++;
            }
            
            public void AddPeopleList()
            {
                bool show = false;
                do
                {
                    Console.WriteLine("To add people input '+' or '-' to show a full list");

                    char addShow = Convert.ToChar(Console.ReadLine());
                    switch (addShow)
                    {
                        case '+':
                            AddNodeToFront();
                            show = false;
                            break;
                        case '-':
                            show = true;
                            break;
                        default:
                            show = false;
                            break;
                    }
                } while (show != true);
                
            }
            private void PrintListReady()
            {
                foreach (LinkedList item in pl) 
                        Console.WriteLine($"{numberPe++} - : {item.FNane} {item.LName} {item.Ages}");// Выводится список 2
                numberPe = 1;
            }
            public void RemoveAt()
            {
                Console.Clear();

                Console.WriteLine("\tPeople list is ready:\n");

                PrintListReady();

                Console.Write("\nInput number to remove at list -->");
                var numb = int.Parse(Console.ReadLine());

                pl.RemoveAt(numb-1);
                
                PrintListReady();
            }

            public void PrintList() // вывод 
            {
                Console.WriteLine("\n\t** List **\n Added people: ");
                
                LinkedListNode people = head;
                
                while (people != null)
                {
                    pl.Add(new LinkedList () {FNane = Convert.ToString(people.firstName), LName = Convert.ToString(people.lastName), Ages = people.age});// Заполняется список 2

                    Console.WriteLine($" - {people.firstName} {people.lastName} { people.age}");// Выводится ссылочный список 
                    people = people.next;
                }
                Console.ReadKey();
            }
        }

        class LinkedListNode // чертеж -  ноды (перемен + линк на след=0)
        {
            public string firstName;
            public string lastName;
            public int age;
            public LinkedListNode next;

            public LinkedListNode(string x, string y, int z)
            {
                firstName = x;
                lastName = y;
                age = z;
                next = null;
            }
        }
    }
}

