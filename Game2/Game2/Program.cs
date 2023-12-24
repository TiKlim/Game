class Program
{
    static void Main()
    {
        Gaming.persons = new List<Gaming>();
        Gaming.alive = new List<Gaming>();
        Console.WriteLine("");
        Console.WriteLine("                                                       ~           ");
        Console.WriteLine("                                                INSPECTOR MORS");
        Console.WriteLine("                                                the  videogame");
        Console.WriteLine("                                                       ~           ");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("                                           press ENTER,  чтобы начать.");
        Console.ReadLine();
        Gaming.Menu(Gaming.persons, Gaming.alive);
    }
}
