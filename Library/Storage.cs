using System;
using System.Collections.Generic;
using System.IO;

namespace MyLibrary
{
    public class Storage: Catalog
    {
        private List<Book> Recently_deleted_books = new List<Book>(9);
        public void Sort()
        {
            int size = catalog.Count;
            Book temp;
            for (int i = 0; i < size - 1; i++)
            {
                for (int j = i + 1; j < size; j++)
                {
                    if (catalog[i].Name[0] > catalog[j].Name[0])
                    {
                        temp = catalog[i];
                        catalog[i] = catalog[j];
                        catalog[j] = temp;
                    }
                }
            }
        }
        public int Get_Size()
        {
            return catalog.Count;
        }
        public void Input_Books()
        {
            try
            {
                string path = @"D:\Catalog.txt";
                StreamReader sr = new StreamReader(path);
                bool Bool = true;
                bool write = false;
                string name = null;
                string author = null;
                string subject = null;
                while (Bool)
                {
                    string line = sr.ReadLine();
                    int number = 0;
                    if (line != null)
                    {
                        string str = null;
                        int num = 0;
                        for (int i = 0; i < line.Length; i++)
                        {
                            if (line[i] == '"' && number % 2 == 0)
                            {
                                write = true;
                                number++;
                                continue;
                            }
                            else if (line[i] == '"' && number % 2 == 1)
                            {
                                write = false;
                                number++;
                                if (number == 2)
                                    name = str;
                                else if (number == 4)
                                    author = str;
                                else if (number == 6)
                                    subject = str;
                                str = null;
                                continue;
                            }
                            if (write)
                                str += line[i];
                            if (number == 6 && char.IsDigit(line[i]))
                            {
                                num *= 10;
                                num += int.Parse(line[i].ToString());
                            }
                        }
                        Add_book(name, author, subject);
                        catalog[catalog.Count - 1].Copies = num;
                        name = null;
                        author = null;
                        subject = null;
                    }
                    else
                        Bool = false;
                }
            }
            catch (Exception)
            {
                throw new Exception("Не удалось загрузить список книг. Файла по заданному пути не существует!");
            }
        }
        public void Add_book(string name, string author, string subject = null)
        {
            bool Bool = true;
            for (int i = 0; i < catalog.Count; i++)
            {
                if (catalog[i].Name.ToUpper().Contains(name.ToUpper()))
                {
                    if (catalog[i].Author.ToUpper().Contains(author.ToUpper()))
                    {
                        Bool = false;
                    }
                }
            }
            if (Bool)
            {
                Book book = new Book(name, author, subject);
                catalog.Add(book);
            }
            else
                throw new FoundMatchException("Такая книга уже есть в каталоге!");
        }
        public void Delete_books_by_name(string name)
        {
            if (name.Length < 3)
                throw new ArgumentException("Введите не менее 3 символа!");
            for (int i = catalog.Count - 1; i >= 0; i--)
            {
                if (catalog[i].Name != null && catalog[i].Name.ToUpper().Contains(name.ToUpper()))
                {
                    if (Recently_deleted_books.Count < 9)
                        Recently_deleted_books.Add(catalog[i]);
                    else
                    {
                        Recently_deleted_books.RemoveAt(0);
                        Recently_deleted_books.Add(catalog[i]);
                    }
                    catalog.RemoveAt(i);
                }
            }
        }
        public void Delete_books_by_author(string author)
        {
            if (author.Length < 3)
                throw new ArgumentException("Введите не менее 3 символов!");
            for (int i = catalog.Count - 1; i >= 0; i--)
            {
                if (catalog[i].Author != null && catalog[i].Author.ToUpper().Contains(author.ToUpper()))
                {
                    if (Recently_deleted_books.Count < 9)
                        Recently_deleted_books.Add(catalog[i]);
                    else
                    {
                        Recently_deleted_books.RemoveAt(0);
                        Recently_deleted_books.Add(catalog[i]);
                    }
                    catalog.RemoveAt(i);
                }
            }
        }
        public void Delete_books_by_subject(string subject)
        {
            if (subject.Length < 3)
                throw new ArgumentException("Введите не менее 3 символов!");
            for (int i = catalog.Count - 1; i >= 0; i--)
            {
                if (catalog[i].Subject != null && catalog[i].Subject.ToUpper().Contains(subject.ToUpper()))
                {
                    if (Recently_deleted_books.Count < 9)
                        Recently_deleted_books.Add(catalog[i]);
                    else
                    {
                        Recently_deleted_books.RemoveAt(0);
                        Recently_deleted_books.Add(catalog[i]);
                    }
                    catalog.RemoveAt(i);
                }
            }
        }
        public List<Book> Recently_deleted()
        {
            return Recently_deleted_books;
        }
        public void Rebuild_the_book(int number)
        {
            if (Recently_deleted_books.Count == 0)
                throw new ListEmptyException("За последнее время ни одной книги не было удалено!");
            int i = Recently_deleted_books.Count - number;
            if (number > Recently_deleted_books.Count && number <= 0)
                throw new ArgumentException();
            catalog.Add(Recently_deleted_books[i]);
            Recently_deleted_books.RemoveAt(i);
        }
        public void Increase_the_number_of_these_books(string name, int num)
        {
            num = Math.Abs(num);
            List<Book> list = Search_by_name(name);
            list[0].Copies += num;
        }
        public void Reduce_the_number_of_these_books(string name, int num)
        {
            num = Math.Abs(num);
            {
                List<Book> list = Search_by_name(name);
                list[0].Copies -= num;
                if (list[0].Copies < 0)
                    list[0].Copies = 0;
            }
        }
    }
}
