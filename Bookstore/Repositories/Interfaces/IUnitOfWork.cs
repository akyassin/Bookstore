using System;
using System.Threading.Tasks;

namespace Bookstore.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        IBlobRepository BlobRepository { get; }

        Task<bool> Complete();
    }
}
