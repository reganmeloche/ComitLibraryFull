using System;
using System.Collections.Generic;
using System.Linq;
using F23.StringSimilarity;

using ComitLibrary.Storage;
using ComitLibrary.Models;

namespace ComitLibrary
{

    public class LibrarySystem
    {
        /*** CONSTRUCTOR ***/
        public LibrarySystem(IStoreBooks bookStorage, IStorePatrons patronStorage, IStoreLoans loanStorage) {
            _bookStorage = bookStorage;
            _patronStorage = patronStorage;
            _loanStorage = loanStorage;
        }

        /*** STORAGE ***/
        private readonly IStoreBooks _bookStorage;
        private readonly IStorePatrons _patronStorage;
        private readonly IStoreLoans _loanStorage;
        

        /*** METHODS ***/
        public List<Book> SearchForBook(string titleToSearch, Guid userId) {
            List<Book> resultSet = new List<Book>();
            var l = new Levenshtein();
            string lowerCaseSearch = titleToSearch.ToLower();
            var books = _bookStorage.GetAll(userId);

            foreach (var book in books) {
                var lowerCaseTitle = book.Title.ToLower();
                if (l.Distance(lowerCaseSearch, lowerCaseTitle) < 5) {
                    resultSet.Add(book);
                }
            }

            return resultSet;
        }

        public List<Book> GetAllBooks(Guid userId) {
            return _bookStorage.GetAll(userId);
        }

        public Book GetBook(Guid id, Guid userId) {
            return _bookStorage.GetById(id, userId);
        }

        public void UpdateBook(Book bookToUpdate) {
            _bookStorage.Update(bookToUpdate);
        }

        public Book AddNewBook(Book newBook) {
            _bookStorage.Create(newBook);
            return newBook;
        }

        public List<Patron> GetAllPatrons(Guid userId) {
            return _patronStorage.GetAll(userId);
        }

        public void AddPatron(Patron newPatron) {
            _patronStorage.Create(newPatron);
        }

        public Loan CheckoutBook(Guid patronId, Guid bookId, Guid userId) {
            var patron = _patronStorage.GetById(patronId, userId);
            patron.CheckOutBook();
            _patronStorage.Update(patron);

            var book = _bookStorage.GetById(bookId, userId);
            book.CheckOut();
            _bookStorage.Update(book);

            var loan = new Loan(patron, book, userId);
            _loanStorage.Create(loan);
            return loan;
        }

        public void ReturnBook(Guid bookId, Guid userId) {
            var book = _bookStorage.GetById(bookId, userId);
            book.CheckIn();
            _bookStorage.Update(book);

            var loan = _loanStorage.GetByBookId(bookId, userId);
            
            var patron = _patronStorage.GetById(loan.Patron.Id, userId);
            patron.CheckInBook();
            _patronStorage.Update(patron);

            loan.IsReturned = true;
            _loanStorage.Update(loan);
        }

        public void DeleteBookById(Guid id, Guid userId) {
            _bookStorage.Delete(id, userId);
        }

        public List<Loan> GetAllActiveLoans(Guid userId) {
            var allLoans = _loanStorage.GetAll(userId);

            var activeLoans = allLoans
                .Where(x => x.IsReturned == false)
                .ToList();
            
            return activeLoans;
        }
    }
}