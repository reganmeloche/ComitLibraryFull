using System;

namespace ComitLibrary.Models
{
    public class Loan
    {
        public Loan(Patron patron, Book book, Guid userId) {
            Id = Guid.NewGuid();
            Book = book;
            Patron = patron;
            IsReturned = false;
            UserId = userId;
        }

        public Loan(Guid id, Patron patron, Book book, bool isReturned, Guid userId) {
            Id = id;
            Patron = patron;
            Book = book;
            IsReturned = isReturned;
            UserId = userId;
        }

        public Guid Id { get; }
        public Patron Patron { get; private set; }
        public Book Book { get; private set; }
        public bool IsReturned { get; set; }
        public Guid UserId { get; set; }
    }
}