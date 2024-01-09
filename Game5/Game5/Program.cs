using System;
using Game5;
namespace Game5
{
    internal class Program
    {
        public static void Main()
        {
            Game.persons = new List<Game>();
            Console.WriteLine("");
            Console.WriteLine("                                                       ~           ");
            Console.WriteLine("                                                INSPECTOR MORS");
            Console.WriteLine("                                                the  videogame");
            Console.WriteLine("                                                       ~           ");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("                                          press ENTER,  чтобы начать.");
            Console.ReadLine();
            Game per;
            while (true)
            {
                Console.WriteLine("                                                ~ИГРОВОЕ МЕНЮ~");
                Console.WriteLine("                         1 - Создать нового персонажа; 2 - Выбрать уже существующего.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string? vybor = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if (vybor == "1")
                {
                    Game.persons.Add(new Game()); //Добавляю в список живых новобранца
                }
                else if (vybor == "2")
                {
                    foreach (Game a in Game.persons) //Выполняю перебор в списке живых
                    {
                        Console.WriteLine("> Имя: ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        string? s = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.White;
                        if (s == a.Name) //Поиск по имени персонажа
                        {
                            per = a;
                            per.Menu2(Game.persons);
                        }
                    }
                }
            }
        }
    }
}