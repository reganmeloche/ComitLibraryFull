using System;
using System.Collections.Generic;
using ComitLibrary.Models;

namespace ComitLibrary.Storage
{
    public class PatronStorageList : IStorePatrons
    {
        private readonly List<Patron> _patronList;

        public PatronStorageList() {
            _patronList = new List<Patron>();
        }

        public void Create(Patron newPatron) {
            _patronList.Add(newPatron);
        }

        public void Update(Patron updatedPatron) {
            // todo
        }
        
        public Patron GetById(Guid id) {
            var patron = _patronList.Find(x => x.Id == id);

            if (patron == null) {
                throw new Exception($"Patron {id} does not exist!!");
            }

            return patron;
        }

        public List<Patron> GetAll() {
            return _patronList;
        }
    }
}