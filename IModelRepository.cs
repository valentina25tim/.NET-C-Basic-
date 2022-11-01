using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP_Dal
{
    public interface IModelRepository <T> 
        where T : class
    {
        Task<List<T>> Parse();
        Task<List<T>> Print();

        Task<List<T>> GetFull(CancellationToken ct);
        Task<T> GetById (int Id, CancellationToken ct);
        Task<T> Add (T entity, CancellationToken ct);
        Task<T> Update(T entity, CancellationToken ct);
        Task<T> Delete (int Id, CancellationToken ct);
    }
}
