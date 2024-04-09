using System;
using System.Collections.Generic;
using System.Linq;
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
        public string GetFormatFileOfPath()
        {
            string path = PathToFile;
            path = new string(path.Reverse().ToArray());
            path = path.Substring(0, path.IndexOf('.'));
            return new string(path.Reverse().ToArray());
        }


    }
}
