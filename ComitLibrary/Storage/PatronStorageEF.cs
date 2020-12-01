using System;
using System.Collections.Generic;
using ComitLibrary.Models;

namespace ComitLibrary.Storage
{
    public class PatronStorageEF : IStorePatrons
    {
        private ApplicationContext _context;

        public PatronStorageEF(ApplicationContext context) {
            _context = context;
        }

        public void Create(Patron newPatron) {
        }
        
        public Patron GetById(Guid id) {
            return null;
        }

        public List<Patron> GetAll() {
            return new List<Patron>();
        }
    }
}