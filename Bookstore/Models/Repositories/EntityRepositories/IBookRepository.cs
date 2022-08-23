using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories.EntityRepositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<bool> IConnectedToBook(string fileName);

        Task<IEnumerable<Book>> Serach(string term);
    }
}
