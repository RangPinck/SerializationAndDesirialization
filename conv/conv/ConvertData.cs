using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace conv
{
    class ConvertData
    {
        /// <summary>
        /// путь к файлу с которым будет работать пользователь
        /// </summary>
        public string PathToFile { set; get; }

        /// <summary>
        /// Полчение формата файла по пути к нему
        /// </summary>
        /// <returns>
        /// строку формата "csv","yaml" и тп.
        /// </returns>
        string GetFormatFileOfPath()
        {
            string path = PathToFile;
            path = new string(path.Reverse().ToArray());
            path = path.Substring(0, path.IndexOf('.'));
            return new string(path.Reverse().ToArray());
        }

        /// <summary>
        /// Проверка сущестования файла по указанному пути
        /// </summary>
        /// <returns>
        /// true - файл есть
        /// false - файла нет
        /// </returns>
        bool CheckingTheExistenceOfTheFile()
        {
            return File.Exists(PathToFile);
        }

        /// <summary>
        /// Проверка файла на соотвествующий формат
        /// </summary>
        /// <returns>
        /// true - файл форматов: "csv", "yaml", "json", "xml"
        /// false - формат файла не указан или не соответсвует списку в категории true
        /// </returns>
        bool CheckingForTheCorrectFileFormat()
        {
            string format = GetFormatFileOfPath();
            if (format == "csv" || format == "yaml" || format == "xml" || format == "json")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод который проверяет введёные пользователем данные и создаёт нужную модель передавая её дальше для чтения
        /// </summary>
        public void ReadFile()
        {
            if (CheckingTheExistenceOfTheFile() && CheckingForTheCorrectFileFormat())
            {
                switch (GetModelNumberForSwith())
                {
                    case 1:
                        List<Programm> program = ReadModelFromFile(new List<Programm>());
                        SorstAndSerchGetsModel(program);
                        break;
                    case 2:
                        List<Place> place = ReadModelFromFile(new List<Place>());
                        SorstAndSerchGetsModel(place);
                        break;
                    default:
                        Console.WriteLine("Выбранного вами варианта нет! Перезапустите программу для повторного ввода");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Файл не существует или не того формата, который невозможно обработать с помощью даннй программы!\nПроверьте путь к файлу, его существование и формат!");
            }
        }

        /// <summary>
        /// Получение нидекса модели по диалогу с пользователем
        /// </summary>
        /// <returns>
        /// Возвращаемые индексы:
        /// 1 - модель "Программа"
        /// 2 - модель "Место"
        /// 0 - ничего (заглушка при ошибке)
        /// </returns>
        int GetModelNumberForSwith()
        {
            Console.Write("Выберите модель:\n1. Программа\n2. Место\nИндекс вашего выбора:\t");
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Вы ввели не тот символ!");
            }

            return 0;
        }

        /// <summary>
        /// метод чтения с файла уже определённого формата модели
        /// </summary>
        /// <typeparam name="T">тип данных модели - созданной структуры</typeparam>
        /// <param name="model">список, куда данные будут записываться для последующего вывода</param>
        List<T> ReadModelFromFile<T>(List<T> model)
        {
            SerializationAndDeserialization ser = new SerializationAndDeserialization();
            switch (GetFormatFileOfPath())
            {
                //чтение данных с csv
                case "csv":
                    model = ser.CSVRead(model, PathToFile);
                    break;
                //чтение данных с json
                case "json":
                    model = ser.JSONRead(model, PathToFile);
                    break;
                //чтение данных с xml
                case "xml":
                    model = ser.XMLRead(model, PathToFile);
                    break;
                //чтение данных с yaml
                case "yaml":
                    model = ser.YAMLRead(model, PathToFile);
                    break;
            }

            return model;
        }

        /// <summary>
        /// Метод который проверяет введёные пользователем данные и создаёт нужную модель передавая её дальше для записи
        /// </summary>
        public void WriteFile()
        {
            if (CheckingForTheCorrectFileFormat())
            {
                switch (GetModelNumberForSwith())
                {
                    case 1:
                        Console.Write("Выюерите тип заполнения данными:\n1. Ручной ввод\n2. Тестовые готовые данные\nИндекс вашего выбора: ");
                        int variosProgramm = int.Parse(Console.ReadLine());
                        List<Programm> templateProgramm = new List<Programm>();

                        switch (variosProgramm)
                        {
                            case 1:
                                templateProgramm = GetListPrograms(); break;
                            case 2:
                                templateProgramm = ListProgrammForTest(); break;
                        }

                        if (templateProgramm.Count == 0)
                        {
                            Console.WriteLine("В ведённых данных ошибка! Повторите ввод данных!");
                            return;
                        }
                        WriteModelFromFile(templateProgramm);
                        break;
                    case 2:
                        Console.Write("Выюерите тип заполнения данными:\n1. Ручной ввод\n2. Тестовые готовые данные\nИндекс вашего выбора: ");
                        int variosPlace = int.Parse(Console.ReadLine());
                        List<Place> templatePlace = new List<Place>();

                        switch (variosPlace)
                        {
                            case 1:
                                templatePlace = GetListPlaces(); break;
                            case 2:
                                templatePlace = ListPlacesForTest(); break;
                        }
                        WriteModelFromFile(templatePlace);
                        break;
                    default:
                        Console.WriteLine("Выбранного вами варианта нет! Перезапустите программу для повторного ввода");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Файл не того формата, который невозможно обработать с помощью даннй программы!\nПроверьте формат файла!");
            }
        }

        /// <summary>
        /// метод записи с файла уже определённого формата модели
        /// </summary>
        /// <typeparam name="T">тип данных модели - созданной структуры</typeparam>
        /// <param name="model">список, откуда будут браться  данные для записи </param>
        void WriteModelFromFile<T>(List<T> model)
        {
            SerializationAndDeserialization ser = new SerializationAndDeserialization();
            switch (GetFormatFileOfPath())
            {
                //запись данных в csv
                case "csv":
                    ser.CSVWrite(model, PathToFile);
                    break;
                //запись данных в json
                case "json":
                    ser.JSONWrite(model, PathToFile);
                    break;
                //запись данных в xml
                case "xml":
                    ser.XMLWrite(model, PathToFile);
                    break;
                //запись данных в yaml
                case "yaml":
                    ser.YAMLWrite(model, PathToFile);
                    break;
            }

        }

        /// <summary>
        /// Метод получения данных для списка записей стурктуры Programm
        /// </summary>
        /// <returns>
        /// Список записей структуры
        /// </returns>
        public List<Programm> GetListPrograms()
        {
            List<Programm> list = new List<Programm>();
            Programm programm = new Programm();
            int flag = 0;
            bool flagError = false;

            Console.WriteLine("Для заполнения списка модели данными следуйте инструкциям.");
            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить ...");
            Console.ReadKey();
            Console.Clear();
            do
            {
                Console.Write("Введите название программы: ");
                programm.title = Console.ReadLine();
                Console.Write("Введите версию программы: ");
                programm.versoin = Console.ReadLine();
                Console.Write("Введите дату релиза программы строкой по шаблону [yyyy.mm.dd] программы: ");
                string dateTemplate = Console.ReadLine();
                //проверка на правильность ввода даты
                if (programm.CheckingTheCorrectnessOfTheDate(dateTemplate))
                {
                    flagError = true;
                    break;
                }
                programm.dataReilese = dateTemplate;
                list.Add(programm);
                Console.Write("Запись создана!\nХотите продолжить ввод?\nДа - 1..9\nНет - 0\nВведите цифру в соответствии с выбранным вариантом: ");
                flag = int.Parse(Console.ReadLine());
            } while (flag != 0);

            if (flagError)
            {
                return new List<Programm>();
            }

            return list;
        }

        /// <summary>
        /// Метод получения данных для списка записей стурктуры Place
        /// </summary>
        /// <returns>
        /// Список записей структуры
        /// </returns>
        public List<Place> GetListPlaces()
        {
            List<Place> list = new List<Place>();
            Place place = new Place();
            int flag = 0;

            Console.WriteLine("Для заполнения списка модели данными следуйте инструкциям.");
            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить ...");
            Console.ReadKey();
            Console.Clear();
            do
            {
                Console.Write("Введите название страны: ");
                place.country = Console.ReadLine();
                Console.Write("Введите название города: ");
                place.country = Console.ReadLine();
                Console.Write("Введите название улицы: ");
                place.country = Console.ReadLine();
                Console.Write("Введите номер дома: ");
                place.country = Console.ReadLine();
                list.Add(place);
                Console.WriteLine("Запись создана!\nХотите продолжить ввод?\nДа - 1..9\nНет - 0\nВведите цифру в соответствии с выбранным вариантом: ");
                flag = int.Parse(Console.ReadLine());
            } while (Convert.ToBoolean(flag));

            return list;
        }

        /// <summary>
        /// Метод возвращающий список тестовых данных модели "Место"
        /// </summary>
        /// <returns>
        /// список тестовых данных модели "Место"
        /// </returns>
        public List<Place> ListPlacesForTest()
        {
            List<Place> list = new List<Place>();

            list.Add(new Place()
            {
                country = "Japan",
                city = "Tokio",
                street = "Hayao Miyazaki",
                house = "19A"
            });
            list.Add(new Place()
            {
                country = "Russia",
                city = "Moskow",
                street = "Lenins",
                house = "20b/2"
            });
            list.Add(new Place()
            {
                country = "Germany",
                city = "Luxembourg",
                street = "Arlon",
                house = "7g"
            });

            return list;
        }

        /// <summary>
        /// Метод возвращающий список тестовых данных модели "Программа"
        /// </summary>
        /// <returns>
        /// список тестовых данных модели "Программа"
        /// </returns>
        public List<Programm> ListProgrammForTest()
        {
            List<Programm> list = new List<Programm>();

            list.Add(new Programm()
            {
                title = "Word",
                versoin = "1.0",
                dataReilese = "2010.01.01"
            });
            list.Add(new Programm()
            {
                title = "Exel",
                versoin = "2.05",
                dataReilese = "2013.04.01"
            });
            list.Add(new Programm()
            {
                title = "Power Point",
                versoin = "7.103",
                dataReilese = "2016.07.15"
            });

            return list;
        }

        /// <summary>
        /// Метод работы с полученным списком: сортировки и поиска данных
        /// </summary>
        /// <param name="program">список модели "Programm"</param>
        void SorstAndSerchGetsModel(List<Programm> model)
        {
            Console.Write("Выберите действие с полученным списком:\n1.Показать\n2.Отсортировать и вывести\n3.Поиск по тексту\n4.Выход (Список полученных данных всё равно будет выведен)\nИндекс вашего выбора: ");
            try
            {
                int varios = int.Parse(Console.ReadLine());
                switch (varios)
                {
                    case 1:
                        FullPrint(model);
                        break;
                    case 2:
                        SortList(model);
                        break;
                    case 3:
                        SearchList(model);
                        break;
                    case 4:
                        FullPrint(model);
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Выбранного варианта нет!");
                        SorstAndSerchGetsModel(model);
                        break;
                }
                Console.WriteLine("Нажимте любую клавигшу для продолжения...");
                Console.ReadKey();
                Console.Clear();
                SorstAndSerchGetsModel(model);
            }
            catch
            {
                Console.WriteLine("Вы ввели не вернуй символ! повторите ввод!");
                Console.WriteLine("Нажимте любую клавигшу для продолжения...");
                Console.ReadKey();
                Console.Clear();
                SorstAndSerchGetsModel(model);
            }
        }

        /// <summary>
        /// Метод для полноценного вывода списка "Programm"
        /// </summary>
        /// <param name="model">список тип "Programm"</param>
        void FullPrint(List<Programm> model)
        {
            Console.WriteLine("Назвение\tВерсия\tДата релиза:");
            foreach (var item in model)
            {
                Console.WriteLine(item.PrintData());
            }
        }

        /// <summary>
        /// Сортировка списка тип "Programm" по выбранным параметрам
        /// </summary>
        /// <param name="model">список типа "Programm"</param>
        void SortList(List<Programm> model)
        {
            Console.Write("Выберите метод сортировки:\n1.По названию\n2.По версии\n3.По дате релиза\n4.Выход\nИндекс вашего выбора: ");
            try
            {
                int varios = int.Parse(Console.ReadLine());
                switch (varios)
                {
                    case 1:
                        model = model.OrderBy(mod => mod.title).ToList();
                        break;
                    case 2:
                        model = model.OrderBy(mod => mod.versoin).ToList();
                        break;
                    case 3:
                        model = model.OrderBy(mod => mod.dataReilese).ToList();
                        break;
                    case 4:
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Выбранного варианта нет!");
                        SortList(model);
                        break;
                }

            }
            catch
            {
                Console.WriteLine("Вы ввели не вернуй символ! повторите ввод!");
                Console.WriteLine("Нажимте любую клавигшу для продолжения...");
                Console.ReadKey();
                Console.Clear();
                SortList(model);
            }

            FullPrint(model);
        }

        /// <summary>
        /// Метод поиска по списку типа "Programm"
        /// </summary>
        /// <param name="model">список типа "Programm"</param>
        void SearchList(List<Programm> model)
        {
            Console.Write("Введите часть фразы/текста/строки которые хотите найти (строчными буквами, иначе результат будет пустой): ");
            string str = Console.ReadLine();
            model = model.Where(mod => mod.PrintData().Replace("\t", "").ToString().ToLower().IndexOf(str) != -1).ToList();
            FullPrint(model);
        }

        /// <summary>
        /// Метод работы с полученным списком: сортировки и поиска данных
        /// </summary>
        /// <param name="program">список модели "Programm"</param>
        void SorstAndSerchGetsModel(List<Place> model)
        {
            Console.Write("Выберите действие с полученным списком:\n1.Показать\n2.Отсортировать и вывести\n3.Поиск по тексту\n4.Выход (Список полученных данных всё равно будет выведен)\nИндекс вашего выбора: ");
            try
            {
                int varios = int.Parse(Console.ReadLine());
                switch (varios)
                {
                    case 1:
                        FullPrint(model);
                        break;
                    case 2:
                        SortList(model);
                        break;
                    case 3:
                        SearchList(model);
                        break;
                    case 4:
                        FullPrint(model);
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Выбранного варианта нет!");
                        SorstAndSerchGetsModel(model);
                        break;
                }
                Console.WriteLine("Нажимте любую клавигшу для продолжения...");
                Console.ReadKey();
                Console.Clear();
                SorstAndSerchGetsModel(model);
            }
            catch
            {
                Console.WriteLine("Вы ввели не вернуй символ! повторите ввод!");
                Console.WriteLine("Нажимте любую клавигшу для продолжения...");
                Console.ReadKey();
                Console.Clear();
                SorstAndSerchGetsModel(model);
            }
        }

        /// <summary>
        /// Метод для полноценного вывода списка "Place"
        /// </summary>
        /// <param name="model">список тип "Place"</param>
        void FullPrint(List<Place> model)
        {
            Console.WriteLine("Страна\tГород\tУлица\tДом:");
            foreach (var item in model)
            {
                Console.WriteLine(item.PrintData());
            }
        }

        /// <summary>
        /// Сортировка списка тип "Place" по выбранным параметрам
        /// </summary>
        /// <param name="model">список типа "Place"</param>
        void SortList(List<Place> model)
        {
            Console.Write("Выберите метод сортировки:\n1.По стране\n2.По городу\n3.По улице\n4.По дому\n5.Выход\nИндекс вашего выбора: ");
            try
            {
                int varios = int.Parse(Console.ReadLine());
                switch (varios)
                {
                    case 1:
                        model = model.OrderBy(mod => mod.country).ToList();
                        break;
                    case 2:
                        model = model.OrderBy(mod => mod.city).ToList();
                        break;
                    case 3:
                        model = model.OrderBy(mod => mod.street).ToList();
                        break;
                    case 4:
                        model = model.OrderBy(mod => mod.house).ToList();
                        break;
                    case 5:
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Выбранного варианта нет!");
                        SortList(model);
                        break;
                }

            }
            catch
            {
                Console.WriteLine("Вы ввели не вернуй символ! повторите ввод!");
                Console.WriteLine("Нажимте любую клавигшу для продолжения...");
                Console.ReadKey();
                Console.Clear();
                SortList(model);
            }

            FullPrint(model);
        }

        /// <summary>
        /// Метод поиска по списку типа "Place"
        /// </summary>
        /// <param name="model">список типа "Place"</param>
        void SearchList(List<Place> model)
        {
            Console.Write("Введите часть фразы/текста/строки которые хотите найти (строчными буквами, иначе результат будет пустой): ");
            string str = Console.ReadLine();
            model = model.Where(mod => mod.PrintData().Replace("\t", "").ToString().ToLower().IndexOf(str) != -1).ToList();
            FullPrint(model);
        }

        /// <summary>
        /// Метод для отображения для пользователя что такое модель и какие есть в данной программе
        /// </summary>
        public void viewModelsForUsers()
        {
            Console.WriteLine("\nМодель - это тип записи данных о каком-то объекте\n");
            Console.WriteLine("В данном проекте реализованы 2 модели: \"Место\" и \"Программа\"");
            Console.WriteLine("\n\t==\t==\t==\t==\t==\t==\n");
            Console.Write("\t\t");
            Console.WriteLine("Модель \"Программа\"\nСостоит из:\n\t1. Название программы\n\t2. Версии\n\t3. Даты рализа - даты публикции программы, которая записывается по шаблону [yyyy.mm.dd],\t\n где \"y\" - цифра года, \"m\" - цифра месяца, \"d\" - цифра дня\n");
            Console.WriteLine("\n\t==\t==\t==\t==\t==\t==\n");
            Console.Write("\t\t");
            Console.WriteLine("Модель \"Место\"\nСостоит из:\n\t1. Названия страны\n\t2. Названия города\n\t3. Названия улицы\n\t4. Названия дома\n");
            Console.WriteLine("\n\t==\t==\t==\t==\t==\t==\n");
            Console.Write("Нажимте любую клавишу для продолжения...");
            Console.ReadKey();
            Console.Clear();
            StartProgramClass.Main();
        }
    }
}
