using System;

namespace ComitLibrary.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsCheckedOut { get; set; }
        public int Year { get; set; }
        public Guid UserId { get; set; }

        public void CheckOut() {
            if (!IsCheckedOut) {
                IsCheckedOut = true;
            } else {
                throw new Exception($"Book {Id} is already checked out!");
            }
        }

        public void CheckIn() {
            if (IsCheckedOut) {
                IsCheckedOut = false;
            } else {
                throw new Exception($"Book {Id} is already checked in!");
            }
        }

        public override string ToString()
        {
            string details = $"\n----- Book {Id} -----\n";
            details += $"Title: {Title}\n";
            details += $"Author: {Author}\n";
            details += $"Checked out: {IsCheckedOut}\n";
            details += "-------------------------\n";
            return details;
        }
    }
}