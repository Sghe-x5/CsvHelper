using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary
{
    /// <summary>
    /// Класс для парсинга JSON-данных и работы с сохранением данных в формате JSON.
    /// </summary>
    public class JsonParser
    {
        /// <summary>
        /// Парсит JSON-данные и возвращает список объектов Organism.
        /// </summary>
        /// <param name="jsonData">Строка JSON-данных.</param>
        /// <returns>Список объектов Organism.</returns>
        public static List<Organism> ParseJsonData(string jsonData)
        {
            List<Organism> organismsList = new List<Organism>();

            if (jsonData == null)
            {
                Console.WriteLine("Организмов нет");
                return organismsList;
            }
            try
            {
                string pattern = "\"organism_id\":\\s*(\\d+),\\s*\"scientific_name\":\\s*\"([^\"]+)\",\\s*\"common_name" +
                                 "\":\\s*\"([^\"]+)\",\\s*\"kingdom\":\\s*\"([^\"]+)\",\\s*\"habitat\":\\s*\"([^\"]+)\",\\s*" +
                                 "\"population\":\\s*(\\d+),\\s*\"characteristics\":\\s*\\[\\s*((?:\"[^\"]+\",\\s*)*\"[^\"]+\"" +
                                 ")?\\s*\\],\\s*\"researchers\":\\s*\\[\\s*((?:\"[^\"]+\",\\s*)*\"[^\"]+\")?\\s*\\]";

                MatchCollection matches = Regex.Matches(jsonData, pattern);
                foreach (Match m in matches)
                {
                    organismsList.Add(new Organism(
                        organismId: int.Parse(m.Groups[1].Value),
                        scientificName: m.Groups[2].Value,
                        commonName: m.Groups[3].Value,
                        kingdom: m.Groups[4].Value,
                        habitat: m.Groups[5].Value,
                        population: int.Parse(m.Groups[6].Value),
                        characteristics: m.Groups[7].Value.Split(',').Select(c => c.Trim('\"', ' ', '\r', '\n')).ToList(),
                        researchers: m.Groups[8].Value.Split(',').Select(r => r.Trim('\"', ' ', '\r', '\n')).ToList()
                    ));
                }


                return organismsList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке данных JSON: {ex.Message}");
                throw;
            }

            return organismsList;
        }

        /// <summary>
        /// Читает значения массива из строки с разделителями и возвращает массив строк.
        /// </summary>
        /// <param name="arrayMatch">Строка с данными массива.</param>
        /// <returns>Массив строк.</returns>
        private static string[] ReadArrayValues(string arrayMatch)
        {
            if (string.IsNullOrWhiteSpace(arrayMatch))
            {
                return new string[0];
            }

            string[] values = arrayMatch.Split(new[] { "\", \"" }, StringSplitOptions.None);
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Replace("\"", "");
            }

            return values;
        }

        /// <summary>
        /// Записывает данные в формате JSON в указанный файл.
        /// </summary>
        /// <param name="organismsList">Список объектов Organism.</param>
        /// <param name="filePath">Путь к файлу для записи данных.</param>
        public static void WriteJson(List<Organism> organismsList, string filePath)
        {
            try
            {
                string jsonData = "[";

                foreach (Organism organism in organismsList)
                {
                    string characteristics = string.Join("\", \"", organism.Characteristics.Select(c => c.ToString()));
                    string researchers = string.Join("\", \"", organism.Researchers.Select(r => r.ToString()));

                    jsonData += $@"
  {{
    ""organism_id"": {organism.OrganismId},
    ""scientific_name"": ""{organism.ScientificName}"",
    ""common_name"": ""{organism.CommonName}"",
    ""kingdom"": ""{organism.Kingdom}"",
    ""habitat"": ""{organism.Habitat}"",
    ""population"": {organism.Population},
    ""characteristics"": [""{characteristics}""],
    ""researchers"": [""{researchers}""]
  }}";

                    if (organism != organismsList.Last())  // Проверка, не последний ли элемент в списке
                        jsonData += ",";  // Добавление запятой, если это не последний элемент
                }

                jsonData += "\n]";

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    streamWriter.Write(jsonData);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Отказано в доступе при записи файла JSON.");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при сохранении данных в файл JSON.");
                throw;
            }
        }

        /// <summary>
        /// Сохраняет JSON-данные в файл.
        /// </summary>
        /// <param name="jsonData">Строка с JSON-данными для сохранения.</param>
        /// <param name="filePath">Путь к файлу, в который будут сохранены данные.</param>
        public static void SaveJson(string jsonData, string filePath)
        {
            try
            {
                filePath += ".json";
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    streamWriter.Write(jsonData);
                }
                Console.WriteLine("\nДанные успешно сохранены.");
            }
            catch (Exception)
            {
                Console.WriteLine("\nОшибка при сохранении данных.");
            }
        }
    }
}
