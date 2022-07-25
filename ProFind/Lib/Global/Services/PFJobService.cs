using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ProFind.Lib.Global.Models;

namespace ProFind.Lib.Global.Services
{
    public class PFJobService : ICrudService<PFJob>
    {
        public async Task<PFJob> GetObjectAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFJob>> ListObjectAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFJob>> ListPaginatedObjectAsync(int fromIndex)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFJob>> ListPaginatedObjectAsync(int fromIndex, int toIndex)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFJob>> Search(IDictionary<string, string> searchCriteria)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Create(PFJob toCreateObject)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Update(PFJob toUpdateObject)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}