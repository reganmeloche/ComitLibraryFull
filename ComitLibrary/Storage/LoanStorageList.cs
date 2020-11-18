using System;
using System.Collections.Generic;
using ComitLibrary.Models;

namespace ComitLibrary.Storage
{
    public class LoanStorageList : IStoreLoans
    {
        private readonly List<Loan> _loanList;

        public LoanStorageList() {
            _loanList = new List<Loan>();
        }

        public void Create(Loan newLoan) {
            _loanList.Add(newLoan);
        }

        public Loan GetByPatronIdAndBookId(long patronId, long bookId) {
            var loan = _loanList.Find(x => x.Patron.Id == patronId && x.Book.Id == bookId);

            if (loan == null) {
                throw new Exception($"Loan does not exist for Patron {patronId} and Book {bookId}");
            }

            return loan;
        }
    }
}