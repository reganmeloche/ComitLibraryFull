using System.Collections.Generic;
using ComitLibrary.Models;

namespace ComitLibrary.Storage
{
    public interface IStorePatrons
    {
        void Create(Patron newPatron);
        
        Patron GetById(long id);

        List<Patron> GetAll();
    }
}