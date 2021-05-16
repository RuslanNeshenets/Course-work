using System;
using System.Collections.Generic;
using System.Globalization;

namespace MyLibrary
{
    public class Reader
    {
        private string surname;
        private string name;
        private string patronymic;
        private List<Book> list_books = new List<Book>(10);
        public Reader(string surname, string name, string patronymic)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            this.name = ti.ToTitleCase(name);
            this.surname = ti.ToTitleCase(surname);
            this.patronymic = ti.ToTitleCase(patronymic);
        }
        public List<Book> List_books()
        {
            return list_books;
        }
        public void Take_book(Book book)
        {
            if (list_books.Count == 10)
            {
                throw new MaximumNumberOfBooksExcaption("Читатель взял максимальное количество книг - 10!");
            }
            else
            {
                for (int i = 0; i < list_books.Count; i++)
                {
                    if (book.Name == list_books[i].Name)
                        throw new FoundMatchException("Данная книжка уже есть у читателя");
                }
                if (book.Copies > 0)
                {
                    list_books.Add(book);
                    book.Copies--;
                }
            }
        }
        public void Return_book(Book book)
        {
            for (int i = 0; i < list_books.Count; i++)
            {
                if(book == list_books[i])
                {
                    book.Copies++;
                    list_books.RemoveAt(i);
                }
                else
                    throw new BookNotFoundException("Заданный читатель не брал такую книгу!");
            }
        }
        public int Get_number_books_in_list()
        {
            return list_books.Count;
        }
        public string Surname { get { return surname; } }
        public string Name { get { return name; } }
        public string Patronymic { get { return patronymic; } }
    }
}
