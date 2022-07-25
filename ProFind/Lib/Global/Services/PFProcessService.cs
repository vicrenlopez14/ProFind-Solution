using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ProFind.Lib.Global.Models;

namespace ProFind.Lib.Global.Services
{
    public class PFProcessService :ICrudService<PFProcess>
    {
        public async Task<PFProcess> GetObjectAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFProcess>> ListObjectAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFProcess>> ListPaginatedObjectAsync(int fromIndex)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFProcess>> ListPaginatedObjectAsync(int fromIndex, int toIndex)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFProcess>> Search(IDictionary<string, string> searchCriteria)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Create(PFProcess toCreateObject)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Update(PFProcess toUpdateObject)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}