using System;

namespace ComitLibrary.Models
{
    public class Loan
    {
        public Loan(Patron patron, Book book) {
            Id = Guid.NewGuid();
            Book = book;
            Patron = patron;
            IsReturned = false;
        }

        public Loan(Guid id, Patron patron, Book book, bool isReturned) {
            Id = id;
            Patron = patron;
            Book = book;
            IsReturned = isReturned;
        }

        public Guid Id { get; }
        public Patron Patron { get; private set; }
        public Book Book { get; private set; }
        public bool IsReturned { get; set; }
    }
}