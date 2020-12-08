using System;

namespace ComitLibrary.Storage.EFModels
{
    public class Patron
    {
        public Guid PatronId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime JoinDate { get; set; }
        public int BooksOut { get; set; }
        public Guid UserId { get; set; }
    }
}