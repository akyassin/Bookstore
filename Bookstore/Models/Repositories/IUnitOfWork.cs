using Bookstore.Models.Repositories.EntityRepositories;
using System;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        Task<bool> Complete();
    }
}
