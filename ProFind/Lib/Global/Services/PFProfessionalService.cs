using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ProFind.Lib.Global.Models;

namespace ProFind.Lib.Global.Services
{
    public class PFProfessionalService : ICrudService<PFProfessional>
    {
        public async Task<PFProfessional> GetObjectAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFProfessional>> ListObjectAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFProfessional>> ListPaginatedObjectAsync(int fromIndex)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFProfessional>> ListPaginatedObjectAsync(int fromIndex, int toIndex)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<PFProfessional>> Search(IDictionary<string, string> searchCriteria)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Create(PFProfessional toCreateObject)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Update(PFProfessional toUpdateObject)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}