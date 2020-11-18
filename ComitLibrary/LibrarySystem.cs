using System;
using System.Collections.Generic;

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
            var newBook1 = new Book() {
                Id = 123,
                Title = "The Hobbit",
                Author = "Tolkien",
                IsCheckedOut = false
            };

            var newBook2 = new Book() {
                Id = 999,
                Title = "Handmaids Tale",
                Author = "Atwood",
                IsCheckedOut = false
            };

            var newBook3 = new Book() {
                Id = 76348,
                Title = "Slaughterhouse five",
                Author = "Vonnegut",
                IsCheckedOut = false
            };

            _bookStorage.Create(newBook1);
            _bookStorage.Create(newBook2);
            _bookStorage.Create(newBook3);

            // Create 2 sample patrons
            _patronStorage.Create(new Patron(11118888, "Pablo", "Listingart"));
            _patronStorage.Create(new Patron(22227777, "Jesselyn", "Popoff"));
        }

        /*** STORAGE ***/
        private readonly IStoreBooks _bookStorage;
        private readonly IStorePatrons _patronStorage;
        private readonly IStoreLoans _loanStorage;
        

        /*** METHODS ***/
        public List<Book> SearchForBook(string titleToSearch) {
            return _bookStorage.GetByTitle(titleToSearch);
        }

        public List<Book> GetAllBooks() {
            return _bookStorage.GetAll();
        }

        public Book AddNewBook(Book newBook) {
            _bookStorage.Create(newBook);
            return newBook;
        }

        public List<Patron> GetAllPatrons() {
            return _patronStorage.GetAll();
        }

        public Loan CheckoutBook(long patronId, long bookId) {
            var patron = _patronStorage.GetById(patronId);
            patron.CheckOutBook();

            var book = _bookStorage.GetById(bookId);
            book.CheckOut();

            var loan = new Loan(patron, book);
            _loanStorage.Create(loan);
            return loan;
        }

        public void ReturnBook(long patronId, long bookId) {
            var patron = _patronStorage.GetById(patronId);
            patron.CheckInBook();

            var book = _bookStorage.GetById(bookId);
            book.CheckIn();

            var loan = _loanStorage.GetByPatronIdAndBookId(patronId, bookId);
            loan.IsReturned = true;
        }
    }
}