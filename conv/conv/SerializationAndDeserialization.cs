﻿using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conv
{
    //сделать проверку на запись даты

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
            List<T> result = new List<T>();

            Console.WriteLine("Данные считаны!");
            return result;
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
            List<T> result = new List<T>();

            Console.WriteLine("Данные считаны!");
            return result;
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
            List<T> result = new List<T>();

            Console.WriteLine("Данные считаны!");
            return result;
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
            Console.WriteLine("Данные записаны!");
        }
    }
}
