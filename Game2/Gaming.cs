using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    internal class Gaming
    {
        private string? name; //Имя персонажа
        private int x; //Координата X
        private int y; //Координата Y
        private bool svoychuzhoy; //Принадлежность к лагерю
        private int heartstek; //Кол-во ОЗ (очков здоровья)
        private int hearts; //Макс. кол-во ОЗ
        private int drugspasdruga; //Получаемое лечение
        private int uron; //Сила (урон)
        private int ulta; //Заряд для ультимативной способности
        private int medaly; //Кол-во убийств
        private int chudo; //Заряд для полного исцеления
        //private int _omen; //метка для удаления объектов из списка врагов
        private int _end; //метка для завершения игры
        static public List<Gaming> persons;
        static public List<Gaming> alive;
        static public List<Gaming> vragy;
        public string? Name { get { return name; } }
        public Gaming() { Info(persons); } //При создании нового перса взываем пользователя к заполнению досье на подопечного
        private void Info(List<Gaming> persons) //Создание персонажа
        {
            Console.WriteLine("> Введите имя персонажа:");
            this.name = Console.ReadLine();
            Console.WriteLine("> Введите место нахождения персонажа (по горизонтали х, ENTER, и по вертикали у):");
            this.x = Convert.ToInt32(Console.ReadLine());
            this.y = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("> Выберите команду (1 или 2):");
            string? vybor = Console.ReadLine();
            if (vybor == "1")
            {
                this.svoychuzhoy = true;
            }
            else if (vybor == "2")
            {
                this.svoychuzhoy = false;
            }
            this.hearts = 100;
            this.heartstek = 100;
            this.ulta = 0;
            this.chudo = 0;
            //this._omen = 0;
            Random random = new();
            this.uron = random.Next(hearts / 2, hearts);
            Console.WriteLine("Данные сохранены.");
        }

        private void Print() //Вывод иннформации
        {
            Console.WriteLine($"[Имя персонажа: {name}]");
            Console.WriteLine($"[Местоположение (горизонталь/вертикаль): {x}/{y}]");
            Console.WriteLine($"[Здоровье (текущее/общее): {heartstek}/{hearts}]");
            Console.WriteLine($"[Урон: {uron}]");
            Console.WriteLine($"[Врагов побеждено: {medaly}]");
            if (svoychuzhoy == true)
            {
                Console.WriteLine("[Полицейский]");
            }
            else
            {
                Console.WriteLine("[Преступник]");
            }
            Igra(persons);
        }


        private void Boy(List<Gaming> vragy, List<Gaming> persons) //Не "мальчик", а БОЙ
        {
            foreach (Gaming g in persons)
            {
                for (int i = 0; i < persons.Count; i++)
                {
                    for (int j = 0; j < persons.Count; j++)
                    {
                        if (i != j)
                        {
                            if (persons[i].svoychuzhoy != persons[j].svoychuzhoy)
                            {
                                vragy.Add(persons[i]);
                            }
                        }
                    }
                }
            }
                    int obshiyuron = 0;
                    int uronvboyu = this.uron / vragy.Count; //Урон игрока делится на кол-во противников
                    if (vragy.Count > 1)
                    {
                        Console.WriteLine("\nОбнаружены враги!\n\nВаши противники: ");

                        foreach (Gaming vrag in vragy) //Вывод списка противников
                        {
                            Console.WriteLine($"{vrag.name}");
                            obshiyuron += vrag.uron; //общий урон отряда противников
                        }
                        Console.WriteLine($"\nСила отряда: {obshiyuron}.");
                    }
                    else
                    {
                        Console.WriteLine("\nОбнаружен враг!");
                        foreach (Gaming vrag in vragy)
                        {
                            obshiyuron += vrag.uron; //общий урон отряда противников
                            Console.WriteLine($"\nСила: {obshiyuron}.");
                        }
                    }

                    string answer = "";

                    if (ulta >= 5)//Если до этой битвы убито 5 противников, появляется возможность примненить ульту
                    {
                        Console.WriteLine("\nВозможно использование ультимативной способности.");
                        while (answer == "")
                        {
                            Console.WriteLine("Использовать? (да/нет)");
                            answer = Console.ReadLine();
                            switch (answer)
                            {
                                case "да":
                                    Ulta(vragy, persons);
                                    Console.WriteLine("Битва окончена.");
                                    break;
                                case "нет":
                                    break;
                                default:
                                    answer = "";
                                    break;
                            }
                        }
                    }

                    if (answer == "нет" || answer == "")
                    {
                        Console.WriteLine("Битва начинается!");

                        while (heartstek > 0 && vragy.Count > 0) //Битва идет пока мы живы и противники есть
                        {
                            if (vragy.Count > 1) //Если враг был или остается один, то выводится его имя. Иначе пишет что нас бьет отряд
                            {
                                Console.WriteLine($"Отряд из {vragy.Count} врагов наносит удар!\n{name} получает {obshiyuron} ед. урона!\n");
                            }
                            else
                            {
                                Console.WriteLine($"{vragy[0].name} наносит удар!\n{name} получает {obshiyuron} ед. урона!\n");
                            }

                            heartstek -= obshiyuron; //Враги первыми наносят урон

                            Console.WriteLine($"{name} наносит ответный удар в размере {uronvboyu} ед. урона!\n");

                            foreach (Gaming vrag in vragy) //Мы бьем каждого из списка врагов
                            {
                                vrag.heartstek -= uronvboyu;
                                if (vrag.heartstek <= 0) //проверка на наличие мертвых врагов
                                {
                                    vrag.Smert(persons); //метка для удаления объекта с отрицательными или нулевыми ОЗ из списка врагов
                                    obshiyuron -= vrag.uron; //уменьшение общего урона на значение силы убитого
                                    Console.WriteLine($"Сила отряда упала на {vrag.uron} ед. урона!\n");
                                }
                            }

                            if (heartstek <= 0) //Если мы получили фатальный урон, то всем противникам дают очки, а нас убивает
                            {
                                Smert(persons);
                                foreach (var vrag in vragy)
                                {
                                    if (this.heartstek <= 0)
                                    {
                                        Console.WriteLine($"{vrag.name} получает очки.");
                                        vrag.medaly++;
                                        vrag.ulta++;
                                        vrag.chudo++;
                                    }
                                }
                            }
                            foreach (Gaming vrag in persons) //перебирается список живых, противники с меткой удаляются, а им за это дают очки
                            {
                                if (vrag.heartstek <= 0)
                                {
                                    Console.WriteLine($"{vrag.name} повержен.\n{name} получает очки.\n");
                                    vragy.Remove(vrag);
                                    medaly++;
                                    ulta++;
                                    chudo++;
                                }
                            }
                            uronvboyu = vragy.Count > 0 ? uron / vragy.Count : uronvboyu; //Пересчет урона отряда (если протиивников больше 0)
                        }
                        Console.WriteLine("Битва окончена.");
                        if (persons.Contains(this) == false && vragy.Count == 0)
                        {
                            Console.WriteLine("Все воины погибли.\n");
                            foreach (var vrag in persons)
                            {
                                if (vrag.heartstek <= 0)
                                {
                                    vrag.medaly++;
                                    vrag.ulta++;
                                    vrag.chudo++;
                                    //vrag._omen = 0;
                                }
                            }
                        }
                        else
                        {
                            if (vragy.Count == 0)
                            {
                                Console.WriteLine("Отряд противников повержен.\n");
                            }
                            else
                            {
                                if (persons.Contains(this) == false)
                                {
                                    Console.WriteLine($"Персонаж {name} погиб.\n");
                                }
                            }
                        }
                    }
                }
                private void Pobeda1(List<Gaming> persons) //Проверка условий завершения игры
                {
                    if (persons.Count(c => c.svoychuzhoy == true) == 0 && alive.Count(c => c.svoychuzhoy == false) == 0) //Если все члены обеих команд погибли
                    {
                        Console.WriteLine("Игра окончена.\nВсе погибли");
                        Itog(persons, alive);
                    }
                    else
                    {
                        if (persons.Count(c => c.svoychuzhoy == true) > 0 && alive.Count(c => c.svoychuzhoy == false) == 0) //если в 1 команде остались живые
                        {
                            Console.WriteLine("Игра окончена.\nПобедила команда 1");
                            Itog(persons, alive);
                        }
                        else
                        {
                            if (persons.Count(c => c.svoychuzhoy == true) == 0 && alive.Count(c => c.svoychuzhoy == false) > 0) //если во 2 команде остались живые
                            {
                                Console.WriteLine("Игра окончена.\nПобедила команда 2");
                                Itog(persons, alive);
                            }
                        }
                    }
                }
                private void Pobeda(List<Gaming> persons, List<Gaming> alive) //Объявление победы
                {
                    switch (_end)
                    {
                        case 4:
                            Console.WriteLine("Игра окончена.");
                            Itog(persons, alive);
                            break;
                    }
                }

                private void Ulta(List<Gaming> vragy, List<Gaming> alive) //ульта
                {
                    Console.WriteLine("Использована ультимативная способность.\nПоверженные враги:");
                    foreach (Gaming vrag in vragy)
                    {
                        ulta -= 5;
                        vrag.heartstek = 0;
                        vrag.Smert(alive);
                        Console.WriteLine(vrag.name);
                        medaly++;
                        chudo++;
                    }
                }
                private void Chudo(List<Gaming> persons) //Полное исцеление
                {
                    foreach (Gaming pers in persons)
                    {
                        if (pers != null)
                        {
                            if (pers.svoychuzhoy == svoychuzhoy && pers.heartstek < pers.hearts && persons.Contains(pers) == true && pers != this && chudo <= 3)
                            {
                                pers.heartstek = hearts;
                                chudo -= 3;
                                Console.WriteLine($"\nИгрок {pers.name} полностью исцелен.");
                            }
                            else
                            {
                                Console.WriteLine("\nНевозможно выполнить лечение.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Адресат лечения задан некорректно.");
                        }
                    }
                }
                private void Aibolit(List<Gaming> persons) //Лечение персонажа
                {
                    foreach (Gaming pers in persons)
                    {
                        if (pers != null)
                        {
                            Console.WriteLine($"Введите количетсво своих ОЗ, которые хотите отдать союзнику {pers.name}:");
                            drugspasdruga = Convert.ToInt32(Console.ReadLine());
                            if (drugspasdruga > 0 && heartstek > drugspasdruga && pers.svoychuzhoy == svoychuzhoy && persons.Contains(pers) == true && pers != this && pers.hearts - pers.heartstek >= drugspasdruga)
                            {
                                pers.heartstek += drugspasdruga;
                                heartstek -= drugspasdruga;
                                Console.WriteLine($"\nИгрок {pers.name} исцелен на {drugspasdruga} ОЗ.\nТекущее кол-во ОЗ {pers.name}: {pers.heartstek}.\nТекущее кол-во ОЗ: {heartstek}.");
                            }
                            else
                            {
                                Console.WriteLine("\nНевозможно выполнить лечение.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Адресат лечения задан некорректно.");
                        }
                    }
                    Igra(persons);
                }

                private void Smert(List<Gaming> persons) //Смерть персонажа
                {
                    persons.Remove(this);
                }
                private void moveX()//перемещение по x
                {
                    Console.WriteLine("Влево или вправо?");
                    string? levprav = Console.ReadLine();
                    switch (levprav)
                    {
                        case "влево":
                            x -= 1;
                            break;
                        case "вправо":
                            x += 1;
                            break;
                    }
                    for (int i = 0; i < persons.Count; i++) //Для одного участника движения ...
                    {
                        for (int j = 0; j < persons.Count; j++) //...и для другого
                        {
                            if (i != j)
                            {
                                if (persons[i].x == persons[j].x && persons[i].y == persons[j].y)
                                {
                                    Boy(vragy, persons);
                                }
                                else
                                {
                                    Igra(persons);
                                }
                            }
                        }
                    }
                }
                private void moveY()//перемещение по y
                {
                    Console.WriteLine("Вперёд или назад?");
                    string? perzad = Console.ReadLine();
                    switch (perzad)
                    {
                        case "вперёд":
                            y += 1;
                            break;
                        case "назад":
                            y -= 1;
                            break;
                    }
                    for (int i = 0; i < persons.Count; i++) //Для одного участника движения ...
                    {
                        for (int j = 0; j < persons.Count; j++) //...и для другого
                        {
                            if (i != j)
                            {
                                if (persons[i].x == persons[j].x && persons[i].y == persons[j].y)
                                {
                                    Boy(vragy, persons);
                                }
                                else
                                {
                                    Igra(persons);
                                }
                            }
                        }
                    }
                }
                private Gaming ProverkanaXY(List<Gaming> characters, int X, int Y, List<Gaming> alive) //Поиск персонажа по его местоположению
                {
                    foreach (Gaming pers in characters) //Перебор объектов в массиве
                    {
                        if (pers.x == X && pers.y == Y && pers != this && alive.Contains(pers) == true) //Проверка элементов массива на соответствие искомомым координатам
                        {
                            return pers; //Возврат искомого элемента массива
                        }
                    }
                    return null;    //Если объект не был найден, вернется пустое значение
                }

                private Gaming Saymyname(List<Gaming> persons, string name) //поиск по имени
                {
                    foreach (Gaming pers in persons)
                    {
                        if (name == pers.name)
                        {
                            return pers;
                        }
                    }
                    return null;
                }

                private void Rozhdenie(List<Gaming> persons, List<Gaming> alive, bool team) //Создание персонажа
                {
                    while (name == "" || svoychuzhoy == null || x == null || y == null || hearts <= 0)
                    {
                        Console.WriteLine("\nНеобходимо создать персонажа, чтобы продолжить.\nВведите имя (должно быть уникальным):");
                        string nameChar = Console.ReadLine();
                        Console.WriteLine("Введите количетсво ОЗ (очков здоровья) персонажа:");
                        int hpChar = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Задайте местоположение вашего персонажа.\nВведите X:");
                        int XChar = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Введите Y:");
                        int YChar = Convert.ToInt32(Console.ReadLine());

                        foreach (Gaming pers in persons) //Ищет объект с таким же именем
                        {
                            if (nameChar == pers.name)
                            {
                                nameChar = "";
                                Console.WriteLine("Персонаж с таким именем уже существует.");
                            }
                        }

                        if (hpChar <= 0 || nameChar == "" || nameChar == null)
                        {
                            Console.WriteLine("Какие-то из значений введены некорректно. Попробуйте снова.\n");
                        }
                        else
                        {
                            if (ProverkanaXY(persons, XChar, YChar, alive) != null)
                            {
                                if (ProverkanaXY(persons, XChar, YChar, alive).svoychuzhoy != team) //Не позволяет разместить персонажа там, где враги
                                {
                                    Console.WriteLine("Введенные координаты занял враг. Разместите своего персонажа в другом месте.\n");
                                }
                                else
                                {
                                    Info(persons);
                                    alive.Add(this);
                                }
                            }
                            else
                            {
                                Info(persons);
                                alive.Add(this);
                            }
                        }
                    }
                }
                private void Sozdanie(List<Gaming> characters, List<Gaming> alive, bool team) //Применение метода создания персонажей 
                {
                    Console.Write("\nСоздайте игроков для ");
                    if (team == true)
                    {
                        Console.Write("первой команды\n");
                    }
                    else
                    {
                        Console.Write("второй команды\n");
                    }

                    string answer = "";
                    while (answer != "нет")
                    {
                        Gaming character = new Gaming();
                        character.Rozhdenie(characters, alive, team);
                        characters.Add(character);

                        do
                        {
                            Console.WriteLine("\nПродолжить? (да/нет)\n");
                            answer = Console.ReadLine();
                            switch (answer)
                            {
                                case "да":
                                case "нет":
                                    break;
                                default:
                                    answer = "";
                                    break;
                            }
                        } while (answer != "да" && answer != "нет");
                    }
                }
                private void Svoii(List<Gaming> persons) //  Вывод всех персонажей с нумерацией
                {
                    Console.WriteLine();
                    foreach (Gaming pers in persons)
                    {
                        //int i = 1;
                        //Console.WriteLine($"[{i}.]");
                        Console.WriteLine($"[Имя персонажа: {pers.name}]");
                        Console.WriteLine($"[Местоположение (горизонталь/вертикаль): {pers.x}/{pers.y}]");
                        Console.WriteLine($"[Здоровье (текущее/общее): {pers.heartstek}/{pers.hearts}]");
                        Console.WriteLine($"[Урон: {pers.uron}]");
                        Console.WriteLine($"[Врагов побеждено: {pers.medaly}]");
                        if (pers.svoychuzhoy == true)
                        {
                            Console.Write("Полицейский");
                        }
                        else
                        {
                            Console.Write("Преступник");
                        }
                        if (persons.Contains(pers) == true)
                        {
                            Console.Write("[жив]");
                        }
                        else
                        {
                            Console.Write("[мертв]");
                        }
                        Igra(persons);
                        //i++;
                    }
                }
                private void Itog(List<Gaming> persons, List<Gaming> alive) //Вывод статистики в конце игры
                {
                    Console.WriteLine("\nКоманда 1:");
                    int i = 1;
                    foreach (Gaming pers in persons)
                    {
                        if (pers.svoychuzhoy == true)
                        {
                            Console.Write($"{i}. {pers.name}");
                            if (alive.Contains(pers) == true)
                            {
                                Console.Write(" - жив");
                            }
                            else
                            {
                                Console.Write(" - мертв");
                            }
                            Console.Write($" - Убито: {pers.medaly}\n");
                            i++;
                        }
                    }

                    Console.WriteLine("\nКоманда 2:");
                    i = 1;
                    foreach (Gaming pers in persons)
                    {
                        if (pers.svoychuzhoy == false)
                        {
                            Console.Write($"{i}. {pers.name}");
                            if (alive.Contains(pers) == true)
                            {
                                Console.Write(" - жив");
                            }
                            else
                            {
                                Console.Write(" - мертв");
                            }
                            Console.Write($" - Убито: {pers.medaly}\n");
                            i++;
                        }
                    }
                }
                public static void Menu(List<Gaming> persons) //Метод для вызова общего метода
                {
                    Gaming per;
                    while (true)
                    {
                        Console.WriteLine("                                                ~ИГРОВОЕ МЕНЮ~"); Console.WriteLine(persons.Count);
                        Console.WriteLine("                         1 - Создать нового персонажа; 2 - Выбрать уже существующего.");
                        string? vybor = Console.ReadLine();
                        if (vybor == "1")
                        {
                            persons.Add(new Gaming()); //Добавляю в список живых новобранца
                        }
                        else if (vybor == "2")
                        {
                            foreach (Gaming a in persons) //Выполняю перебор в списке живых
                            {
                                Console.WriteLine("Введите имя персонажа: ");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                string? s = Console.ReadLine();
                                Console.ForegroundColor = ConsoleColor.White;
                                if (s == a.Name) //Поиск по имени персонажа
                                {
                                    per = a;
                                    per.Igra(persons);
                                }
                            }
                        }
                    }
                }
                private void Igra(List<Gaming> persons) //Общий метод
                {
                    Console.WriteLine("                                                                      ~МЕНЮ ПЕРСОНАЖА~");
                    Console.WriteLine("1 - Посмотреть информацию о себе; 2 - Переместиться влево/вправо; 3 - Переместиться вперёд/назад; \n4 - Посмотреть список своих товарищей; 5 -  Лечение; 6 - Лечение товарищей; 7 - Вернуться в игровое меню.");
                    string? vybor = Console.ReadLine();
                    switch (vybor)
                    {
                        case "1":
                            Print();
                            break;
                        case "2":
                            moveX();
                            break;
                        case "3":
                            moveY();
                            break;
                        case "4":
                            Svoii(persons);
                            break;
                        case "5":
                            Aibolit(persons);
                            break;
                        case "6":
                            Console.WriteLine();
                            break;
                        case "7":
                            Menu(persons);
                            break;
                    }
                }
            }
        }
            /*Console.WriteLine("Добро пожаловать в Игру.\nПРАВИЛА:\nУничтожьте всех членов вражеской команды до того, как они уничтожат вас. Поддерживайте союзников лечением, группируйтесь в отряды и устраивайте засады.\nПолное исцеление: 3 очка;\nУльтимативная способность: 5 очков.");
            Sozdanie(persons, alive, true); //Создание персонажей 1 команды
            Sozdanie(persons, alive, false);//Создание персонажей 2 команды
            Console.WriteLine("\nСоздание персонажей завершено.\nВыберите персонажа, за которого хотите играть:\n");
            Svoii(persons, alive);//Вывод списка персонажей
            string answ = "да"; //значение пропускает первый цикл в следующем цикле
            string numb = "";
            while (answ != "нет")
            {
                while (answ == "")
                {
                    Console.WriteLine("\nВернуться к выбору персонажа? (да/нет)\n");
                    answ = Console.ReadLine();
                    switch (answ)
                    {
                        case "да":
                            Svoii(persons, alive);
                            numb = "";
                            break;
                        case "нет":
                            numb = " ";
                            break;
                        default:
                            answ = "";
                            break;
                    }
                }

                while (numb == "")
                {
                    Console.WriteLine("\nВведите номер персонажа:\n");
                    numb = Console.ReadLine();
                    if (Convert.ToInt32(numb) > 0 && Convert.ToInt32(numb) <= persons.Count()) //Проверка вхождения введенного номера в установленные рамки (больше нуля и меньше вол-ва объектов в списке)
                    {
                        Console.WriteLine($"\nМеню персонажа {persons[Convert.ToInt32(numb) - 1].name}.\nВыберите действия:\n");//процесс игры
                        string continuation = "";

            Console.WriteLine("1 - Вывод информации; 2 - Переместиться (влево/вправо); 3 - Переместиться (вверх/вниз); 4 - Лечение товарища; 5 - Полное исцеление товарища; 6 - Завершить игру.");
            string choise = Console.ReadLine();
            switch (choise)
            {
                case "1":
                    Print();
                    break;
                case "2":
                    moveX();
                    break;
                case "3":
                    Console.WriteLine("\nВведите имя союзника, которому хотите отдать свои ОЗ:");
                    string namefriend = Console.ReadLine();
                    persons[Convert.ToInt32(numb) - 1].Aibolit(persons[Convert.ToInt32(numb) - 1].Saymyname(persons, namefriend), alive);
                    break;
                case "4":
                    Console.WriteLine("\nВведите имя союзника, которого хотите полностью иссцелить:");
                    string namefriend2 = Console.ReadLine();
                    persons[Convert.ToInt32(numb) - 1].Chudo(persons[Convert.ToInt32(numb) - 1].Saymyname(persons, namefriend2), alive);
                    break;
                case "5":
                    Console.WriteLine("\nВы уверены, что хотите завершить игру? (да/нет)\n");
                    string answer = Console.ReadLine();
                    if (answer == "да")
                    {
                        persons[Convert.ToInt32(numb) - 1]._end = 4;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nОтмена.\n");
                    }
                    break;
                default:
                    Console.WriteLine("\nВведено некорректное значение.\n");
                    break;
            }

            if (persons[Convert.ToInt32(numb) - 1]._end == 0)
            {
                Console.WriteLine("\nЧтобы продолжить, нажмите \"Enter\".\nЧтобы выйти в меню выбора персонажа, напишите что-нибудь и нажмите \"Enter\".\n");
                continuation = Console.ReadLine();
                answ = "";
            }
            else
            {
                break;
            }
        }
                        else
                        {
                            Console.WriteLine($"\nПерсонаж {persons[Convert.ToInt32(numb) - 1].name} погиб.\n\nЧтобы продолжить, нажмите \"Enter\".\nЧтобы выйти в меню выбора персонажа, напишите что-нибудь и нажмите \"Enter\".\n");
                            continuation = Console.ReadLine();
                            answ = "";
                        }
}
if (persons[Convert.ToInt32(numb) - 1]._end != 0)
{
    persons[Convert.ToInt32(numb) - 1].Pobeda(persons, alive);
    answ = "нет";
    numb = " ";
    break;
}
                }
                else
{
    Console.WriteLine("\nВыбран несуществующий номер персонажа.\n");
    numb = "";
}*/
