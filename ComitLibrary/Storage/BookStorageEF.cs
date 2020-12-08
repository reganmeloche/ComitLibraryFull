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
        
        public Book GetById(Guid id, Guid userId) {
            var bookFromDb = _context.Books
                .AsNoTracking()
                .Where(x => x.IsDeleted == false && x.UserId == userId)
                .First(x => x.BookId == id);
            var book = ConvertFromDb(bookFromDb);
            return book;
        }

        public List<Book> GetAll(Guid userId) {
            List<Book> results = new List<Book>();

            var booksFromDb = _context.Books
                .AsNoTracking()
                .Where(x => x.IsDeleted == false && x.UserId == userId)
                .ToList();

            foreach (var bookFromDb in booksFromDb) {
                var nextBook = ConvertFromDb(bookFromDb);
                results.Add(nextBook);
            }

            return results;
        }

        public void Delete(Guid id, Guid userId) {
            var bookFromDb = _context.Books
                .AsNoTracking()
                .First(x => x.BookId == id && x.UserId == userId);
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
                UserId = bookFromDb.UserId,
            };
        }

        public static EFModels.Book ConvertToDb(Book book) {
            return new EFModels.Book() {
                BookId = book.Id,
                Title = book.Title,
                Author = book.Author,
                IsCheckedOut = book.IsCheckedOut,
                Year = book.Year,
                UserId = book.UserId,
            };
        }
    }
}