using System;

namespace ComitLibrary.Storage.EFModels
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsCheckedOut { get; set; }
        public bool IsDeleted { get; set; }
        public int Year { get; set; }
        public Guid UserId { get; set; }
    }
}