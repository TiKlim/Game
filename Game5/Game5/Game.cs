using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game5
{
    internal class Game
    {
        private string? name;
        private int x;
        private int y;
        private bool svoychuzhoy;
        private int heartstek;
        private int hearts;
        private bool life = true;
        private int uron;
        private int medaly;
        static public List<Game> persons;
        public string Name { get { return name; } }
        public bool Svoychuzhoy { get { return svoychuzhoy; } }
        public int Heartstek { get { return heartstek; } }
        public bool Life { get { return life; } }
        public Game() { Info(persons); } //При создании нового перса взываем пользователя к заполнению досье на подопечного
        public Game(int a) { Smert(); }
        private void Info(List<Game> persons)
        {
            Console.WriteLine("> Имя:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            this.name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("> Сторона (1 или 2):");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string? vybor = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            if (vybor == "1")
            {
                this.svoychuzhoy = true;
            }
            else if (vybor == "2")
            {
                this.svoychuzhoy = false;
            }
            Console.WriteLine("> Местоположение (По горизонтали 'x', ENTER, затем по вертикали 'y')"); //Координаты
            Console.ForegroundColor = ConsoleColor.Cyan;
            this.x = Convert.ToInt32(Console.ReadLine());
            this.y = Convert.ToInt32(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.White;
            hearts = 100;
            heartstek = hearts;
            Random random = new Random();
            uron = random.Next(hearts);
        }
        private void Print()
        {
            Console.WriteLine($"[Имя: {name} ~");
            Console.WriteLine($"[Местоположение: ({x},{y}) ~");
            Console.WriteLine($"[Здоровье: {heartstek}/{hearts} ~");
            if (svoychuzhoy == true)
            {
                Console.WriteLine($"[Лагерь: полицейские ~");
            }
            else
            {
                Console.WriteLine($"[Лагерь: преступники ~");
            }
            Console.WriteLine($"[Урон: {uron} ~");
            Console.WriteLine($"[Очки побед: {medaly} ~");
            //Menu2(persons);
        }
        private void MoveX(List<Game> persons) //Перемещение X
        {
            Console.WriteLine("> Влево или вправо:");
            while (true)
            {
                if (life == false)
                {
                    return;
                }
                if (persons.Count(Game => Game.life == true && Game.svoychuzhoy == false) == 0)
                {
                    return;
                }
                if (persons.Count(Game => Game.life == true && Game.svoychuzhoy == true) == 0)
                {
                    return;
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                string? vybor5 = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                switch (vybor5)
                {
                    case "влево":
                        x -= 1;
                        break;
                    case "вправо":
                        x += 1;
                        break;
                }
                Console.WriteLine($"({x},{y})");
                Proverka(persons); break;
            }
        }
        private void MoveY(List<Game> persons) //Перемещение Y
        {
            Console.WriteLine("> Вверх или вниз");
            while (true)
            {
                if (life == false)
                {
                    return;
                }
                if (persons.Count(Game => Game.life == true && Game.svoychuzhoy == false) == 0)
                {
                    return;
                }
                if (persons.Count(Game => Game.life == true && Game.svoychuzhoy == true) == 0)
                {
                    return;
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                string? vybor4 = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                switch (vybor4)
                {
                    case "вверх":
                        y += 1;
                        break;
                    case "вниз":
                        y -= 1;
                        break;
                }
                Console.WriteLine($"({x},{y})");
                Proverka(persons);
            }
        }
        private void Proverka(List<Game> persons)
        {
            foreach (Game person in persons)
            {
                if (x == person.x && y == person.y)
                {
                    if (person.life == true)
                    {
                        if (svoychuzhoy != person.svoychuzhoy)
                        {
                            if (life == false)
                            {
                                return;
                            }
                            if (persons.Count(Game => Game.life == true && Game.svoychuzhoy == false) == 0)
                            {
                                return;
                            }
                            if (persons.Count(Game => Game.life == true && Game.svoychuzhoy == true) == 0)
                            {
                                return;
                            }
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("! ЗАМЕЧЕН ВРАГ !");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("> Ваши действия: 1 - Сражаться, нанося обычный урон; 2 - Использовать ультимативную способность.");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            string? vybor3 = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            switch (vybor3)
                            {
                                case "1":
                                    Boy(persons);
                                    break;
                                case "2":
                                    Ulta(persons);
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    Menu2(persons);
                }
            }
        }

        private void Boy(List<Game> persons) //Не "мальчик", а БОЙ
        {
            List<Game> drugy = new List<Game>();
            List<Game> vragy = new List<Game>();
            foreach (Game person in persons)
            {
                if (x == person.x && y == person.y)
                {
                    if (person.life == true)
                    {
                        if (svoychuzhoy != person.svoychuzhoy)
                        {
                            vragy.Add(person);
                        }
                        else
                        {
                            drugy.Add(person);
                        }
                    }
                }
            }
            Console.WriteLine("> Ваша команда:"); //Перечисление учасников битвы
            foreach (Game person in drugy)
            {
                person.Print();
            }
            Console.WriteLine("> Команда противника:");
            foreach (Game person in vragy)
            {
                person.Print();
            }
            Console.WriteLine("> Нажмите ENTER, чтобы продолжить.");
            Console.ReadLine();
            while (true) //Создание переменных урона
            {
                int druguron = 0;
                int vraguron = 0;
                foreach (Game person in drugy) //Суммирование урона живых членов команд
                {
                    druguron += person.uron;
                }
                foreach (Game person in vragy)
                {
                    vraguron += person.uron;
                }
                druguron = druguron / vragy.Count; //Деление общего урона на количество противников
                vraguron = vraguron / drugy.Count; //Деление общего урона на количество противников
                foreach (Game person in drugy) //Нанесение урона 
                {
                    person.heartstek -= vraguron;
                    if (person.heartstek <= 0)
                    {
                        person.life = false;
                    }
                }
                foreach (Game person in vragy)
                {
                    person.heartstek -= druguron;
                    if (person.heartstek <= 0)
                    {
                        person.life = false;
                    }
                }
                if (life == false) //Проверка на живой/неживой
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" вы мертвы ");
                    Console.ForegroundColor = ConsoleColor.White;
                    persons.Remove(new Game(1));
                    return;
                }
                if (persons.Count(Game => Game.life == true && Game.svoychuzhoy == svoychuzhoy) == 0)
                {
                    return;
                }
                if (persons.Count(Game => Game.life == true && Game.svoychuzhoy != svoychuzhoy) == 0)
                {
                    return;
                }
                Console.WriteLine("Вы ещё целы.");
                Console.WriteLine("> Состояние Ваших товарищей: "); //проверка ОЗ
                foreach (Game person in drugy)
                {
                    if (person.life == true)
                    {
                        Console.WriteLine($"Имя: {person.name}, \nОЗ: {person.heartstek}, \nУрона получено: {vraguron}");
                    }
                }
                Console.WriteLine("> Состояние Ваших врагов: ");
                foreach (Game person in vragy)
                {
                    if (person.life == true)
                    {
                        Console.WriteLine($"Имя: {person.name}, \nОЗ: {person.heartstek}, \nУрона получено: {druguron}");
                    }
                }
                Console.WriteLine("> Бой продолжается");
                Console.WriteLine("> Ваши действия: 1 - Сражаться, нанося обычный урон; 2 - Использовать ультимативную способность.");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string? vybor3 = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                switch (vybor3)
                {
                    case "1":
                        Boy(persons);
                        break;
                    case "2":
                        Ulta(persons);
                        break;
                }
                if (persons.Count(Game => Game.life == true && Game.svoychuzhoy != svoychuzhoy) == 0 && persons.Count(Game => Game.life == true && Game.svoychuzhoy == svoychuzhoy) == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(" Ничья ");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }
                else if (persons.Count(Game => Game.life == true && Game.svoychuzhoy != svoychuzhoy) == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("    ПОБЕДА \nвсе враги повержены ");
                    Console.ForegroundColor = ConsoleColor.White;
                    medaly += 1;
                    return;
                }
                else if (persons.Count(Game => Game.life == true && Game.svoychuzhoy == svoychuzhoy) == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("    поражениe \nсоюзников не осталось ");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }
                else
                {
                    if (life == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" вы мертвы ");
                        Console.ForegroundColor = ConsoleColor.White;
                        persons.Remove(new Game(1));
                        return;
                    }
                    while (true) //выбор дествий в бою
                    {
                        Menu2(persons);
                        break;
                    }
                    if (vragy.Count(Game => Game.life == true) == 0) //проверка кто победил
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(" ПОБЕДА ");
                        Console.ForegroundColor = ConsoleColor.White;
                        medaly += 1;
                        return;
                    }
                    Console.WriteLine("> Бой продолжается");
                }
            }
        }
        private void Ulta(List<Game> persons) //Использование ультимативной способности в бою
        {
            foreach (Game person in persons)
            {
                if (x == person.x && y == person.y)
                {
                    if (svoychuzhoy != person.svoychuzhoy)
                    {
                        person.life = false;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("    ПОБЕДА \nвсе враги повержены ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }

            Menu2(persons);
        }
        private void Iscelenie(List<Game> persons) //Лечение товарищей
        {
            while (true)
            {
                Console.WriteLine("> Имя:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string? vybor2 = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                foreach (Game person in persons)
                {
                    if (vybor2 == person.name)
                    {
                        if (person.svoychuzhoy == svoychuzhoy)
                        {
                            Console.WriteLine("> Сколько очков здоровья восстановить:");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            int hp = Convert.ToInt32(Console.ReadLine());
                            Console.ForegroundColor = ConsoleColor.White;
                            if (hp < heartstek)
                            {
                                if (hp < person.hearts)
                                {
                                    person.heartstek += hp;
                                    heartstek -= hp;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("! НЕЛЬЗЯ ВОССТАНОВИТЬ БОЛЬШЕ 100 ОЧКОВ ЗДОРОВЬЯ !");
                                }
                            }
                            else
                            {
                                Console.WriteLine("! НЕЛЬЗЯ ТРАТИТЬ ОЧКОВ ЗДОРОВЬЯ БОЛЬШЕ, ЧЕМ ИМЕЕТСЯ !");
                            }
                        }
                        else
                        {
                            Console.WriteLine("! НЕЛЬЗЯ ЛЕЧИТЬ ВРАГОВ !");
                        }
                    }
                }
                break;
            }
            Menu2(persons);
        }
        private void Lechenie() //Лечение себя
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string? vybor7 = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            if (vybor7 == "1")
            {
                if (heartstek >= 50 && heartstek < hearts)
                {
                    heartstek += 5;
                }
                else if (heartstek <= 50)
                {
                    heartstek += 25;
                }
            }
            else if (vybor7 == "2")
            {
                if (medaly >= 5)
                {
                    heartstek = hearts;
                    medaly -= 5;
                    Console.WriteLine("> Вы восстановили полноценное количество очков здоровья.");
                }
                else
                {
                    Console.WriteLine("! СЛЕДУЕТ ПОБЕДИТЬ В ПЯТИ СТЫЧКАХ !");
                }
            }
            Menu2(persons);
        }
        public void Menu2(List<Game> persons)
        {
            Console.WriteLine("                                                ~МЕНЮ ПЕРСОНАЖА~");
            Console.WriteLine("1 - Показать информацию о персонаже; 2 - Переместиться влево/вправо; 3 - Переместиться вверх/вниз; 4 - Лечить себя; 5 - Лечить товарищей; 6 - Выход в иговое меню.");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string? vybor6 = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            switch (vybor6)
            {
                case "1":
                    Print();
                    Menu2(persons);
                    break;
                case "2":
                    MoveX(persons);
                    break;
                case "3":
                    MoveY(persons);
                    break;
                case "4":
                    Lechenie();
                    break;
                case "5":
                    Iscelenie(persons);
                    break;
                case "6":
                    break;
            }
        }
        private void Smert()
        {
            Program.Main();
        }
    }
}
