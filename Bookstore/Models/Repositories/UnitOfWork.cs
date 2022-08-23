using Bookstore.Models.Repositories.EntityRepositories;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly BookstoreDbContext db;

        public UnitOfWork(BookstoreDbContext db)
        {
            this.db = db;
        }

        private IBookRepository bookRepository;
        public IBookRepository BookRepository => bookRepository ?? new BookRepository(db);

        private IAuthorRepository authorRepository;
        public IAuthorRepository AuthorRepository => authorRepository ?? new AuthorRepository(db);

        public async Task<bool> Complete()
        {
            return await db.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
