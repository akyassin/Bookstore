using DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Repositories.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> Serach(string term);
    }
}
