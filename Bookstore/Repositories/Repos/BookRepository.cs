using Bookstore.Repositories.Interfaces;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Repositories.Repos
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly BookstoreDbContext db;

        public BookRepository(BookstoreDbContext db) : base(db)
        {
            this.db = db;
        }

        public override async Task<Book> Get(int id)
        {
            return await db.Books.Include(b => b.Author).SingleOrDefaultAsync(b => b.BookId == id);
        }

        public override async Task<IEnumerable<Book>> GetAll()
        {
            return await db.Books.Include(b => b.Author).ToListAsync();
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