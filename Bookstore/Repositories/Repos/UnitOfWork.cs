using Azure.Storage.Blobs;
using Bookstore.Repositories.Interfaces;
using DataAccess.Data;
using System;
using System.Threading.Tasks;

namespace Bookstore.Repositories.Repos
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly BookstoreDbContext db;
        private readonly BlobServiceClient blobClient;

        private IBookRepository bookRepository;
        private IAuthorRepository authorRepository;
        private IBlobRepository blobRepository;

        public UnitOfWork(BookstoreDbContext db, BlobServiceClient blobClient)
        {
            this.db = db;
            this.blobClient = blobClient;
        }

        public IBookRepository BookRepository => bookRepository ?? new BookRepository(db);

        public IAuthorRepository AuthorRepository => authorRepository ?? new AuthorRepository(db);

        public IBlobRepository BlobRepository => blobRepository ?? new BlobRepository(blobClient);

        public async Task<bool> Complete()
        {
            return await db.SaveChangesAsync() > 0;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
