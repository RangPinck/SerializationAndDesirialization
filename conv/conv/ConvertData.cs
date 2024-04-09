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
                        List<Program> programs = new List<Program>();
                        ReadModelFromFile<Program>(programs);
                        break;
                    case 2:
                        List<Place> places = new List<Place>();
                        ReadModelFromFile<Place>(places);
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
        void ReadModelFromFile<T>(List<T> model)
        {
            SerializationAndDeserialization ser = new SerializationAndDeserialization();
            switch (GetFormatFileOfPath())
            {
                //чтение данных с csv
                case "csv":
                    ser.CSVRead(model);
                    break;
                //чтение данных с json
                case "json":
                    ser.JSONRead(model);
                    break;
                //чтение данных с xml
                case "xml":
                    ser.XMLRead(model);
                    break;
                //чтение данных с yaml
                case "yaml":
                    ser.YAMLRead(model);
                    break;
            }

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
                        List<Program> programs = new List<Program>(); //метод для создания списка 
                        ReadModelFromFile(programs);
                        break;
                    case 2:
                        List<Place> places = new List<Place>(); //метод для создания списка 
                        ReadModelFromFile(places);
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
    }
}
