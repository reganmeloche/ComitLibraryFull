using System;
using System.Collections.Generic;
using ComitLibrary.Models;

namespace ComitLibrary.Storage
{
    public interface IStoreLoans
    {
        void Create(Loan newLoan);

        void Update(Loan updatedLoan);

        Loan GetByBookId(Guid BookId);

        List<Loan> GetAll();
    }
}