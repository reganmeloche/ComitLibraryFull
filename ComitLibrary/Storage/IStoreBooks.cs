using System;
using System.Collections.Generic;
using ComitLibrary.Models;

namespace ComitLibrary.Storage
{
    public interface IStoreBooks
    {
        void Create(Book newBook);
        
        Book GetById(long id);

        List<Book> GetByTitle(string title);

        List<Book> GetAll();
    }
}