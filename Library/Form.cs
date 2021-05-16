using System;
using System.Collections.Generic;

namespace MyLibrary
{
    public class Form
    {
        private List<Reader> reader_list = new List<Reader>();
        public void Register_new_reader(string surname, string name, string patronymic)
        {
            try
            {
                Reader reader = Reader_search(surname, name, patronymic);
                throw new FailedToRegisterException("Такой читатель уже зарегестрирован!");
            }
            catch (ListEmptyException)
            {
                reader_list.Add(new Reader(surname, name, patronymic));
            }
            catch (ReaderNotFoundException)
            {
                reader_list.Add(new Reader(surname, name, patronymic));
            }
        }
        public void Delete_reader(string surname, string name, string patronymic)
        {
            Reader reader = Reader_search(surname, name, patronymic);
            if (reader.Get_number_books_in_list() > 0)
                throw new ObjectsRemainInTheListException("Читатель ещё не вернул все книги!");
            else
            {
                for (int i = 0; i < reader_list.Count; i++)
                {
                    if (reader == reader_list[i])
                        reader_list.RemoveAt(i);
                }
            }
        }
        public Reader Reader_search(string surname, string name, string patronymic)
        {
            if (reader_list.Count == 0)
                throw new ListEmptyException("Список читателей пуст!");
            for (int i = 0; i < reader_list.Count; i++)
            {
                int counter = 0;

                if (reader_list[i].Name.ToUpper().Contains(name.ToUpper()))
                    counter++;
                if (reader_list[i].Surname.ToUpper().Contains(surname.ToUpper()))
                    counter++;
                if (reader_list[i].Patronymic.ToUpper().Contains(patronymic.ToUpper()))
                    counter++;
                if (counter == 3)
                    return reader_list[i];
            }
            throw new ReaderNotFoundException("Данного читателя не найдено!");
        }
        public List<Reader> List_readers()
        {
            if (reader_list.Count == 0)
                throw new ListEmptyException("Список читателей пуст!");
            return reader_list;
        }
    }
}