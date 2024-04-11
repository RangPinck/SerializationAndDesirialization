﻿using Microsoft.VisualBasic;
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
                        //метод сортировки
                        break;
                    case 2:
                        List<Place> place = ReadModelFromFile(new List<Place>());
                        //метод сортировки
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
    }
}
