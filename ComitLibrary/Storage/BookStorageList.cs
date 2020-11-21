using System;
using System.Collections.Generic;
using System.Linq;

using ComitLibrary.Models;

namespace ComitLibrary.Storage
{
    public class BookStorageList : IStoreBooks
    {
        private readonly List<Book> _bookList;

        public BookStorageList() {
            _bookList = new List<Book>();
        }

        public void Create(Book newBook) {
            _bookList.Add(newBook);
        }

        public void Update(Book bookToUpdate) {
            var book = GetById(bookToUpdate.Id);
            book.Title = bookToUpdate.Title;
            book.Author = bookToUpdate.Author;
        }
        
        public Book GetById(Guid id) {
            var book = _bookList.Find(x => x.Id == id);

            if (book == null) {
                throw new Exception($"Book {id} does not exist!!");
            }

            return book;
        }

        public List<Book> GetByTitle(string title) {
            return _bookList.Where(x => x.Title.ToLower() == title.ToLower()).ToList();
        }

        public List<Book> GetAll() {
            return _bookList;
        }
    }
}