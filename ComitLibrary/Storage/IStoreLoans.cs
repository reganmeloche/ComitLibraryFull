using ComitLibrary.Models;

namespace ComitLibrary.Storage
{
    public interface IStoreLoans
    {
        void Create(Loan newLoan);

        Loan GetByPatronIdAndBookId(long patronId, long bookId);
    }
}