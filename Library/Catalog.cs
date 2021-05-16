using System;
using System.Collections.Generic;

namespace MyLibrary
{
    public class Catalog
    {
        protected List<Book> catalog = new List<Book>();
        public List<Book> List_books()
        {
            return catalog;
        }
        public List<Book> Search_by_name(string name)
        {
            List<Book> list = new List<Book>();
            if (name.Length < 3)
                throw new ArgumentException("Введите не менее 3 символов!");
            for (int i = 0; i < catalog.Count; i++)
            {
                if(catalog[i].Name.ToUpper().Contains(name.ToUpper()))
                    list.Add(catalog[i]);
            }
            return list;
        }
        public List<Book> Search_by_author(string author)
        {
            List<Book> list = new List<Book>();
            if (author.Length < 3)
                throw new ArgumentException("Введите не менее 3 символов!");
            for (int i = 0; i < catalog.Count; i++)
            {
                if (catalog[i].Author != null && catalog[i].Author.ToUpper().Contains(author.ToUpper()))
                    list.Add(catalog[i]);
            }
            return list;
        }
        public List<Book> Search_by_subject(string subject)
        {
            List<Book> list = new List<Book>();
            if (subject.Length < 3)
                throw new ArgumentException("Введите не менее 3 символов!");
            for (int i = 0; i < catalog.Count; i++)
            {
                if (catalog[i].Subject != null && catalog[i].Subject.ToUpper().Contains(subject.ToUpper()))
                    list.Add(catalog[i]);
            }
            return list;
        }
    }
}