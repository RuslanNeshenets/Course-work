using System;
using System.Collections.Generic;
using MyLibrary;

namespace Сourse_work
{
    class Custom_Menu
    {
        static List<string> Menu_1()
        {
            List<string> menu = new List<string>();
            menu.Add("=====================================");
            menu.Add("Что вы хотите сделать? (Используйте стрелочки для перемещения по меню)");
            menu.Add("=====================================");
            menu.Add("1 - Показать каталог книг");
            menu.Add("2 - Поиск книги по названию");
            menu.Add("3 - Поиск книги по автору");
            menu.Add("4 - Поиск книги по тематике");
            menu.Add("5 - Взять книгу");
            menu.Add("6 - Вернуть книгу");
            return menu;
        }
        static List<string> Menu_2()
        {
            List<string> menu = new List<string>();
            menu.Add("=====================================");
            menu.Add("(Режим администрации) (Используйте стрелочки для перемещения по меню)");
            menu.Add("=====================================");
            menu.Add("1 - Добавить новую книгу");
            menu.Add("2 - Изменить тематику книги");
            menu.Add("3 - Удалить книгу с заданным названием");
            menu.Add("4 - Удалить книги заданного автора");
            menu.Add("5 - Удалить книги заданной тематики");
            menu.Add("6 - Последние удалённые книги");
            menu.Add("7 - Восстановить книгу");
            menu.Add("8 - Узнать количество заданных книг");
            menu.Add("9 - Увеличить количество заданных книг");
            menu.Add("10 - Уменьшить количество заданных книг");
            menu.Add("11 - Список зарегестрированных читателей");
            menu.Add("12 - Зарегестрировать нового читателя");
            menu.Add("13 - Список книг читателя");
            menu.Add("14 - Аннулировать карточку читателя");
            return menu;
        }
        static void Print_menu(List<string> menu, int Num)
        {
            for (int i = 0; i < menu.Count; i++)
            {
                if (i - 2 == Num)
                    Color_Print_Green(menu[i]);
                else
                    Console.WriteLine(menu[i]);
            }
        }
        static void Color_Print_Green(string str)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(str);
            Console.ResetColor();
        }
        static void Color_Print_Red(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ResetColor();
        }
        static int Menu_navigation(List<string> menu)
        {
            int Num = 1;
            bool Bool = true;
            int count = 0;
            ConsoleKeyInfo key;
            while (Bool)
            {
                key = Console.ReadKey();
                count++;
                if (count == 1)
                    Console.Clear();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            Num--;
                            if (Num < 1)
                                Num = menu.Count - 3;
                            Console.SetCursorPosition(0, 0);
                            Print_menu(menu, Num);
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            Num++;
                            if (Num > menu.Count - 3)
                                Num = 1;
                            Console.SetCursorPosition(0, 0);
                            Print_menu(menu, Num);
                            break;
                        }
                    case ConsoleKey.Enter:
                        {
                            Bool = false;
                            break;
                        }
                    case ConsoleKey.Backspace:
                        {
                            Num = 0;
                            Bool = false;
                            break;
                        }
                    default:
                        {
                            Console.Clear();
                            Print_menu(menu, Num);
                            break;
                        }
                }
            }
            return Num;
        }
        static void Information(Book book)
        {
            Console.Write($"{book.Name,-25}");
            Console.Write($" - Автор: {book.Author,-20}");
            if (book.Subject != null)
                Console.Write($" - Тематика: {book.Subject,-30}");
            Console.WriteLine();
        }
        static void Information(Reader reader)
        {
            Console.WriteLine($"{reader.Surname} {reader.Name} {reader.Patronymic}");
        }
        static void Main(string[] args)
        {

            Storage storage = new Storage();
            Form form = new Form();
            try
            {
                storage.Input_Books();
            }
            catch (Exception ex)
            {
                Color_Print_Red(ex.Message);
            }

            bool administrator_mode = false;
            int counter = 0;
            int num = -1;

            while (true)
            {

                if (num == 0 && counter % 2 == 0)
                {
                    administrator_mode = true;
                    counter++;
                }
                else if (num == 0 && counter % 2 == 1)
                {
                    administrator_mode = false;
                    counter++;
                }
                if (administrator_mode == false)
                {
                    List<string> menu_1 = Menu_1();
                    Print_menu(menu_1, 1);
                    num = Menu_navigation(menu_1);

                    Console.Clear();
                    switch (num)
                    {
                        case 1:
                            {
                                void Border(int name, int author, int subject)
                                {
                                    Console.Write("|-----|");
                                    for (int i = 0; i < name + author + subject + 9; i++)
                                    {
                                        if (i == name + 2 || i == name + author + 5 || i == name + author + subject + 8)
                                            Console.Write("|");
                                        else
                                            Console.Write("-");
                                    }
                                    Console.WriteLine();
                                }
                                List<Book> list = storage.List_books();
                                int size = storage.Get_Size();
                                storage.Sort();

                                Color_Print_Green("Каталог книг:");
                                int max_name_size = 0;
                                int max_author_size = 0;
                                int max_subject_size = 0;
                                for (int i = 0; i < size; i++)
                                {
                                    if (list[i].Name.Length > max_name_size)
                                        max_name_size = list[i].Name.Length;
                                    if (list[i].Author.Length > max_author_size)
                                        max_author_size = list[i].Author.Length;
                                    if (list[i].Subject != null && list[i].Subject.Length > max_subject_size)
                                        max_subject_size = list[i].Subject.Length;
                                }

                                Border(max_name_size, max_author_size, max_subject_size);

                                Console.Write("|Номер");
                                Console.Write("| Название книги");
                                for (int i = 0; i < max_name_size - 13; i++) { Console.Write(" "); }
                                Console.Write("| Автор");
                                for (int i = 0; i < max_author_size - 4; i++) { Console.Write(" "); }
                                Console.Write("| Тематика");
                                for (int i = 0; i < max_subject_size - 7; i++) { Console.Write(" "); }
                                Console.WriteLine("|");

                                Border(max_name_size, max_author_size, max_subject_size);

                                for (int i = 0; i < size; i++)
                                {
                                    if (i < 9)
                                        Console.Write($"| {i + 1}   ");
                                    else if (i < 99)
                                        Console.Write($"| {i + 1}  ");
                                    else
                                        Console.Write($"| {i + 1} ");
                                    Console.Write($"| {list[i].Name}");
                                    for (int j = 0; j < 1 + max_name_size - list[i].Name.Length; j++) {Console.Write(" "); }
                                    Console.Write($"| {list[i].Author}");
                                    for (int j = 0; j < 1 + max_author_size - list[i].Author.Length; j++) { Console.Write(" "); }
                                    Console.Write($"| {list[i].Subject}");
                                    if (list[i].Subject != null)
                                        for (int j = 0; j < 1 + max_subject_size - list[i].Subject.Length; j++) { Console.Write(" "); }
                                    else
                                        for (int j = 0; j < 1 + max_subject_size; j++) { Console.Write(" "); }
                                    Console.WriteLine("|");

                                    Border(max_name_size, max_author_size, max_subject_size);
                                }
                                break;
                            }
                        case 2:
                            {
                                Console.Write("Введите название книги: ");
                                string name = Console.ReadLine();
                                try
                                {
                                    List<Book> list = storage.Search_by_name(name);
                                    if (list.Count == 0)
                                        Color_Print_Red("Книг с данным названием не найдено!");
                                    for (int i = 0; i < list.Count; i++)
                                        Information(list[i]);
                                }
                                catch (ArgumentException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                break;
                            }
                        case 3:
                            {
                                Console.Write("Введите автора книги: ");
                                string author = Console.ReadLine();
                                try
                                {
                                    List<Book> list = storage.Search_by_author(author);
                                    if (list.Count == 0)
                                        Color_Print_Red("Книг с данным автором не найдено!");
                                    for (int i = 0; i < list.Count; i++)
                                        Information(list[i]);
                                }
                                catch (ArgumentException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                break;
                            }
                        case 4:
                            {
                                Console.Write("Введите тематику книги: ");
                                string subject = Console.ReadLine();
                                try
                                {
                                    List<Book> list = storage.Search_by_subject(subject);
                                    if (list.Count == 0)
                                        Color_Print_Red("Книг с данной тематикой не найдено!");
                                    for (int i = 0; i < list.Count; i++)
                                        Information(list[i]);
                                }
                                catch (ArgumentException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                break;
                            }
                        case 5:
                            {
                                Console.Write("Введите название книжки, которую хотите взять: ");
                                try
                                {
                                    List<Book> list = storage.Search_by_name(Console.ReadLine());
                                    if (list.Count > 1)
                                        Color_Print_Red("Уточните название книги");
                                    else if (list.Count == 0)
                                        Color_Print_Red("Книги с таким названием не найдено");
                                    else
                                    {
                                        Book book = list[0];
                                        Information(book);
                                        if (book.Copies > 0)
                                        {
                                            Console.Write("Введите фамилию читателя: ");
                                            string surname = Console.ReadLine();
                                            Console.Write("Введите имя читателя: ");
                                            string name = Console.ReadLine();
                                            Console.Write("Введите отчество читателя: ");
                                            string patronymic = Console.ReadLine();
                                            if (surname != "" && name != "" && patronymic != "")
                                            {
                                                try
                                                {
                                                    Reader reader = form.Reader_search(surname, name, patronymic);
                                                    try
                                                    {
                                                        reader.Take_book(book);
                                                        Color_Print_Green("Книга добавлена в ваш список книг. Приятного чтения!");
                                                    }
                                                    catch (MaximumNumberOfBooksExcaption ex)
                                                    {
                                                        Color_Print_Red(ex.Message);
                                                    }
                                                    catch (FoundMatchException ex)
                                                    {
                                                        Color_Print_Red(ex.Message);
                                                    }
                                                }
                                                catch (ListEmptyException ex)
                                                {
                                                    Color_Print_Red(ex.Message);
                                                }
                                                catch (ReaderNotFoundException ex)
                                                {
                                                    Color_Print_Red(ex.Message);
                                                }
                                            }
                                            else
                                                Color_Print_Red("Некоректные данные!");
                                        }
                                        else
                                        {
                                            Color_Print_Red("Данной книжки нет в наличии!");
                                        }
                                    }
                                }
                                catch (ArgumentException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                break;
                            }
                        case 6:
                            {
                                Console.Write("Введите название книжки, которую хотите отдать: ");
                                try
                                {
                                    List<Book> list = storage.Search_by_name(Console.ReadLine());
                                    if (list.Count > 1)
                                        Color_Print_Red("Уточните название книги");
                                    else if (list.Count == 0)
                                        Color_Print_Red("Книги с таким названием не найдено в библиотеке");
                                    else
                                    {
                                        Book book = list[0];
                                        Information(book);
                                        Console.Write("Введите фамилию читателя: ");
                                        string surname = Console.ReadLine();
                                        Console.Write("Введите имя читателя: ");
                                        string name = Console.ReadLine();
                                        Console.Write("Введите отчество читателя: ");
                                        string patronymic = Console.ReadLine();
                                        try
                                        {
                                            Reader reader = form.Reader_search(surname, name, patronymic);
                                            try
                                            {
                                                reader.Return_book(book);
                                                Color_Print_Green("Книга успешно сдана!");
                                            }
                                            catch (BookNotFoundException ex)
                                            {
                                                Color_Print_Red(ex.Message);
                                            }
                                        }
                                        catch (ListEmptyException ex)
                                        {
                                            Color_Print_Red(ex.Message);
                                        }
                                        catch (ReaderNotFoundException ex)
                                        {
                                            Color_Print_Red(ex.Message);
                                        }
                                    }
                                }
                                catch (ArgumentException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                break;
                            }
                    }
                }
                else
                {
                    List<string> menu_2 = Menu_2();
                    Print_menu(menu_2, 1);
                    num = Menu_navigation(menu_2);
                    Console.Clear();

                    switch (num)
                    {
                        case 1:
                            {
                                bool Bool = true;
                                string name;
                                Console.Write("Укажите название книги: ");
                                name = Console.ReadLine();
                                while (Bool)
                                {
                                    if (name == "")
                                    {
                                        Color_Print_Red("Книга должна иметь название!");
                                        Console.Write("Укажите название книги: ");
                                        name = Console.ReadLine();
                                    }
                                    else
                                        Bool = false;
                                }
                                Bool = true;
                                Console.Write("Укажите автора книги: ");
                                string author = Console.ReadLine();
                                while (Bool)
                                {
                                    if (author == "")
                                    {
                                        Color_Print_Red("Книга должна иметь автора!");
                                        Console.Write("Укажите автора книги: ");
                                        author = Console.ReadLine();
                                    }
                                    else
                                        Bool = false;
                                }
                                Console.Write("Укажите тематику книги: ");
                                string subject = Console.ReadLine();
                                try
                                {
                                    storage.Add_book(name, author, subject);
                                    Color_Print_Green("Книга успешно добавлена!");
                                }
                                catch (FoundMatchException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                break;
                            }
                        case 2:
                            {
                                Console.Write("Введите название книги, тематику которой хотите изменить: ");
                                string str = Console.ReadLine();
                                try
                                {
                                    List<Book> list = storage.Search_by_name(str);
                                    if (list.Count > 1)
                                        Color_Print_Red("Уточните название книги!");
                                    else if (list.Count == 0)
                                        Color_Print_Red("Книги с таким название не найдено!");
                                    else
                                    {
                                        Information(list[0]);
                                        Console.Write("Введите тематику книги: ");
                                        str = Console.ReadLine();
                                        list[0].Subject = str;
                                        Color_Print_Green("Тематика книги успешно изменена!");
                                    }
                                }
                                catch (ArgumentException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                break;
                            }
                        case 3:
                            {
                                Console.Write("Введите название книг: ");
                                string name = Console.ReadLine();
                                try
                                {
                                    List<Book> list = storage.Search_by_name(name);
                                    if (list.Count != 0)
                                    {
                                        for (int i = 0; i < list.Count; i++)
                                        {
                                            Console.Write($"{i + 1}) ");
                                            Information(list[i]);
                                        }
                                        bool Bool = true;
                                        while (Bool)
                                        {
                                            if (list.Count == 1)
                                            {
                                                Console.WriteLine("Вы действительно хотите удалить эту книгу?");
                                                Color_Print_Green("1 - Да");
                                                Color_Print_Red("2 - Нет");
                                                var Input = Console.ReadKey();
                                                int number = -1;
                                                if (char.IsDigit(Input.KeyChar))
                                                {
                                                    number = int.Parse(Input.KeyChar.ToString());
                                                    Console.WriteLine();
                                                }
                                                if (number == 1)
                                                {
                                                    storage.Delete_books_by_name(name);
                                                    Color_Print_Green("Книга с заданным название успешно удалена!");
                                                    Bool = false;
                                                }
                                                else if (number == 2)
                                                    Bool = false;
                                            }
                                            else if (list.Count > 1)
                                            {
                                                Console.WriteLine("Вы хотите удалить одну книгу или все?");
                                                Color_Print_Green("1 - Одну");
                                                Color_Print_Green("2 - Все");
                                                var Input = Console.ReadKey();
                                                int number = -1;
                                                if (char.IsDigit(Input.KeyChar))
                                                {
                                                    number = int.Parse(Input.KeyChar.ToString());
                                                    Console.WriteLine();
                                                }
                                                if (number == 1)
                                                {
                                                    bool exit = true;
                                                    while (exit)
                                                    {
                                                        Console.Write("Выберите номер книги, которую хотите удалить: ");
                                                        int Num = int.Parse(Console.ReadLine());
                                                        if (Num > 0 && Num <= list.Count)
                                                        {
                                                            storage.Delete_books_by_name(list[Num - 1].Name);
                                                            Color_Print_Green("Книга успешно удалена!");
                                                            exit = false;
                                                            Bool = false;
                                                        }
                                                        else
                                                            Color_Print_Red("Такого номера нет в списке!");
                                                    }
                                                }
                                                if (number == 2)
                                                {
                                                    storage.Delete_books_by_name(name);
                                                    Color_Print_Green("Книги с заданным название успешно удалены!");
                                                    Bool = false;
                                                }
                                            }
                                        }
                                    }
                                    else
                                        Color_Print_Red("Книги с заданным названием не найдено!");
                                }
                                catch (ArgumentException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                break;
                            }
                        case 4:
                            {
                                Console.Write("Введите автора книг: ");
                                string author = Console.ReadLine();
                                try
                                {
                                    List<Book> list = storage.Search_by_author(author);
                                    if (list.Count != 0)
                                    {
                                        for (int i = 0; i < list.Count; i++)
                                        {
                                            Console.Write($"{i + 1}) ");
                                            Information(list[i]);
                                        }
                                        bool Bool = true;
                                        while (Bool)
                                        {
                                            if (list.Count == 1)
                                            {
                                                Console.WriteLine("Вы действительно хотите удалить эту книгу?");
                                                Color_Print_Green("1 - Да");
                                                Color_Print_Red("2 - Нет");
                                                var Input = Console.ReadKey();
                                                int number = -1;
                                                if (char.IsDigit(Input.KeyChar))
                                                {
                                                    number = int.Parse(Input.KeyChar.ToString());
                                                    Console.WriteLine();
                                                }
                                                if (number == 1)
                                                {
                                                    storage.Delete_books_by_author(author);
                                                    Color_Print_Green("Книга с заданным автором успешно удалена!");
                                                    Bool = false;
                                                }
                                                else if (number == 2)
                                                    Bool = false;
                                            }
                                            else if (list.Count > 1)
                                            {
                                                Console.WriteLine("Вы хотите удалить одну книгу или все?");
                                                Color_Print_Green("1 - Одну");
                                                Color_Print_Green("2 - Все");
                                                var Input = Console.ReadKey();
                                                int number = -1;
                                                if (char.IsDigit(Input.KeyChar))
                                                {
                                                    number = int.Parse(Input.KeyChar.ToString());
                                                    Console.WriteLine();
                                                }
                                                if (number == 1)
                                                {
                                                    bool exit = true;
                                                    while (exit)
                                                    {
                                                        Console.Write("Выберите номер книги, которую хотите удалить: ");
                                                        int Num = int.Parse(Console.ReadLine());
                                                        if (Num > 0 && Num <= list.Count)
                                                        {
                                                            storage.Delete_books_by_name(list[Num - 1].Name);
                                                            Color_Print_Green("Книга успешно удалена!");
                                                            exit = false;
                                                            Bool = false;
                                                        }
                                                        else
                                                            Color_Print_Red("Такого номера нет в списке!");
                                                    }
                                                }
                                                if (number == 2)
                                                {
                                                    storage.Delete_books_by_author(author);
                                                    Color_Print_Green("Книги с заданным название успешно удалены!");
                                                    Bool = false;
                                                }
                                            }
                                        }
                                    }
                                    else
                                        Color_Print_Red("Книг с заданным автором не найдено!");
                                }
                                catch (ArgumentException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                break;
                            }
                        case 5:
                            {
                                Console.Write("Введите тематику книг: ");
                                string subject = Console.ReadLine();
                                try
                                {
                                    List<Book> list = storage.Search_by_subject(subject);
                                    if (list.Count != 0)
                                    {
                                        for (int i = 0; i < list.Count; i++)
                                        {
                                            Console.Write($"{i + 1}) ");
                                            Information(list[i]);
                                        }
                                        bool Bool = true;
                                        while (Bool)
                                        {
                                            if (list.Count == 1)
                                            {
                                                Console.WriteLine("Вы действительно хотите удалить эту книгу?");
                                                Color_Print_Green("1 - Да");
                                                Color_Print_Red("2 - Нет");
                                                var Input = Console.ReadKey();
                                                int number = -1;
                                                if (char.IsDigit(Input.KeyChar))
                                                {
                                                    number = int.Parse(Input.KeyChar.ToString());
                                                    Console.WriteLine();
                                                }
                                                if (number == 1)
                                                {
                                                    storage.Delete_books_by_subject(subject);
                                                    Color_Print_Green("Книга с заданным автором успешно удалена!");
                                                    Bool = false;
                                                }
                                                else if (number == 2)
                                                    Bool = false;
                                            }
                                            else if (list.Count > 1)
                                            {
                                                Console.WriteLine("Вы хотите удалить одну книгу или все?");
                                                Color_Print_Green("1 - Одну");
                                                Color_Print_Green("2 - Все");
                                                var Input = Console.ReadKey();
                                                int number = -1;
                                                if (char.IsDigit(Input.KeyChar))
                                                {
                                                    number = int.Parse(Input.KeyChar.ToString());
                                                    Console.WriteLine();
                                                }
                                                if (number == 1)
                                                {
                                                    bool exit = true;
                                                    while (exit)
                                                    {
                                                        Console.Write("Выберите номер книги, которую хотите удалить: ");
                                                        int Num = int.Parse(Console.ReadLine());
                                                        if (Num > 0 && Num <= list.Count)
                                                        {
                                                            storage.Delete_books_by_name(list[Num - 1].Name);
                                                            Color_Print_Green("Книга успешно удалена!");
                                                            exit = false;
                                                            Bool = false;
                                                        }
                                                        else
                                                            Color_Print_Red("Такого номера нет в списке!");
                                                    }
                                                }
                                                if (number == 2)
                                                {
                                                    storage.Delete_books_by_subject(subject);
                                                    Color_Print_Green("Книги с заданным название успешно удалены!");
                                                    Bool = false;
                                                }
                                            }
                                        }
                                    }
                                    else
                                        Color_Print_Red("Книг с заданной тематикой не найдено!");
                                }
                                catch (ArgumentException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                break;
                            }
                        case 6:
                            {
                                int k = 1;
                                if (storage.Recently_deleted().Count > 0)
                                {
                                    for (int i = storage.Recently_deleted().Count - 1; i >= 0; i--)
                                    {
                                        Console.Write($"{k}) ");
                                        Information(storage.Recently_deleted()[i]);
                                        k++;
                                    }
                                }
                                else
                                    Color_Print_Red("Список удалённых книг пуст!");
                                break;
                            }
                        case 7:
                            {
                                int k = 1;
                                if (storage.Recently_deleted().Count > 0)
                                {
                                    for (int i = storage.Recently_deleted().Count - 1; i >= 0; i--)
                                    {
                                        Console.Write($"{k}) ");
                                        Information(storage.Recently_deleted()[i]);
                                        k++;
                                    }
                                    Console.Write("Введите номер книги в списке удалённых книг: ");
                                    var Input = Console.ReadKey();
                                    int number = -1;
                                    if (char.IsDigit(Input.KeyChar))
                                    {
                                        number = int.Parse(Input.KeyChar.ToString());
                                        Console.WriteLine();
                                    }
                                    try
                                    {
                                        storage.Rebuild_the_book(number);
                                        Color_Print_Green("Книга успешно восстановлена!");
                                    }
                                    catch (ArgumentException)
                                    {
                                        Color_Print_Red("Такого номера нет в списке!");
                                    }
                                    catch (ListEmptyException ex)
                                    {
                                        Color_Print_Red(ex.Message);
                                    }
                                }
                                else
                                    Color_Print_Red("Список удалённых книг пуст!");
                                break;
                            }
                        case 8:
                            {
                                Console.Write("Введите название книги, количество которых хотите узнать: ");
                                string name = Console.ReadLine();
                                try
                                {
                                    List<Book> list = storage.Search_by_name(name);
                                    if (list.Count > 1)
                                        Color_Print_Red("Уточните название книги!");
                                    else if (list.Count == 0)
                                        Color_Print_Red("Книги с таким название не найдено!");
                                    else
                                    {
                                        Information(list[0]);
                                        Console.WriteLine($"Количество указанных книг: {list[0].Copies}");
                                    }
                                }
                                catch (ArgumentException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                break;
                            }
                        case 9:
                            {
                                Console.Write("Введите название книги, количество которых хотите увеличить: ");
                                string name = Console.ReadLine();
                                try
                                {
                                    List<Book> list = storage.Search_by_name(name);
                                    if (list.Count > 1)
                                        Color_Print_Red("Уточните название книги!");
                                    else if (list.Count == 0)
                                        Color_Print_Red("Книги с таким название не найдено!");
                                    else
                                    {
                                        Information(list[0]);
                                        int number = 0;
                                        Console.Write("Укажите количество книг: ");
                                        try
                                        {
                                            number = int.Parse(Console.ReadLine());
                                            storage.Increase_the_number_of_these_books(name, number);
                                            Color_Print_Green("Количество заданных книг увеличино!");
                                        }
                                        catch (Exception)
                                        {
                                            Color_Print_Red("Неверно указанно количество книг!");
                                            Console.ResetColor();
                                        }
                                    }
                                }
                                catch (ArgumentException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                break;
                            }
                        case 10:
                            {
                                Console.Write("Введите название книги, количество которых хотите уменьшить: ");
                                string name = Console.ReadLine();
                                try
                                {
                                    List<Book> list = storage.Search_by_name(name);
                                    if (list.Count > 1)
                                        Color_Print_Red("Уточните название книги!");
                                    else if (list.Count == 0)
                                        Color_Print_Red("Книги с таким название не найдено!");
                                    else
                                    {
                                        Information(list[0]);
                                        int number = 0;
                                        Console.Write("Укажите количество книг: ");
                                        try
                                        {
                                            number = int.Parse(Console.ReadLine());
                                            storage.Reduce_the_number_of_these_books(name, number);
                                            Color_Print_Green("Количество заданных книг уменьшено!");
                                        }
                                        catch (Exception)
                                        {
                                            Color_Print_Red("Неверно указанно количество книг!");
                                        }
                                    }
                                }
                                catch (ArgumentException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                break;
                            }
                        case 11:
                            {
                                try
                                {
                                    Console.WriteLine("Список зарегестрированных читателей: ");
                                    for (int i = 0; i < form.List_readers().Count; i++)
                                    {
                                        Console.Write($"{i + 1}) ");
                                        Information(form.List_readers()[i]);
                                    }
                                }
                                catch (ListEmptyException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                break;
                            }
                        case 12:
                            {
                                Console.Write("Введите фамилию читателя: ");
                                string surname = Console.ReadLine();
                                Console.Write("Введите имя читателя: ");
                                string name = Console.ReadLine();
                                Console.Write("Введите отчество читателя: ");
                                string patronymic = Console.ReadLine();
                                if (surname != "" && name != "" && patronymic != "")
                                {
                                    try
                                    {
                                        form.Register_new_reader(surname, name, patronymic);
                                        Color_Print_Green("Читатель зарегестрирован!");
                                    }
                                    catch (FailedToRegisterException ex)
                                    {
                                        Color_Print_Red(ex.Message);
                                    }
                                }
                                else
                                {
                                    Color_Print_Red("Заполните все поля!");
                                }
                                break;
                            }
                        case 13:
                            {
                                Console.Write("Введите фамилию читателя: ");
                                string surname = Console.ReadLine();
                                Console.Write("Введите имя читателя: ");
                                string name = Console.ReadLine();
                                Console.Write("Введите отчество читателя: ");
                                string patronymic = Console.ReadLine();
                                try
                                {
                                    Reader reader = form.Reader_search(surname, name, patronymic);
                                    if (reader.List_books().Count == 0)
                                        Color_Print_Red("Читатель ещё не брал книги!");
                                    for (int i = 0; i < reader.List_books().Count; i++)
                                    {
                                        Console.Write($"{i + 1}) ");
                                        Information(reader.List_books()[i]);
                                    }
                                }
                                catch (ListEmptyException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                catch (ReaderNotFoundException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                break;
                            }
                        case 14:
                            {
                                Console.Write("Введите фамилию читателя: ");
                                string surname = Console.ReadLine();
                                Console.Write("Введите имя читателя: ");
                                string name = Console.ReadLine();
                                Console.Write("Введите отчество читателя: ");
                                string patronymic = Console.ReadLine();
                                try
                                {
                                    form.Delete_reader(surname, name, patronymic);
                                    Color_Print_Green("Карточка читателя аннулирована!");
                                }
                                catch(ListEmptyException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                catch (ObjectsRemainInTheListException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                catch (ReaderNotFoundException ex)
                                {
                                    Color_Print_Red(ex.Message);
                                }
                                break;
                            }
                    }
                }
            }
        }
    }
}
