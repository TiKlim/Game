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
    private int _omen; //метка для удаления объектов из списка врагов
    private int _end; //метка для завершения игры
    static public List<Gaming> persons;
    static public List<Gaming> alive;
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
        this._omen = 0;
        Random random = new();
        this.uron = random.Next(hearts / 2, hearts);
        Console.WriteLine("\nСоздание персонажа завершено.\n");
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
    }


    private void Boy(List<Gaming> opponents, List<Gaming> alive) //Не "мальчик", а БОЙ
    {
        int partyDamage = 0;
        int separatedDamage = uron / opponents.Count; //Урон игрока делится на кол-во противников
        if (opponents.Count > 1)
        {
            Console.WriteLine("\nОбнаружены враги!\n\nВаши противники: ");

            foreach (Gaming opponent in opponents) //Вывод списка противников
            {
                Console.WriteLine($"{opponent.name}");
                partyDamage += opponent.uron; //общий урон отряда противников
            }
            Console.WriteLine($"\nСила отряда: {partyDamage}.");
        }
        else
        {
            Console.WriteLine("\nОбнаружен враг!");
            foreach (Gaming opponent in opponents)
            {
                partyDamage += opponent.uron; //общий урон отряда противников
                Console.WriteLine($"\nСила: {partyDamage}.");
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
                        Ulta(opponents, alive);
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
            Console.WriteLine("\nБитва начинается!\n");

            while (heartstek > 0 && opponents.Count > 0) //Битва идет пока мы живы и противники есть
            {
                if (opponents.Count > 1) //Если враг был или остается один, то выводится его имя. Иначе пишет что нас бьет отряд
                {
                    Console.WriteLine($"Отряд из {opponents.Count} врагов наносит удар!\n{name} получает {partyDamage} ед. урона!\n");
                }
                else
                {
                    Console.WriteLine($"{opponents[0].name} наносит удар!\n{name} получает {partyDamage} ед. урона!\n");
                }

                heartstek -= partyDamage; //Враги первыми наносят урон

                Console.WriteLine($"{name} наносит ответный удар в размере {separatedDamage} ед. урона!\n");

                foreach (Gaming opponent in opponents) //Мы бьем каждого из списка врагов
                {
                    opponent.heartstek -= separatedDamage;
                    if (opponent.heartstek <= 0) //проверка на наличие мертвых врагов
                    {
                        opponent._omen = 1; //метка для удаления объекта с отрицательными или нулевыми ОЗ из списка врагов
                        partyDamage -= opponent.uron; //уменьшение общего урона на значение силы убитого
                        Console.WriteLine($"Сила отряда упала на {opponent.uron} ед. урона!\n");
                    }
                }

                if (heartstek <= 0) //Если мы получили фатальный урон, то всем противникам дают очки, а нас убивает
                {
                    Smert(alive);
                    foreach (var opponent in opponents)
                    {
                        if (opponent._omen == 0)
                        {
                            Console.WriteLine($"{opponent.name} получает очки.");
                            opponent.medaly++;
                            opponent.ulta++;
                            opponent.chudo++;
                        }
                    }
                }

                foreach (Gaming opponent in alive) //перебираеьтся сприсок живых, противники с меткой удаляются, а им за это дают очки
                {
                    if (opponent._omen == 1)
                    {
                        Console.WriteLine($"{opponent.name} повержен.\n{name} получает очки.\n");
                        opponents.Remove(opponent);
                        medaly++;
                        ulta++;
                        chudo++;
                    }
                }

                separatedDamage = opponents.Count > 0 ? uron / opponents.Count : separatedDamage; //Пересчет урона отряда (если протиивников больше 0)
            }

            Console.WriteLine("\nБитва окончена.\n");

            if (alive.Contains(this) == false && opponents.Count == 0)
            {
                Console.WriteLine("Все воины погибли.\n");
                foreach (var opponent in alive)
                {
                    if (opponent._omen == 1)
                    {
                        opponent.medaly++;
                        opponent.ulta++;
                        opponent.chudo++;
                        opponent._omen = 0;
                    }
                }
            }
            else
            {
                if (opponents.Count == 0)
                {
                    Console.WriteLine("Отряд противников повержен.\n");
                }
                else
                {
                    if (alive.Contains(this) == false)
                    {
                        Console.WriteLine($"Персонаж {name} погиб.\n");
                    }
                }
            }
        }
    }

    private void Pobeda1(List<Gaming> alive) //Проверка условий завершения игры
    {
        if (alive.Count(c => c.svoychuzhoy == true) == 0 && alive.Count(c => c.svoychuzhoy == false) == 0) //Если все члены обеих команд погибли
        {
            _end = 1;
        }
        else
        {
            if (alive.Count(c => c.svoychuzhoy == true) > 0 && alive.Count(c => c.svoychuzhoy == false) == 0) //если в 1 команде остались живые
            {
                _end = 2;
            }
            else
            {
                if (alive.Count(c => c.svoychuzhoy == true) == 0 && alive.Count(c => c.svoychuzhoy == false) > 0) //если во 2 команде остались живые
                {
                    _end = 3;
                }
            }
        }
    }

    private void Pobeda(List<Gaming> persons, List<Gaming> alive) //Объявление победы
    {
        switch (_end)
        {
            case 1:
                Console.WriteLine("Игра окончена.\nВсе погибли");
                Itog(persons, alive);
                break;
            case 2:
                Console.WriteLine("Игра окончена.\nПобедила команда 1");
                Itog(persons, alive);
                break;
            case 3:
                Console.WriteLine("Игра окончена.\nПобедила команда 2");
                Itog(persons, alive);
                break;
            case 4:
                Console.WriteLine("Игра окончена.");
                Itog(persons, alive);
                break;
        }
    }

    private void Ulta(List<Gaming> opponents, List<Gaming> alive) //ульта
    {
        Console.WriteLine("Использована ультимативная способность.\nПоверженные враги:");
        foreach (Gaming opponent in opponents)
        {
            ulta -= 5;
            opponent.heartstek = 0;
            opponent.Smert(alive);
            Console.WriteLine(opponent.name);
            medaly++;
            chudo++;
        }
    }

    private void Chudo(Gaming Menur, List<Gaming> alive) //Полное исцеление
    {
        if (Menur != null)
        {
            if (Menur.svoychuzhoy == svoychuzhoy && Menur.heartstek < Menur.hearts && alive.Contains(Menur) == true && Menur != this && chudo <= 3)
            {
                Menur.heartstek = hearts;
                chudo -= 3;
                Console.WriteLine($"\nИгрок {Menur.name} полностью исцелен.");
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

    private void Aibolit(Gaming Menur, List<Gaming> alive) //Лечение персонажа
    {
        if (Menur != null)
        {
            Console.WriteLine($"Введите количетсво своих ОЗ, которые хотите отдать союзнику {Menur.name}:");
            drugspasdruga = Convert.ToInt32(Console.ReadLine());
            if (drugspasdruga > 0 && heartstek > drugspasdruga && Menur.svoychuzhoy == svoychuzhoy && alive.Contains(Menur) == true && Menur != this && Menur.hearts - Menur.heartstek >= drugspasdruga)
            {
                Menur.heartstek += drugspasdruga;
                heartstek -= drugspasdruga;
                Console.WriteLine($"\nИгрок {Menur.name} исцелен на {drugspasdruga} ОЗ.\nТекущее кол-во ОЗ {Menur.name}: {Menur.heartstek}.\nТекущее кол-во ОЗ: {heartstek}.");
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

    private void Smert(List<Gaming> alive) //Смерть персонажа
    {
        alive.Remove(this);
    }

    private void moveX()//перемещение по x
    {
        x = x;
    }

    private void moveY()//перемещение по y
    {
        y = y;
    }

    private Gaming ProverkanaXY(List<Gaming> characters, int X, int Y, List<Gaming> alive) //Поиск персонажа по его местоположению
    {
        foreach (Gaming Menur in characters) //Перебор объектов в массиве
        {
            if (Menur.x == X && Menur.y == Y && Menur != this && alive.Contains(Menur) == true) //Проверка элементов массива на соответствие искомомым координатам
            {
                return Menur; //Возврат искомого элемента массива
            }
        }
        return null;    //Если объект не был найден, вернется пустое значение
    }

    private Gaming Saymyname(List<Gaming> persons, string name) //поиск по имени
    {
        foreach (Gaming Menur in persons)
        {
            if (name == Menur.name)
            {
                return Menur;
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

            foreach (Gaming Menur in persons) //Ищет объект с таким же именем
            {
                if (nameChar == Menur.name)
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

    private void Svoii(List<Gaming> persons, List<Gaming> alive) //  Вывод всех персонажей с нумерацией
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

    public static void Menu(List<Gaming> persons, List<Gaming> alive) //Метод для вызова общего метода
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
                        per.Igra(persons, alive);
                    }
                }
            }
        }
    }

    private void Igra(List<Gaming> persons, List<Gaming> alive) //Общий метод
    {
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
                    string continuation = "";*/

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
                }

