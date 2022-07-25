using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ProFind.Lib.Global.Models;

namespace ProFind.Lib.Global.Services
{
    public class PFClientService: ICrudService<PFClient>

    {
        public async Task<PFClient> GetObjectAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFClient>> ListObjectAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFClient>> ListPaginatedObjectAsync(int fromIndex)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFClient>> ListPaginatedObjectAsync(int fromIndex, int toIndex)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFClient>> Search(IDictionary<string, string> searchCriteria)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Create(PFClient toCreateObject)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Update(PFClient toUpdateObject)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}