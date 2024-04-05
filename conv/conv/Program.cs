namespace conv
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать!\nВыберите вариант действия (введите номер в виде числа):\n1.Считать модель\n2.Записать модель\n3.Выйти");
            Console.Write("Ваш выбор: ");
            int variantDo = int.Parse(Console.ReadLine());

            ConvertData conv = new ConvertData();

            Console.Clear();
            switch (variantDo)
            {
                case 1:
                    Console.WriteLine("Вы выбрали сичтывание модели  с файла");
                    Console.WriteLine("Правила записи пути к файлу:");
                    Console.WriteLine("1.Вводите путь (полный или относительный) файлу без кавычек и через 2 слеша разделения пути\n2. У файла должно быть чётко указано расширение!\n3.При введения просто названия файла (с расширением), файл создастся по стандартному относительному пути");
                    Console.Write("Данные файла:\t");
                    conv.PathRead = Console.ReadLine();
                    conv.ReadFile();
                    break;
                case 2:
                    Console.WriteLine("Вы выбрали запись модели в файл\n");
                    Console.WriteLine("Правила записи пути к файлу:");
                    Console.WriteLine("1.Вводите путь (полный или относительный) файлу без кавычек и через 2 слеша разделения пути\n2. У файла должно быть чётко указано расширение!\n3.При введения просто названия файла (с расширением), файл создастся по стандартному относительному пути");
                    Console.Write("\nДанные файла:\t");
                    conv.PathWrite = Console.ReadLine();
                    conv.WriteFile();
                    break;
                default:
                    Console.WriteLine("До свидания!");
                    return;
            }
        }
    }
}