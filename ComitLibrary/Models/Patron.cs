using System;

namespace ComitLibrary.Models
{
    public class Patron
    {
        public Patron(Guid id, string firstName, string lastName, Guid userId) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            JoinDate = DateTime.Now;
            UserId = userId;
        }

        public Patron(Guid id, string firstName, string lastName, DateTime joinDate, int booksOut, Guid userId) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            JoinDate = joinDate;
            BooksOut = booksOut;
            UserId = userId;
        }

        public Guid Id { get; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime JoinDate { get; private set; }
        public int BooksOut { get; private set; }
        public Guid UserId { get; set; }
        private static readonly int MAX_BOOKS_ALLOWED = 5;

        public string FullName { 
            get {
                return $"{LastName}, {FirstName}";
            } 
        }

        public int DaysBeingMember {
            get {
                return (DateTime.Now - JoinDate).Days;
            }
        }

        public void CheckOutBook() {
            if (BooksOut < MAX_BOOKS_ALLOWED) {
                BooksOut++;
            } else {
                throw new Exception($"Patron {Id} cannot check out any more books.");
            }
        }

        public void CheckInBook() {
            if (BooksOut > 0) {
                BooksOut--;
            } else {
                throw new Exception($"Patron {Id} has no books to return.");
            }
        }

        public override string ToString()
        {
            string details = $"\n----- Patron {Id} -----\n";
            details += $"Name: {FullName}\n";
            details += $"Join Date: {JoinDate} ({DaysBeingMember} days)\n";
            details += $"Books out: {BooksOut}\n";
            details += "-------------------------\n";
            return details;
        }

    }
}