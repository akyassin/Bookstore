using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories.EntityRepositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        BookstoreDbContext db;

        public AuthorRepository(BookstoreDbContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Author>> Serach(string term)
        {
            if (string.IsNullOrEmpty(term))
                return await GetAll();

            return await db.Authors.Where(b => b.FullName.Contains(term)).ToListAsync();
        }
    }
}