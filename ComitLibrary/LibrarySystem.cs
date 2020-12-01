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
            // Init storage using Dependency Injection
            _bookStorage = bookStorage;
            _patronStorage = patronStorage;
            _loanStorage = loanStorage;

            // Create 3 sample books
            // var newBook1 = new Book() {
            //     Id = Guid.NewGuid(),
            //     Title = "The Hobbit",
            //     Author = "Tolkien",
            //     IsCheckedOut = false
            // };

            // var newBook2 = new Book() {
            //     Id = Guid.NewGuid(),
            //     Title = "Handmaids Tale",
            //     Author = "Atwood",
            //     IsCheckedOut = false
            // };

            // var newBook3 = new Book() {
            //     Id = Guid.NewGuid(),
            //     Title = "Slaughterhouse five",
            //     Author = "Vonnegut",
            //     IsCheckedOut = false
            // };

            // _bookStorage.Create(newBook1);
            // _bookStorage.Create(newBook2);
            // _bookStorage.Create(newBook3);

            // // Create 2 sample patrons
            // var patron1 = new Patron(Guid.NewGuid(), "Pablo", "Listingart");
            // Console.WriteLine($"Patron ID: {patron1.Id}");
            // _patronStorage.Create(patron1);
            // _patronStorage.Create(new Patron(Guid.NewGuid(), "Jesselyn", "Popoff"));
        }

        /*** STORAGE ***/
        private readonly IStoreBooks _bookStorage;
        private readonly IStorePatrons _patronStorage;
        private readonly IStoreLoans _loanStorage;
        

        /*** METHODS ***/
        public List<Book> SearchForBook(string titleToSearch) {
            List<Book> resultSet = new List<Book>();
            var l = new Levenshtein();
            string lowerCaseSearch = titleToSearch.ToLower();
            var books = _bookStorage.GetAll();

            foreach (var book in books) {
                var lowerCaseTitle = book.Title.ToLower();
                if (l.Distance(lowerCaseSearch, lowerCaseTitle) < 5) {
                    resultSet.Add(book);
                }
            }

            return resultSet;
        }

        public List<Book> GetAllBooks() {
            return _bookStorage.GetAll();
        }

        public Book GetBook(Guid id) {
            return _bookStorage.GetById(id);
        }

        public void UpdateBook(Book bookToUpdate) {
            _bookStorage.Update(bookToUpdate);
        }

        public Book AddNewBook(Book newBook) {
            _bookStorage.Create(newBook);
            return newBook;
        }

        public List<Patron> GetAllPatrons() {
            return _patronStorage.GetAll();
        }

        public void AddPatron(Patron newPatron) {
            _patronStorage.Create(newPatron);
        }

        public Loan CheckoutBook(Guid patronId, Guid bookId) {
            var patron = _patronStorage.GetById(patronId);
            patron.CheckOutBook();

            var book = _bookStorage.GetById(bookId);
            book.CheckOut();

            var loan = new Loan(patron, book);
            _loanStorage.Create(loan);
            return loan;
        }

        public void ReturnBook(Guid bookId) {
            var book = _bookStorage.GetById(bookId);
            book.CheckIn();

            var loan = _loanStorage.GetByBookId(bookId);
            
            var patron = _patronStorage.GetById(loan.Patron.Id);
            patron.CheckInBook();

            loan.IsReturned = true;
        }

        
    }
}