using System;
using System.Collections.Generic;
using ComitLibrary.Models;

using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ComitLibrary.Storage
{
    public class LoanStorageEF : IStoreLoans
    {
        private ApplicationContext _context;

        public LoanStorageEF(ApplicationContext context) {
            _context = context;
        }

        public void Create(Loan newLoan) {
            var loanDb = ConvertToDb(newLoan);
            _context.Loans.Add(loanDb);
            _context.SaveChanges();
        }

        public void Update(Loan updatedLoan) {
            var loanDb = ConvertToDb(updatedLoan);
            _context.Loans.Update(loanDb);
            _context.SaveChanges();
        }

        public Loan GetByBookId(Guid bookId, Guid userId) {
            var loanDb = _context.Loans
                .AsNoTracking()
                .Include(x => x.Patron)
                .Include(x => x.Book)
                .First(x => x.BookId == bookId && x.IsReturned == false && x.UserId == userId);
            
            var loan = ConvertFromDb(loanDb);
            return loan;
        }

        public List<Loan> GetAll(Guid userId) {
            return _context.Loans
                .AsNoTracking()
                .Include(x => x.Patron)
                .Include(x => x.Book)
                .Where(x => x.UserId == userId)
                .Select(x => ConvertFromDb(x))
                .ToList();
        }

        private static EFModels.Loan ConvertToDb(Loan loan) {
            return new EFModels.Loan() {
                LoanId = loan.Id,
                BookId = loan.Book.Id,
                PatronId = loan.Patron.Id,
                IsReturned = loan.IsReturned,
                UserId = loan.UserId
                //Book = BookStorageEF.ConvertToDb(loan.Book),
                //Patron = PatronStorageEF.ConvertToDb(loan.Patron),
            };
        }

        private static Loan ConvertFromDb(EFModels.Loan loanDb) {
            return new Loan(
                loanDb.LoanId, 
                PatronStorageEF.ConvertFromDb(loanDb.Patron), 
                BookStorageEF.ConvertFromDb(loanDb.Book), 
                loanDb.IsReturned,
                loanDb.UserId);
        }
    }
}