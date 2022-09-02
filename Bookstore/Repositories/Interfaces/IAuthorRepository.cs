using DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Repositories.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> Serach(string term);
    }
}
