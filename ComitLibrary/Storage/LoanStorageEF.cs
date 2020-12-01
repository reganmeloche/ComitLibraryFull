using System;
using System.Collections.Generic;
using ComitLibrary.Models;

namespace ComitLibrary.Storage
{
    public class LoanStorageEF : IStoreLoans
    {
        private ApplicationContext _context;

        public LoanStorageEF(ApplicationContext context) {
            _context = context;
        }

        public void Create(Loan newLoan) {
        }

        public Loan GetByPatronIdAndBookId(Guid patronId, Guid bookId) {
            return null;
        }

        public Loan GetByBookId(Guid bookId) {
            return null;
        }
    }
}