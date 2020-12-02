using System;
using System.Collections.Generic;
using ComitLibrary.Models;

namespace ComitLibrary.Storage
{
    public interface IStoreBooks
    {
        void Create(Book newBook);
        
        Book GetById(Guid id);

        void Update(Book bookToUpdate);

        List<Book> GetAll();

        void Delete(Guid id);
    }
}