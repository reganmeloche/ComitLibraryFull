using System;
using System.Collections.Generic;
using ComitLibrary.Models;

namespace ComitLibrary.Storage
{
    public interface IStoreBooks
    {
        void Create(Book newBook);
        
        Book GetById(Guid id, Guid userId);

        void Update(Book bookToUpdate);

        List<Book> GetAll(Guid userId);

        void Delete(Guid id, Guid userId);
    }
}