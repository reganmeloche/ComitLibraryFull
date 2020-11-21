using System;
using System.Collections.Generic;
using ComitLibrary.Models;

namespace ComitLibrary.Storage
{
    public interface IStorePatrons
    {
        void Create(Patron newPatron);
        
        Patron GetById(Guid id);

        List<Patron> GetAll();
    }
}