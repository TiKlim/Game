using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    internal class Game
    {
        private string? name;
        //private int[] _coor = new int[2]; //*
        private int x;
        private int y;
        private bool svoychuzhoy;
        private int heartstek;
        private int hearts;
        private bool _life = true; //*
        private int uron;
        private int medaly;
        static public List<Game> persons;
        public string Name { get { return name; } }
        //public int CoorX { get { return _coor[0]; } } //*
        //public int CootY { get { return _coor[1]; } } //*
        public bool Svoychuzhoy { get { return svoychuzhoy; } }
        public int Heartstek { get { return heartstek; } }
        public bool Life { get { return _life; } } //*
        public Game() { Info(persons); } //При создании нового перса взываем пользователя к заполнению досье на подопечного

        //Ввод данных
        /*public Game(string name, int healthMax, int coorX, int coorY, bool Svoychuzhoy, int damage, int wins, bool life) //*
        {
            name = name;
            hearts = healthMax;
            heartstek = hearts;
            _coor[0] = coorX;
            _coor[1] = coorY;
            svoychuzhoy = Svoychuzhoy;
            uron = damage;
            medaly = wins;
            _life = life;
        }*/
        private void Info(List<Game> persons)
        {
            Console.WriteLine("> Имя:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            this.name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("> Сторона ('полицейские' или 'преступники'):");
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
            //name = s[0];
            //hearts = Int32.Parse(s[2]);
            hearts = 100;
            heartstek = hearts;
            //_coor[0] = Int32.Parse(s[3]);
            //_coor[1] = Int32.Parse(s[4]);
            Random random = new Random();
            uron = random.Next(hearts);
        }
        //Вывод информации
        private void Print()
        {
            Console.WriteLine($"[Имя: {name} ~");
            Console.WriteLine($"[Местоположение: ({x},{y}) ~");
            Console.WriteLine($"[Здоровье: {heartstek}/{hearts} ~");
            Console.WriteLine($"[Лагерь: {svoychuzhoy} ~");
            Console.WriteLine($"[Урон: {uron} ~");
            Console.WriteLine($"[Очки побед: {medaly} ~");
            Menu2(persons);
        }
        private void MoveX(List<Game> persons) //Перемещение X
        {
            Console.WriteLine("> Влево или вправо:");
            while (true)
            {
                if (_life == false)
                {
                    return;
                }
                //if (persons.Count(Game => Game._life == true && Game.svoychuzhoy == false) == 0) return;
                //if (persons.Count(Game => Game._life == true && Game.svoychuzhoy == true) == 0) return;
                string? vybor5 = Console.ReadLine();
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
                /*foreach (Game person in persons)
                {
                    if (x == person.x && y == person.y)
                    {
                        if (person._life == true)
                        {
                            if (svoychuzhoy != person.svoychuzhoy)
                            {
                                if (_life == false)
                                {
                                    return;
                                }
                                if (persons.Count(Game => Game._life == true && Game.svoychuzhoy == false) == 0)
                                {
                                    return;
                                }
                                if (persons.Count(Game => Game._life == true && Game.svoychuzhoy == true) == 0)
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
                            else
                            {
                                Menu2(persons);
                            }
                        }
                    }
                }*/
            }
        }
        private void MoveY(List<Game> persons) //Перемещение Y
        {
            Console.WriteLine("> Вверх или вниз");
            //while (true)
            //{
            if (_life == false)
            {
                return;
            }
            //if (persons.Count(Game => Game._life == true && Game.svoychuzhoy == false) == 0) return;
            //if (persons.Count(Game => Game._life == true && Game.svoychuzhoy == true) == 0) return;   
            string? vybor4 = Console.ReadLine();
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
        private void Proverka(List<Game> persons)
        {
            foreach (Game person in persons)
            {
                if (x == person.x && y == person.y)
                {
                    if (person._life == true)
                    {
                        if (svoychuzhoy != person.svoychuzhoy)
                        {
                            /*if (_life == false)
                            {
                                return;
                            }
                            if (persons.Count(Game => Game._life == true && Game.svoychuzhoy == false) == 0)
                            {
                                return;
                            }
                            if (persons.Count(Game => Game._life == true && Game.svoychuzhoy == true) == 0)
                            {
                                return;
                            }*/
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
                //}
            }
        }
    
        private void Boy(List<Game> persons) //Не "мальчик", а БОЙ
        {
            List<Game> drugy = new List<Game>(); //поиск персонженей на клетке
            List<Game> vragy = new List<Game>();
            foreach (Game person in persons)
            {
                if (x == person.x && y == person.y)
                {
                    if (person._life == true)
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
            Console.WriteLine("> Ваша команда:"); //перечисление учасников битвы
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
            while (true) //создание переменных урона
            {
                int FrenDam = 0;
                int EnemDam = 0;
                foreach (Game person in drugy) //суммирование урона живых членов команд
                {
                    FrenDam += person.uron;
                }
                foreach (Game person in vragy)
                {
                    EnemDam += person.uron;
                }
                FrenDam /= vragy.Count; //деление суммарного урона на количество противников
                EnemDam /= drugy.Count;
                foreach (Game person in drugy) //нанесение урона 
                {
                    person.heartstek -= EnemDam;
                    if (person.heartstek <= 0)
                        person._life = false;
                }
                foreach (Game person in vragy)
                {
                    person.heartstek -= FrenDam;
                    if (person.heartstek <= 0)
                        person._life = false;
                }
                if (_life == false) //проверка жив ли гг
                { Console.WriteLine("\n------------| Вы умерли |------------\n"); return; }
                if (persons.Count(Game => Game._life == true && Game.svoychuzhoy == svoychuzhoy) == 0) return;
                if (persons.Count(Game => Game._life == true && Game.svoychuzhoy != svoychuzhoy) == 0) return;
                Console.WriteLine("\n----| Вы смогли отбросить врага и нашли момент восстановить силы. |----\n");
                Console.WriteLine("-------------------------------\n" + "ОЗ вашей команды: "); //проверка ОЗ
                foreach (Game person in drugy)
                    if (person._life == true)
                        Console.WriteLine($"Имя: {person.name}, ОЗ: {person.heartstek}, урона получено: {EnemDam}");
                Console.WriteLine("\n-------------------------------\n" + "ОЗ ваших врагов: ");
                foreach (Game person in vragy)
                    if (person._life == true)
                        Console.WriteLine($"Имя: {person.name}, ОЗ: {person.heartstek}, урона получено: {FrenDam}");
                if (persons.Count(Game => Game._life == true && Game.svoychuzhoy != svoychuzhoy) == 0 &&
                    persons.Count(Game => Game._life == true && Game.svoychuzhoy == svoychuzhoy) == 0)
                {
                    Console.WriteLine("\n--------------| Ничья |--------------\n"); return;
                }
                else if (persons.Count(Game => Game._life == true && Game.svoychuzhoy != svoychuzhoy) == 0)
                {
                    Console.WriteLine("\n--------------| Победа, врагов не осталось |--------------\n"); return;
                }
                else if (persons.Count(Game => Game._life == true && Game.svoychuzhoy == svoychuzhoy) == 0)
                {
                    Console.WriteLine("\n--------------| Поражение, союзников не осталось |--------------\n"); return;
                }
                else
                {
                    if (_life == false)
                    {
                        Console.WriteLine("> Персонаж мертв, нажмите Enter для выхода");
                        Console.ReadLine();
                        return;
                    }
                    while (true) //выбор дествий в бою
                    {
                        Menu2(persons);
                        break;
                    }                    
                    if (vragy.Count(Game => Game._life == true) == 0) //проверка кто победил
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("! ПОБЕДА !");
                        Console.ForegroundColor = ConsoleColor.White;
                        medaly += 1;
                        return;
                    }
                    Console.WriteLine("Битва продолжается...");
                }
            }
        }

        //Уничтожение 
        private void Ulta(List<Game> persons)
        {
            if (medaly >= 10)
            {
                foreach (Game person in persons)
                    if (x == person.x && y == person.y)
                        if (svoychuzhoy != person.svoychuzhoy)
                            person._life = false;
                Console.WriteLine("> Все враги уничтожены");
            }
            Menu2(persons);
            /*else
            {
                Console.WriteLine("! ДЛЯ ИСПОЛЬЗОВАНИЯ УЛЬТИМАТИВНОЙ СПОСОБНОСТИ НЕОБХОДИМО  !" + "Битва неизбежна");
                Boy(persons);
            }*/
        }
        private void Iscelenie(List<Game> persons) //Лечение
        {
            while (true)
            {
                Console.Write("> Имя:");
                string? vybor2 = Console.ReadLine();
                foreach (Game person in persons)
                {
                    if (vybor2 == person.name)
                    {
                        if (person.svoychuzhoy == svoychuzhoy)
                        {
                            Console.WriteLine("> Сколько очков здоровья восстановить:");
                            int hp = Convert.ToInt32(Console.ReadLine());
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
                            Console.WriteLine("! НЕЛЬЗЯ ЛЕЧИТЬ ПРЕСТУПНИКОВ !");
                        }
                    }
                }
                break;
            }
            Menu2(persons);
        }        
        private void Chudo() //Полное восстановление
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
            Menu2(persons);
        }
        public static void Menu(List<Game> persons)
        {
            Game per;
            while (true)
            {
                Console.WriteLine("                                                ~ИГРОВОЕ МЕНЮ~"); Console.WriteLine(persons.Count);
                Console.WriteLine("                         1 - Создать нового персонажа; 2 - Выбрать уже существующего.");
                string? vybor = Console.ReadLine();
                if (vybor == "1")
                {
                    persons.Add(new Game()); //Добавляю в список живых новобранца
                }
                else if (vybor == "2")
                {
                    foreach (Game a in persons) //Выполняю перебор в списке живых
                    {
                        Console.WriteLine("> Имя: ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        string? s = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.White;
                        if (s == a.Name) //Поиск по имени персонажа
                        {
                            per = a;
                            per.Menu2(persons);
                        }
                    }
                }
            }
        }
        private void Menu2(List<Game> persons) //Пользовательский интерфейс 
        {
                /*foreach (Game person in persons)
                    if (x == person.x && y == person.y)
                        if (person._life == true)
                            if (svoychuzhoy != person.svoychuzhoy)*/
                //FightIn(persons);
                Console.WriteLine("                                                ~МЕНЮ ПЕРСОНАЖА~"); Console.WriteLine(persons.Count);
                Console.WriteLine("1 - Показать информацию о персонаже; 2 - Переместиться влево/вправо; 3 - Переместиться вверх/вниз; 4 - Восстановить своё здоровье; 5 - Лечить товарищей; 6 - Выход в иговое меню.");
                string? vybor6 = Console.ReadLine();
                switch (vybor6) 
                {
                    case "1": 
                        Print();
                        break;
                    case "2": 
                        MoveX(persons);
                        break;
                    case "3": 
                        MoveY(persons);
                        break;
                    case "4": 
                        Chudo();
                        break;
                    case "5": 
                        Iscelenie(persons);
                        break;
                    case "6": 
                        Menu(persons);
                        break;
                }
        }
    }
}