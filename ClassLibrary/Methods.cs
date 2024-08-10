using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ClassLibrary
{
    public class Methods
    {
        /// <summary>
        /// Отображает меню и возвращает выбранный пользователем вариант.
        /// </summary>
        /// <returns>Выбранный вариант.</returns>
        public static int DisplayMenuAndMakeChoice()
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Заново ввести данные.");
            Console.WriteLine("2. Отфильтровать данные по одному из полей.");
            Console.WriteLine("3. Отсортировать данные по одному из полей.");
            Console.WriteLine("4. Выход.");
            Console.WriteLine("\nВведите число от 1 до 4:");
            int k;
            while (!int.TryParse(Console.ReadLine(), out k) || k <= 0 || k >= 5)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите число от 1 до 4:");
            }
            return k;
        }

        /// <summary>
        /// Получает данные об организмах от пользователя или из файла.
        /// </summary>
        /// <returns>Список объектов Organism.</returns>
        public static string GetData()
        {
            List<Organism> list = new List<Organism>();

            Console.WriteLine("Выберите способ ввода данных:");
            Console.WriteLine("1. Ввести данные вручную");
            Console.WriteLine("2. Предоставить путь к файлу для чтения данных");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice >= 3)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите 1 или 2.");
            }

            if (choice == 1)
            {
                // Ввод данных вручную
                Console.WriteLine("Для завершения ввода нажмите CNTRL + D");
                return GetStdInput();
            }
            else
            {
                // Ввод пути к файлу
                string filePath = GetPath();

                // Чтение данных из файла
                return GetFileInput(filePath);
            }
        }

        /// <summary>
        /// Получает стандартный ввод пользователя.
        /// </summary>
        /// <returns>Строка введенных данных.</returns>
        public static string GetStdInput()
        {
            Stream inputStream = Console.OpenStandardInput();
            var sr = new StreamReader(inputStream);
            var result = "";
            while (sr.ReadLine() is { } line)
            {
                result += line.Trim();
                if (!string.IsNullOrEmpty(line))
                {
                    result += " ";
                }
            }
            inputStream.Dispose();
            sr.Dispose();
            return result.Trim();
        }

        /// <summary>
        /// Получает данные из файла по указанному пути.
        /// </summary>
        /// <param name="fileName">Путь к файлу.</param>
        /// <returns>Содержимое файла.</returns>
        public static string GetFileInput(string fileName)
        {
            if (File.Exists(fileName) && fileName != null)
            {
                FileStream fs = File.OpenRead(fileName);
                StreamReader sr = new StreamReader(fs);
                string fileContent = sr.ReadToEnd();
                sr.Close();
                fs.Close();
                return fileContent;
            }
            Console.WriteLine("Файла {0} не существует", fileName);
            return "";
        }

        /// <summary>
        /// Получает путь к файлу от пользователя.
        /// </summary>
        /// <returns>Путь к файлу.</returns>
        public static string GetPath()
        {
            Console.WriteLine("Введите путь к файлу:\n");
            string path = Console.ReadLine() ?? "";
            if (File.Exists(path) && path.Length > 5 && path.Substring(path.Length - 5, 5) == ".json")
            {
                return path;
            }
            while (!File.Exists(path))
            {
                Console.WriteLine($"Файла {path} не существует, введите путь к существующему файлу:\n");
                path = Console.ReadLine() ?? "";
            }
            while (path.Length <= 5 || path.Substring(path.Length - 5, 5) != ".json")
            {
                Console.WriteLine($"Формат файла {path} не соответствует условию, введите путь к верному файлу:\n");
                path = Console.ReadLine() ?? "";
            }
            return path;
        }

        /// <summary>
        /// Фильтрует данные по выбранному полю и значению.
        /// </summary>
        /// <param name="list">Исходный список Organism.</param>
        /// <param name="val">Значение для фильтрации.</param>
        /// <param name="choice">Выбранное поле для фильтрации.</param>
        /// <returns>Список отфильтрованных объектов Organism.</returns>
        public static List<Organism> FilterData(List<Organism> list, string val, Choice choice)
        {
            switch (choice)
            {
                case Choice.OrganismId:
                    return list.FindAll(e => e.OrganismId.ToString().Contains(val));
                case Choice.ScientificName:
                    return list.FindAll(e => e.ScientificName.Contains(val));
                case Choice.CommonName:
                    return list.FindAll(e => e.CommonName.Contains(val));
                case Choice.Kingdom:
                    return list.FindAll(e => e.Kingdom.Contains(val));
                case Choice.Habitat:
                    return list.FindAll(e => e.Habitat.Contains(val));
                case Choice.Population:
                    return list.FindAll(e => e.Population.ToString().Contains(val));
                case Choice.Characteristics:
                    return list.FindAll(e => e.Characteristics.Contains(val));
                case Choice.Researchers:
                    return list.FindAll(e => e.Researchers.Contains(val));
                default:
                    return list;
            }
        }

        /// <summary>
        /// Отображает текущий список объектов Organism и предоставляет пользователю выбор вывести его или нет.
        /// </summary>
        /// <param name="organisms">Список объектов Organism для отображения.</param>
        public static void DisplayOrganisms(List<Organism> organisms)
        {

            Console.WriteLine("Вывести текущий список объектов Organism?");
            Console.WriteLine("1. Вывести");
            Console.WriteLine("2. Вывод не нужен");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice >= 3)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите 1 или 2.");
            }
            if (choice == 1)
            {
                if (organisms.Count == 0)
                {
                    Console.WriteLine("\nСписок объектов Organism пустой.");
                }
                else
                {
                    Console.WriteLine("\nСписок объектов Organism:\n");

                    foreach (Organism organism in organisms)
                    {
                        Console.WriteLine($"OrganismId: {organism.OrganismId}");
                        Console.WriteLine($"ScientificName: {organism.ScientificName}");
                        Console.WriteLine($"CommonName: {organism.CommonName}");
                        Console.WriteLine($"Kingdom: {organism.Kingdom}");
                        Console.WriteLine($"Habitat: {organism.Habitat}");
                        Console.WriteLine($"Population: {organism.Population}");

                        Console.Write("Characteristics: ");
                        Console.WriteLine(string.Join(", ", organism.Characteristics.Select(c => c)));

                        Console.Write("Researchers: ");
                        Console.WriteLine(string.Join(", ", organism.Researchers.Select(r => r)));

                        Console.WriteLine(); 
                    }
                }
            }
        }

        /// <summary>
        /// Перечисление, представляющее поля для фильтрации данных.
        /// </summary>
        public enum Choice
        {
            OrganismId = 1,
            ScientificName,
            CommonName,
            Kingdom,
            Habitat,
            Population,
            Characteristics,
            Researchers
        }

        /// <summary>
        /// Фильтрует данные по выбранному полю и значению.
        /// </summary>
        /// <param name="list">Исходный список Organism.</param>
        /// <returns>Список отфильтрованных объектов Organism.</returns>
        public static List<Organism> FilterOrganisms(List<Organism> organismsList)
        {
            Console.WriteLine("\nВыберите поле для фильтрации:");
            Console.WriteLine("1. OrganismId");
            Console.WriteLine("2. ScientificName");
            Console.WriteLine("3. CommonName");
            Console.WriteLine("4. Kingdom");
            Console.WriteLine("5. Habitat");
            Console.WriteLine("6. Population");
            Console.WriteLine("7. Characteristics");
            Console.WriteLine("8. Researchers");
            Console.WriteLine("Введите число от 1 до 8.");

            int filterChoice;
            while (!int.TryParse(Console.ReadLine(), out filterChoice) || filterChoice <= 0 || filterChoice >= 9)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите число от 1 до 8.");
            }
            bool flag = false;
            while (!flag)
            {
                Console.WriteLine("Введите значение для фильтрации:");
                string filterValue = Console.ReadLine();

                // Преобразование строки в перечисление Methods.Choice
                if (Enum.TryParse(typeof(Methods.Choice), filterChoice.ToString(), out object enumValue))
                {
                    var choiceEnum = (Methods.Choice)enumValue;

                    // Применение фильтрации и отображение результата
                    var filteredList = Methods.FilterData(organismsList, filterValue, choiceEnum);
                    Methods.DisplayOrganisms(filteredList);
                    Methods.SaveData(filteredList);
                    return filteredList;
                    flag = true;
                }
                else
                {
                    Console.WriteLine("Некорректный выбор поля для фильтрации.\n");
                }
            }
            return new List <Organism> ();
        }

        /// <summary>
        /// Сортирует список объектов Organism по выбранному полю.
        /// </summary>
        /// <param name="list">Исходный список Organism.</param>
        /// <param name="num">Номер выбранного поля для сортировки.</param>
        /// <returns>Список отсортированных объектов Organism.</returns>
        public static List<Organism> Sorter(List<Organism> list, int num)
        {
            switch (num)
            {
                case 1:
                    return list.OrderBy(x => x.OrganismId).ToList();
                case 2:
                    return list.OrderBy(x => x.ScientificName).ToList();
                case 3:
                    return list.OrderBy(x => x.CommonName).ToList();
                case 4:
                    return list.OrderBy(x => x.Kingdom).ToList();
                case 5:
                    return list.OrderBy(x => x.Habitat).ToList();
                case 6:
                    return list.OrderBy(x => x.Population).ToList();
                default:
                    return new List<Organism>();
            }
        }

        /// <summary>
        /// Сортирует список объектов Organism.
        /// </summary>
        /// <param name="organismsList">Исходный список Organism.</param>
        /// <returns>Список отсортированных объектов Organism.</returns>
        public static List<Organism> SortOrganisms(List<Organism> organismsList)
        {
            Console.WriteLine("\nВыберите поле для сортировки:");
            Console.WriteLine("1. OrganismId");
            Console.WriteLine("2. ScientificName");
            Console.WriteLine("3. CommonName");
            Console.WriteLine("4. Kingdom");
            Console.WriteLine("5. Habitat");
            Console.WriteLine("6. Population");
            Console.WriteLine("Введите число от 1 до 6:");

            int filterChoice;
            while (!int.TryParse(Console.ReadLine(), out filterChoice) || filterChoice <= 0 || filterChoice >= 7)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите число от 1 до 6:");
            }
            var sorteredList = Methods.Sorter(organismsList, filterChoice);
            Methods.DisplayOrganisms(sorteredList);
            Methods.SaveData(sorteredList);
            return sorteredList;
        }

        /// <summary>
        /// Сохраняет данные в файл, если пользователь выбирает сохранение.
        /// </summary>
        /// <param name="list">Список объектов Organism.</param>
        public static void SaveData(List<Organism> list)
        {
            Console.WriteLine("Нужно сохранить новый список объектов Organism?");
            Console.WriteLine("1. Да");
            Console.WriteLine("2. Нет");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice >= 3)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите 1 или 2.");
            }
            if (choice == 1)
            {
                string saveJsonData = Methods.ConvertOrganismsToJson(list);
                Console.Write("\nВведите имя (!) файла для сохранения:\n");
                string str = Console.ReadLine();
                while (str.Length == 0 || str.Contains("/"))
                {
                    Console.Write("\nНекорректный ввод, введите имя (!) файла для сохранения:\n");
                    str = Console.ReadLine();
                }
                string fileSavePath = str;
                JsonParser.SaveJson(saveJsonData, fileSavePath);
            }
        }

        /// <summary>
        /// Преобразует список объектов Organism в формат JSON.
        /// </summary>
        /// <param name="organismList">Список объектов Organism.</param>
        /// <returns>Строка в формате JSON.</returns>
        public static string ConvertOrganismsToJson(List<Organism> organismList)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[\n");

            for (int i = 0; i < organismList.Count; i++)
            {
                jsonBuilder.Append("  {\n");
                jsonBuilder.AppendFormat("    \"organism_id\": {0},\n", organismList[i].OrganismId);
                jsonBuilder.AppendFormat("    \"scientific_name\": \"{0}\",\n", organismList[i].ScientificName);
                jsonBuilder.AppendFormat("    \"common_name\": \"{0}\",\n", organismList[i].CommonName);
                jsonBuilder.AppendFormat("    \"kingdom\": \"{0}\",\n", organismList[i].Kingdom);
                jsonBuilder.AppendFormat("    \"habitat\": \"{0}\",\n", organismList[i].Habitat);
                jsonBuilder.AppendFormat("    \"population\": {0},\n", organismList[i].Population);
                jsonBuilder.AppendFormat("    \"characteristics\": [\n");

                for (int j = 0; j < organismList[i].Characteristics.Count; j++)
                {
                    jsonBuilder.AppendFormat("      \"{0}\"", organismList[i].Characteristics[j]);
                    if (j < organismList[i].Characteristics.Count - 1)
                        jsonBuilder.Append(",\n");
                }

                jsonBuilder.Append("\n    ],\n");
                jsonBuilder.AppendFormat("    \"researchers\": [\n");

                for (int k = 0; k < organismList[i].Researchers.Count; k++)
                {
                    jsonBuilder.AppendFormat("      \"{0}\"", organismList[i].Researchers[k]);
                    if (k < organismList[i].Researchers.Count - 1)
                        jsonBuilder.Append(",\n");
                }

                jsonBuilder.Append("\n    ]\n");
                jsonBuilder.Append("  }");

                if (i < organismList.Count - 1)
                    jsonBuilder.Append(",\n");
            }

            jsonBuilder.Append("\n]");
            return jsonBuilder.ToString();
        }

        /// <summary>
        /// Обрабатывает ввод пользователя, получает новые данные и отображает их.
        /// </summary>
        /// <param name="organismsList">Список объектов Organism.</param>
        public static void ProcessUserInput(ref List<Organism> organismsList)
        {
            string newJsonData = Methods.GetData();
            JsonParser newJsonParser = new JsonParser();
            organismsList = JsonParser.ParseJsonData(newJsonData);
            Methods.DisplayOrganisms(organismsList);
        }
    }
}