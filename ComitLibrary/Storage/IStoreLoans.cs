using System;
using ComitLibrary.Models;

namespace ComitLibrary.Storage
{
    public interface IStoreLoans
    {
        void Create(Loan newLoan);

        Loan GetByPatronIdAndBookId(Guid patronId, Guid bookId);
    }
}