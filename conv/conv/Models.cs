using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conv
{
    /// <summary>
    /// Место: страна, город, улица, дом
    /// </summary>
    struct Place
    {
        public string country { set; get; }
        public string city { set; get; }
        public string street { set; get; }
        public string house { set; get; }

        /// <summary>
        /// метод преобразования данных в строку для записи в файл/вывода
        /// </summary>
        public void PrintData()
        {
            Console.WriteLine( $"{country} {city} {street} {house}");
        }
    }

    /// <summary>
    /// Программа: название, версия, дата релиза
    /// </summary>
    struct Programm
    {
        public string title { set; get; }
        public string versoin { set; get; }
        public string dataReilese { set; get; }

        /// <summary>
        /// метод преобразования данных в строку для записи в файл/вывода
        /// </summary>
        public void PrintData()
        {
            Console.WriteLine($"{title} {versoin} {dataReilese}");
        }
    }

     
}
