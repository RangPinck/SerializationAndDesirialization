namespace conv
{
    internal class StartProgramClass
    {
       public static void Main()
        {
            try
            {
                //приветствие
                Console.WriteLine("Добро пожаловать!\nДання программа поможет вам считать из записать данные в соответсвии с выбранной моделью.\nДля корректной работы проаграммы просим следовать шагам рекомендованным самой программой!\n");
                Console.Write("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();

                //выбор действия: запись/печать и запсиь пути к файлу
                Console.WriteLine("\nВыберите вариант действия (введите номер в виде числа):\n1.Считать модель\n2.Записать модель\n3.Показать модели\n4..9.Выйти");
                Console.Write("Ваш выбор: ");
                int variantDo = int.Parse(Console.ReadLine());

                ConvertData conv = new ConvertData();

                Console.Clear();
                switch (variantDo)
                {
                    case 1:
                        Console.WriteLine("Вы выбрали сичтывание модели  с файла");
                        Console.WriteLine("Правила записи пути к файлу:");
                        Console.WriteLine("1.Вводите путь (полный или относительный) файлу без кавычек и через 2 слеша разделения пути\n2.У файла должно быть чётко указано расширение!\n3.При введения просто названия файла (с расширением), файл создастся по стандартному относительному пути");
                        Console.Write("Данные файла:\t");
                        conv.PathToFile = Console.ReadLine();
                        conv.ReadFile();
                        break;
                    case 2:
                        Console.WriteLine("Вы выбрали запись модели в файл\n");
                        Console.WriteLine("Правила записи пути к файлу:");
                        Console.WriteLine("1.Вводите путь (полный или относительный) файлу без кавычек и через 2 слеша разделения пути\n2.У файла должно быть чётко указано расширение!\n3.При введения просто названия файла (с расширением), файл создастся по стандартному относительному пути");
                        Console.Write("\nДанные файла:\t");
                        conv.PathToFile = Console.ReadLine();
                        conv.WriteFile();
                        break;

                    case 3:
                        Console.WriteLine("Модели\n");
                        conv.viewModelsForUsers();
                        break;

                    default:
                        Console.WriteLine("До свидания!");
                        return;
                }
            }
            catch
            {
                Console.WriteLine("Вы ввели не вернуй символ! повторите ввод!");
                Console.Write("Нажимте любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
                Main();
            }
        }
    }
}