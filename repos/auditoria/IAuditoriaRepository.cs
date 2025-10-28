
using TrackingCodeApi.models;
using System.Threading.Tasks;

namespace TrackingCodeApi.repos.auditoria
{
    public interface IAuditoriaRepository
    {
        Task<Auditoria?> FindAsyncById(int id);
        Task<Auditoria> CreateAsync(Auditoria auditoria);
    }
}
