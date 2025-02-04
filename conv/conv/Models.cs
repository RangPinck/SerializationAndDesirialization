﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core.Tokens;


namespace conv
{
    /// <summary>
    /// Место: страна, город, улица, дом
    /// </summary>
    public class Place
    {
        public string country { set; get; }
        public string city { set; get; }
        public string street { set; get; }
        public string house { set; get; }

        /// <summary>
        /// метод преобразования данных в строку для вывода/сортировки
        /// </summary>
        /// <returns>
        /// cтрока данных
        /// </returns>
        public string PrintData()
        {
            return $"{country}\t{city}\t{street}\t{house}";
        }
    }

    /// <summary>
    /// Программа: название, версия, дата релиза
    /// </summary>
    public class Programm
    {
        public string title { set; get; }
        public string versoin { set; get; }
        public string dataReilese{set; get; }

        /// <summary>
        /// метод преобразования данных в строку для вывода/сортировки
        /// </summary>
        /// <returns>
        /// cтрока данных
        /// </returns>
        public string PrintData()
        {
            return $"{title}\t{versoin}\t{dataReilese}";
        }

        /// <summary>
        /// проверка на правильность ввода даты
        /// </summary>
        /// <param name="dateform">строка, которая должна содержать правильный вид записи даты</param>
        /// <returns>
        /// true - дата введена не правильно
        /// false - дата введена правильно
        /// </returns>
        public bool CheckingTheCorrectnessOfTheDate(string dateform)
        {
            List<string> list = new List<string>();

            try
            {
                foreach (string item in dateform.Split('.'))
                {
                    list.Add(item);
                }

                DateTime data = new DateTime(int.Parse(list[0]), int.Parse(list[1]), int.Parse(list[2]));

                return false;
            }
            catch
            {
                Console.WriteLine("Не верно записана дата!");
                return true;
            }
        }
    }
}
