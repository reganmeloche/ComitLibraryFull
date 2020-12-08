using System;

namespace ComitLibrary.Storage.EFModels
{
    public class Loan
    {
        public Guid LoanId { get; set; }
        public Patron Patron { get; set; }
        public Guid PatronId { get; set; }
        public Book Book { get; set; }
        public Guid BookId { get; set; }
        public bool IsReturned { get; set; }
        public Guid UserId { get; set; }
    }
}