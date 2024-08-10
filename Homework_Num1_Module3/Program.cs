using System;
using ClassLibrary;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        do
        {
            try
            {
                List<Organism> organismsList = new List<Organism>();

                // Получение, парсинг и вывод JSON-данных от пользователя
                Methods.ProcessUserInput(ref organismsList);

                bool exit = false;
                do
                {
                    // Отображение меню и получение выбора пользователя
                    string choice = Methods.DisplayMenuAndMakeChoice().ToString();

                    // Обработка выбора пользователя
                    switch (choice)
                    {
                        case "1":
                            // Получение новых JSON-данных и их парсинг
                            Methods.ProcessUserInput(ref organismsList);
                            break;
                        case "2":
                            // Фильтрация данных
                            organismsList = Methods.FilterOrganisms(organismsList);
                            break;
                        case "3":
                            // Сортировка данных
                            organismsList = Methods.SortOrganisms(organismsList);
                            break;
                        case "4":
                            // Выход из вложенного цикла
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Неверный выбор. Пожалуйста, повторите.");
                            break;
                    }
                } while (!exit);
            }
            catch (ArgumentNullException ex)
            {
                // Обработка и вывод ошибок
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                // Обработка и вывод ошибок
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Обработка и вывод ошибок
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }

            // Запрос пользователя о продолжении или завершении программы
            Console.WriteLine("Если хотите начать заново, нажмите Enter, в противном случае - Escape\n");

        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
    }
}
