using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            var patronDb = ConvertToDb(newPatron);
            _context.Patrons.Add(patronDb);
            _context.SaveChanges();
        }

        public void Update(Patron updatedPatron) {
            var patronDb = ConvertToDb(updatedPatron);
            _context.Patrons.Update(patronDb);
            _context.SaveChanges();
        }
        
        public Patron GetById(Guid id, Guid userId) {
            var patronDb = _context.Patrons
                .AsNoTracking()
                .First(x => x.PatronId == id && x.UserId == userId);
            
            var patron = ConvertFromDb(patronDb);
            return patron;
        }

        public List<Patron> GetAll(Guid userId) {
            return _context.Patrons
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => ConvertFromDb(x))
                .ToList();
        }

        public static Patron ConvertFromDb(EFModels.Patron patronDb) {
            return new Patron(
                patronDb.PatronId,
                patronDb.FirstName,
                patronDb.LastName,
                patronDb.JoinDate,
                patronDb.BooksOut,
                patronDb.UserId
            );
        }

        public static EFModels.Patron ConvertToDb(Patron patron) {
            return new EFModels.Patron() {
                PatronId = patron.Id,
                FirstName = patron.FirstName,
                LastName = patron.LastName,
                JoinDate = patron.JoinDate,
                BooksOut = patron.BooksOut,
                UserId = patron.UserId,
            };
        }
    }
}