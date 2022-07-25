using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ProFind.Lib.Global.Models;

namespace ProFind.Lib.Global.Services
{
    public class PFProjectService : ICrudService<PFProject>
    {
        public async Task<PFProject> GetObjectAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFProject>> ListObjectAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFProject>> ListPaginatedObjectAsync(int fromIndex)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFProject>> ListPaginatedObjectAsync(int fromIndex, int toIndex)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFProject>> Search(IDictionary<string, string> searchCriteria)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Create(PFProject toCreateObject)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Update(PFProject toUpdateObject)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}