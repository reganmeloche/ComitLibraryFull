using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            var bookModel = ConvertToDb(newBook);
            _context.Books.Add(bookModel);
            _context.SaveChanges();
        }

        public void Update(Book bookToUpdate) {
            var bookDb = ConvertToDb(bookToUpdate);
            _context.Books.Update(bookDb);
            _context.SaveChanges();
        }
        
        public Book GetById(Guid id) {
            var bookFromDb = _context.Books
                .AsNoTracking()
                .Where(x => x.IsDeleted == false)
                .First(x => x.BookId == id);
            var book = ConvertFromDb(bookFromDb);
            return book;
        }

        public List<Book> GetAll() {
            List<Book> results = new List<Book>();

            var booksFromDb = _context.Books
                .AsNoTracking()
                .Where(x => x.IsDeleted == false)
                .ToList();

            foreach (var bookFromDb in booksFromDb) {
                var nextBook = ConvertFromDb(bookFromDb);
                results.Add(nextBook);
            }

            return results;
        }

        public void Delete(Guid id) {
            var bookFromDb = _context.Books
                .AsNoTracking()
                .First(x => x.BookId == id);
            bookFromDb.IsDeleted = true;
            _context.Books.Update(bookFromDb);
            _context.SaveChanges();
        }

        public static Book ConvertFromDb(EFModels.Book bookFromDb) {
            return new Book() {
                Id = bookFromDb.BookId,
                Author = bookFromDb.Author,
                Title = bookFromDb.Title,
                IsCheckedOut = bookFromDb.IsCheckedOut,
                Year = bookFromDb.Year,
            };
        }

        public static EFModels.Book ConvertToDb(Book book) {
            return new EFModels.Book() {
                BookId = book.Id,
                Title = book.Title,
                Author = book.Author,
                IsCheckedOut = book.IsCheckedOut,
                Year = book.Year
            };
        }
    }
}