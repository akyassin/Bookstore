using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.Repositories.Interfaces
{
    public interface IBlobRepository
    {
        string GetBlob(string name, string containerName);
        Task<IEnumerable<string>> GetaAllBlob(string containerName);
        Task<bool> UploadBlob(string name, IFormFile file, string containerName);
        Task<bool> DeleteBlob(string name, string containerName);
    }
}
