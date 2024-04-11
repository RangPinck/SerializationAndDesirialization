using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using YamlDotNet.Serialization;

namespace conv
{

    class SerializationAndDeserialization
    {
        /// <summary>
        /// Чтение данных с файла формата csv
        /// </summary>
        /// <typeparam name="T">тип данных - тип модели</typeparam>
        /// <param name="model"> список данных соотвествующих модели</param>
        /// <param name="filePath">путь к файлу для записи</param>
        /// <returns>
        /// Список считанных строк в виде списка типа данных переданной модели
        /// </returns>
        public List<T> CSVRead<T>(List<T> model, string filePath)
        {
            using StreamReader file = new StreamReader(filePath);
            using CsvReader csv = new CsvReader(file, CultureInfo.InvariantCulture);
            csv.Read();
            csv.ReadHeader();
            model = csv.GetRecords<T>().ToList();

            Console.WriteLine("Данные считаны!");
            return model;
        }

        /// <summary>
        /// Чтение данных с файла формата yaml
        /// </summary>
        /// <typeparam name="T">тип данных - тип модели</typeparam>
        /// <param name="model"> список данных соотвествующих модели</param>
        /// <param name="filePath">путь к файлу для записи</param>
        /// <returns>
        /// Список считанных строк в виде списка типа данных переданной модели
        /// </returns>
        public List<T> YAMLRead<T>(List<T> model, string filePath)
        {
            string text = File.ReadAllText(filePath);
            var deserializer = new DeserializerBuilder().Build();
            model = deserializer.Deserialize<List<T>>(text);
            Console.WriteLine("Данные считаны!");
            return model;
        }

        /// <summary>
        /// Чтение данных с файла формата xml
        /// </summary>
        /// <typeparam name="T">тип данных - тип модели</typeparam>
        /// <param name="model"> список данных соотвествующих модели</param>
        /// <param name="filePath">путь к файлу для записи</param>
        /// <returns>
        /// Список считанных строк в виде списка типа данных переданной модели
        /// </returns>
        public List<T> XMLRead<T>(List<T> model, string filePath)
        {
            using StreamReader file = new StreamReader(filePath);
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            model = serializer.Deserialize(file) as List<T>;
            Console.WriteLine("Данные считаны!");
            return model;
        }


        /// <summary>
        /// Чтение данных с файла формата json
        /// </summary>
        /// <typeparam name="T">тип данных - тип модели</typeparam>
        /// <param name="model"> список данных соотвествующих модели</param>
        /// <param name="filePath">путь к файлу для записи</param>
        /// <returns>
        /// Список считанных строк в виде списка типа данных переданной модели
        /// </returns>
        public List<T> JSONRead<T>(List<T> model, string filePath)
        {
            string dataJson = "";
            dataJson = File.ReadAllText(filePath);
            if (dataJson.Contains("["))
            {
                model = JsonSerializer.Deserialize<List<T>>(dataJson);
            }
            else
            {
                var strData = JsonSerializer.Deserialize<T>(dataJson);
                model.Add(strData);
            }

            Console.WriteLine("Данные считаны!");
            return model;
        }

        /// <summary>
        /// Запись данных по модели в файл csv
        /// </summary>
        /// <typeparam name="T">тип данных - тип модели</typeparam>
        /// <param name="model"> список данных соотвествующих модели</param>
        /// <param name="filePath">путь к файлу для записи</param>
        public void CSVWrite<T>(List<T> model, string filePath)
        {
            using StreamWriter file = new StreamWriter(filePath, false);
            using CsvWriter csv = new CsvWriter(file, CultureInfo.InvariantCulture);
            csv.WriteHeader<T>();
            csv.NextRecord();

            foreach (var item in model)
            {
                csv.WriteRecord(item);
                csv.NextRecord();
            }

            Console.WriteLine("Данные записаны!");
        }

        /// <summary>
        /// Запись данных по модели в файл yaml
        /// </summary>
        /// <typeparam name="T">тип данных - тип модели</typeparam>
        /// <param name="model"> список данных соотвествующих модели</param>
        /// <param name="filePath">путь к файлу для записи</param>
        public void YAMLWrite<T>(List<T> model, string filePath)
        {
            var yamlser = new SerializerBuilder().Build();
            var yaml = yamlser.Serialize(model);
            File.WriteAllText(filePath, yaml);
            Console.WriteLine("Данные записаны!");
        }

        /// <summary>
        /// Запись данных по модели в файл xml
        /// </summary>
        /// <typeparam name="T">тип данных - тип модели</typeparam>
        /// <param name="model"> список данных соотвествующих модели</param>
        /// <param name="filePath">путь к файлу для записи</param>
        public void XMLWrite<T>(List<T> model, string filePath)
        {
            List<T> result = new List<T>();
            using StreamWriter file = new StreamWriter(filePath);
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            serializer.Serialize(file, model);

            Console.WriteLine("Данные записаны!");
        }

        /// <summary>
        /// Запись данных по модели в файл json
        /// </summary>
        /// <typeparam name="T">тип данных - тип модели</typeparam>
        /// <param name="model"> список данных соотвествующих модели</param>
        /// <param name="filePath">путь к файлу для записи</param>
        public void JSONWrite<T>(List<T> model, string filePath)
        {
            using FileStream file = new FileStream(filePath, FileMode.Create);
            JsonSerializer.Serialize(file, model);
            Console.WriteLine("Данные записаны!");
        }
    }
}
