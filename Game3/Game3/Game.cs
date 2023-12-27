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
        private int[] _coor = new int[2]; //*
        private bool svoychuzhoy;
        private int heartstek;
        private int hearts;
        private bool _life = true; //*
        private int uron;
        private int medaly;
        static public List<Game> persons;
        public string Name { get { return name; } }
        public int CoorX { get { return _coor[0]; } } //*
        public int CootY { get { return _coor[1]; } } //*
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
            Console.Write("> Сторона ('полицейские' или 'преступники'):");
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
            Console.WriteLine("> "); //Координаты
            Console.ForegroundColor = ConsoleColor.Cyan;
            this.name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            //name = s[0];
            //hearts = Int32.Parse(s[2]);
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
            Console.WriteLine($"[Местоположение: ({_coor[0]},{_coor[1]}) ~");
            Console.WriteLine($"[Здоровье: {heartstek}/{hearts} ~");
            Console.WriteLine($"[Лагерь: {svoychuzhoy} ~");
            Console.WriteLine($"[Урон: {uron} ~");
            Console.WriteLine($"[Очки побед: {medaly} ~");
        }
        //Перемещение
        private void Move(List<Game> persons)
        {
            Console.WriteLine("> Идти: | A | V | < | > |");
            while (true)
            {
                if (_life == false) return;
                if (persons.Count(Game => Game._life == true && Game.svoychuzhoy == false) == 0) return;
                if (persons.Count(Game => Game._life == true && Game.svoychuzhoy == true) == 0) return;
                ConsoleKeyInfo c = Console.ReadKey();
                switch (c.Key)
                {
                    case ConsoleKey.UpArrow: _coor[1] += 1; break;
                    case ConsoleKey.DownArrow: _coor[1] -= 1; break;
                    case ConsoleKey.LeftArrow: _coor[0] -= 1; break;
                    case ConsoleKey.RightArrow: _coor[0] += 1; break;
                    default: return;
                }
                Console.WriteLine($"{_coor[0]},{_coor[1]}");
                foreach (Game p in persons)
                    if (_coor[0] == p._coor[0] && _coor[1] == p._coor[1])
                        if (p._life == true)
                            if (svoychuzhoy != p.svoychuzhoy)
                                FightIn(persons);
            }
        }
        //Интер битвы
        private void FightIn(List<Game> persons)
        {
            if (_life == false) return;
            if (persons.Count(Game => Game._life == true && Game.svoychuzhoy == false) == 0) return;
            if (persons.Count(Game => Game._life == true && Game.svoychuzhoy == true) == 0) return;
            Console.Write
            ("---------------| УВАГА |---------------\n" +
             "> На вашем пути враги, что будете делать:\n" +
             "1. Сражаться\n" +
             "2. Бежать\n" +
             "3. Ульта\n" +
             ">");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1: Boy(persons); break;
                case 2: break;
                case 3: Ulta(persons); break;
            }
        }
        //Битва
        private void Boy(List<Game> persons) //Не "мальчик", а БОЙ
        {
            //поиск персонженей на клетке
            List<Game> persFildFren = new List<Game>();
            List<Game> persFildEnem = new List<Game>();
            foreach (Game p in persons)
                if (_coor[0] == p._coor[0] && _coor[1] == p._coor[1])
                    if (p._life == true)
                        if (svoychuzhoy != p.svoychuzhoy)
                            persFildEnem.Add(p);
                        else
                            persFildFren.Add(p);
            //перечисление учасников битвы
            Console.WriteLine("> Ваша команда:");
            foreach (Game p in persFildFren)
                p.Print();
            Console.WriteLine("> Команда противника:");
            foreach (Game p in persFildEnem)
                p.Print();
            Console.WriteLine("> Нажмите Enter, чтобы продолжить...");
            Console.ReadLine();
            while (true)
            {
                //создание переменных урона
                int FrenDam = 0;
                int EnemDam = 0;
                //суммирование урона живых членов команд
                foreach (Game p in persFildFren)
                    FrenDam += p.uron;
                foreach (Game p in persFildEnem)
                    EnemDam += p.uron;
                //деление суммарного урона на количество противников 
                FrenDam /= persFildEnem.Count;
                EnemDam /= persFildFren.Count;
                //нанесение урона 
                foreach (Game p in persFildFren)
                {
                    p.heartstek -= EnemDam;
                    if (p.heartstek <= 0)
                        p._life = false;
                }
                foreach (Game p in persFildEnem)
                {
                    p.heartstek -= FrenDam;
                    if (p.heartstek <= 0)
                        p._life = false;
                }
                //проверка жив ли гг 
                if (_life == false)
                { Console.WriteLine("\n------------| Вы умерли |------------\n"); return; }
                if (persons.Count(Game => Game._life == true && Game.svoychuzhoy == svoychuzhoy) == 0) return;
                if (persons.Count(Game => Game._life == true && Game.svoychuzhoy != svoychuzhoy) == 0) return;
                Console.WriteLine("\n----| Вы смогли отбросить врага и нашли момент восстановить силы. |----\n");
                //проверка ОЗ
                Console.WriteLine("-------------------------------\n" + "ОЗ вашей команды: ");
                foreach (Game p in persFildFren)
                    if (p._life == true)
                        Console.WriteLine($"Имя: {p.name}, ОЗ: {p.heartstek}, урона получено: {EnemDam}");
                Console.WriteLine("\n-------------------------------\n" + "ОЗ ваших врагов: ");
                foreach (Game p in persFildEnem)
                    if (p._life == true)
                        Console.WriteLine($"Имя: {p.name}, ОЗ: {p.heartstek}, урона получено: {FrenDam}");
                //выбор дествий в бою
                while (true)
                {
                    Console.Write
                    ("\n--------------------\n" +
                     "Что будете делать?\n" +
                     "1. Сражаться дальше\n" +
                     "2. Восстановить ОЗ\n" +
                     "3. Лечить союзников\n" +
                     "4. Ульта\n" +
                     "5. Бежать\n" +
                     ">");
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 1: break;
                        case 2: Chudo(); break;
                        case 3: Iscelenie(persons); break;
                        case 4: Ulta(persFildEnem); break;
                        case 5: return;
                    }
                    break;
                }
                //проверка кто победил 
                if (persFildEnem.Count(Game => Game._life == true) == 0)
                {
                    Console.WriteLine("------------| ПОБЕДА |------------\n" + "Идти: | A | V | < | > |");
                    medaly += 1;
                    return;
                }
                Console.WriteLine("Битва продолжается...");
            }
        }

        //Уничтожение 
        private void Ulta(List<Game> persons)
        {
            if (medaly >= 10)
            {
                foreach (Game p in persons)
                    if (_coor[0] == p._coor[0] && _coor[1] == p._coor[1])
                        if (svoychuzhoy != p.svoychuzhoy)
                            p._life = false;
                Console.WriteLine("> Все враги уничтожены");
            }
            else
            {
                Console.WriteLine("> Для ульты необходимо хотя бы 10 побед.\n" + "Битва неизбежна");
                Boy(persons);
            }
        }
        //Лечение
        private void Iscelenie(List<Game> persons)
        {
            while (true)
            {
                Console.Write("> Выберете кого будете лечить:\n" + ">");
                string srh = Console.ReadLine();
                foreach (Game p in persons)
                {
                    if (srh == p.name)
                    {
                        if (p.svoychuzhoy == svoychuzhoy)
                        {
                            Console.Write("> Сколько восстановить оз:\n" + ">");
                            int hp = Convert.ToInt32(Console.ReadLine());
                            if (hp < heartstek)
                            {
                                if (hp < p.hearts)
                                {
                                    p.heartstek += hp;
                                    heartstek -= hp;
                                    break;
                                }
                                else Console.WriteLine("> Нельзя лечить больше максимального ОЗ");
                            }
                            else Console.WriteLine("> Вы у мамы один, а товарищей много.\n" + "Нельзя тратить здоровья больше чем имеется");
                        }
                        else Console.WriteLine("> Врагов лечить нельзя");
                    }
                }
                break;
            }
        }
        //Полное восстановление 
        private void Chudo()
        {
            if (medaly >= 5)
            {
                heartstek = hearts;
                medaly -= 5;
                Console.WriteLine("> Вы полностью восстановили ОЗ");
            }
            else Console.WriteLine("> Победи в 5 битвах, чтобы восстановить ОЗ");
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
        //Пользовательский интерфейс 
        private void Menu2(List<Game> persons)
        {
            foreach (Game p in persons)
                if (_coor[0] == p._coor[0] && _coor[1] == p._coor[1])
                    if (p._life == true)
                        if (svoychuzhoy != p.svoychuzhoy)
                            FightIn(persons);
            while (true)
            {
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
                    else
                    {
                        Console.Write
                        ("--------------------------------\n" +
                         "> Выберете необходимое действие:\n" +
                         "1. Показать данные персонажа\n" +
                         "2. Передвижение\n" +
                         "3. Восстановить ОЗ\n" +
                         "4. Лечить союзников\n" +
                         "5. Выход\n" +
                         ">");
                        switch (Convert.ToInt32(Console.ReadLine()))
                        {
                            case 1: Print(); break;
                            case 2: Move(persons); break;
                            case 3: Chudo(); break;
                            case 4: Iscelenie(persons); break;
                            case 5: return;
                        }
                    }
                }
            }
        }
    }
}