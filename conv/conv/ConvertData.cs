using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conv
{
    struct Place
    {
        public string country;
        public string city;
        public string street;
        public string house;
    }

    struct Programm
    {
        public string title;
        public string versoin;
        public string dataReilese;
    }

    internal class ConvertData
    {
        //путь чтения
        string pathRead;
        //путь записи
        string pathWrite;
        //переменная для обработки ошибок
        bool flag;

        //списки структур
        List<Place> places;
        List<Programm> programms;


        //получение пути считывания
        public string PathRead
        {
            set
            {
                if (CheckFormats(value) && CheckingForTheExistenceOfAFile(value))
                {
                    pathRead = value;
                    flag = true;
                }
                else
                {
                    Console.WriteLine("Файл не существует, либо путь к файлу указан не верно!");
                    pathRead = "";
                }
            }
        }

        //получение пути записи
        public string PathWrite
        {
            set
            {
                if (CheckFormats(value))
                {
                    pathRead = value;
                    flag = true;
                }
            }
        }

        /// <summary>
        /// проверка поступившей строки с путём к файлу на формат самого файла
        /// </summary>
        /// <param name="str">строка</param>
        /// <returns>
        /// true - верный формат
        /// false - фармат не верный (его нет)
        /// </returns>
        bool CheckFormats(string str)
        {
            if (str.Length < 4)
            {
                return false;
            }
            return pathRead.IndexOf(".json") != -1
                || pathRead.IndexOf(".xml") != -1
                || pathRead.IndexOf(".yaml") != -1
                || pathRead.IndexOf(".csv") != -1;
        }

        /// <summary>
        /// проверка на существование файла по заданному пути
        /// </summary>
        /// <param name="str">путь</param>
        /// <returns>
        /// true - файл существует
        /// false - файл не существует
        /// </returns>
        bool CheckingForTheExistenceOfAFile(string str)
        {
            return File.Exists(str);
        }


        /// <summary>
        /// Полчение расширения файла без точки
        /// </summary>
        /// <param name="str">путь к файлу (полный или относительный) с названием и расширением файла</param>
        /// <returns>
        /// строка - расширение файла без точки
        /// </returns>
        string GetFormat(string str)
        {
            string revStr = str.ToCharArray().Reverse().ToString();
            return revStr.Substring(0, revStr.IndexOf('.'));
        }

        public void ReadFile()
        {
            if (flag)
            {
                using (StreamReader file = new StreamReader(pathRead))
                {

                }
            }
            else
                return;
        }

        public void WriteFile()
        {
            if (flag)
            {
                using (StreamWriter file = new StreamWriter(pathRead, false))
                {

                }
            }
            else
                return;
        }


    }
}
