using Microsoft.EntityFrameworkCore;
using ComitLibrary.Storage.EFModels;

namespace ComitLibrary.Storage
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {
            // Empty constructor body...
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Patron> Patrons { get; set; }
    }
}