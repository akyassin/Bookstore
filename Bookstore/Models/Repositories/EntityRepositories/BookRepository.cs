using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories.EntityRepositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        BookstoreDbContext db;

        public BookRepository(BookstoreDbContext db) : base(db)
        {
            this.db = db;
        }

        public override async Task<Book> Get(int id)
        {
            return await db.Books.Include(b => b.Author).SingleOrDefaultAsync(b => b.Id == id);
        }

        public override async Task<IEnumerable<Book>> GetAll()
        {
            return await db.Books.Include(b => b.Author).ToListAsync();
        }

        public async Task<bool> IConnectedToBook(string fileName)
        {
            if (await db.Books.CountAsync(b => b.ImageName == fileName) > 1)
                return true;

            return false;
        }

        public async Task<IEnumerable<Book>> Serach(string term)
        {
            if (string.IsNullOrEmpty(term))
                return await GetAll();

            return await db.Books.Include(b => b.Author)
                .Where(b => b.Title.Contains(term)
                         || b.Description.Contains(term)
                         || b.Author.FullName.Contains(term))
                .ToListAsync();
        }
    }
}