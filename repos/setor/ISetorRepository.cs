using TrackingCodeApi.models;

namespace TrackingCodeApi.repos.setor
{
    public interface ISetorRepository
    {
        Task<IEnumerable<Setor>> GetPagedAsync(int page, int pageSize);
        Task<Setor?> GetByIdAsync(int id);
        Task<Setor> CreateAsync(Setor setor);
        Task UpdateAsync(Setor setor);
        Task DeleteAsync(Setor setor);
        Task<int> CountAsync();
        Task SaveAsync();
    }
}