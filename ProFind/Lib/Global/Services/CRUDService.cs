using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ProFind.Lib.Global.Services
{
    public interface ICrudService<T>
    {
        Task<T> GetObjectAsync(string id);

        Task<IEnumerable<T>> ListObjectAsync();

        Task<IEnumerable<T>> ListPaginatedObjectAsync(int fromIndex);

        Task<IEnumerable<T>> ListPaginatedObjectAsync(int fromIndex, int toIndex);

        Task<IEnumerable<T>> Search(IDictionary<string, string> searchCriteria);

        Task<HttpStatusCode> Create(T toCreateObject);

        Task<HttpStatusCode> Update(T toUpdateObject);

        Task<HttpStatusCode> Delete(string id);
    }
}