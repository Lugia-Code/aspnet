using TrackingCodeApi.models;
using System.Threading.Tasks;

namespace TrackingCodeApi.repos.setor
{
    public interface ISetorRepository
    {
        Task<Setor?> GetByIdAsync(int id);
        Task<Setor> CreateAsync(Setor setor);
    }
}