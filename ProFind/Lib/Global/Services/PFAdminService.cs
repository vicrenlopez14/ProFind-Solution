using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ProFind.Lib.Global.Models;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace ProFind.Lib.Global.Services
{
    public class PFAdminService : ICrudService<PFAdmin>
    {
        public async Task<PFAdmin> GetObjectAsync(string id)
        {
            PFAdmin admin = null;
            HttpResponseMessage response = await WebAPIConnection.GetConnection.GetAsync($"api/Admin/{id}");
            if (response.IsSuccessStatusCode)
            {
                admin = await response.Content.ReadAsAsync<PFAdmin>();
            }

            return admin;
        }

        public async Task<IEnumerable<PFAdmin>> ListObjectAsync()
        {
            IEnumerable<PFAdmin> admin = null;
            HttpResponseMessage response = await WebAPIConnection.GetConnection.GetAsync("api/Admin/list");
            if (response.IsSuccessStatusCode)
            {
                admin = await response.Content.ReadAsAsync<IEnumerable<PFAdmin>>();
            }

            return admin;
        }

        public async Task<IEnumerable<PFAdmin>> ListPaginatedObjectAsync(int fromIndex) =>
            await ListPaginatedObjectAsync(fromIndex, -1);

        public async Task<IEnumerable<PFAdmin>> ListPaginatedObjectAsync(int fromIndex, int toIndex)
        {
            IEnumerable<PFAdmin> admin = null;
            HttpResponseMessage response = await WebAPIConnection.GetConnection.GetAsync("api/Admin/list");
            if (response.IsSuccessStatusCode)
            {
                admin = await response.Content.ReadAsAsync<IEnumerable<PFAdmin>>();
            }

            return admin;
        }

        public async Task<IEnumerable<PFAdmin>> Search(IDictionary<string, string> searchCriteria)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Create(PFAdmin toCreateObject)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Update(PFAdmin toUpdateObject)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpStatusCode> Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}