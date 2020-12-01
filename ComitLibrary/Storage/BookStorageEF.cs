using System;
using System.Collections.Generic;
using System.Linq;

using ComitLibrary.Models;

namespace ComitLibrary.Storage
{
    public class BookStorageEF : IStoreBooks
    {
        private ApplicationContext _context;

        public BookStorageEF(ApplicationContext context) {
            _context = context;
        }

        public void Create(Book newBook) {
            var bookModel = new EFModels.Book() {
                BookId = newBook.Id,
                Author = newBook.Author,
                Title = newBook.Title,
                IsCheckedOut = newBook.IsCheckedOut,
            };
            _context.Books.Add(bookModel);
            _context.SaveChanges();
        }

        public void Update(Book bookToUpdate) {
            
        }
        
        public Book GetById(Guid id) {
            var bookFromDb = _context.Books.First(x => x.BookId == id);
            var book = ConvertFromDb(bookFromDb);
            return book;
        }

        public List<Book> GetByTitle(string title) {
            return new List<Book>();
        }

        public List<Book> GetAll() {
            List<Book> results = new List<Book>();

            var booksFromDb = _context.Books.ToList();

            foreach (var bookFromDb in booksFromDb) {
                var nextBook = ConvertFromDb(bookFromDb);
                results.Add(nextBook);
            }

            return results;
        }

        private static Book ConvertFromDb(EFModels.Book bookFromDb) {
            return new Book() {
                Id = bookFromDb.BookId,
                Author = bookFromDb.Author,
                Title = bookFromDb.Title,
                IsCheckedOut = bookFromDb.IsCheckedOut,
            };
        }
    }
}