using System;
using System.Collections.Generic;
using ComitLibrary.Models;

namespace ComitLibrary.Storage
{
    public interface IStoreLoans
    {
        void Create(Loan newLoan);

        void Update(Loan updatedLoan);

        Loan GetByBookId(Guid BookId, Guid userId);

        List<Loan> GetAll(Guid userId);
    }
}