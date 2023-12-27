using System;
using Game3;
namespace Game3
{
    internal class Program
    {
        static void Main()
        {
            Game.persons = new List<Game>();
            Console.WriteLine("");
            Console.WriteLine("                                                       ~           ");
            Console.WriteLine("                                                INSPECTOR MORS");
            Console.WriteLine("                                                the  videogame");
            Console.WriteLine("                                                       ~           ");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("                                           press ENTER,  чтобы начать.");
            Console.ReadLine();
            Game.Menu(Game.persons);
        }
    }
}