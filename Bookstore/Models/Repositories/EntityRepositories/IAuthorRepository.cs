using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories.EntityRepositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> Serach(string term);
    }
}
