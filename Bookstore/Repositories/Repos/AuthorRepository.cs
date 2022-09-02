using Bookstore.Repositories.Interfaces;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Repositories.Repos
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly BookstoreDbContext db;

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