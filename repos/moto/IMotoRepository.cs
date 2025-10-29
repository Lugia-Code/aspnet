using TrackingCodeApi.models;

namespace TrackingCodeApi.repos.moto
{
    public interface IMotoRepository
    {
        Task<IEnumerable<Moto>> GetPagedAsync(int page, int pageSize);
        Task<Moto?> FindAsyncById(string id);
        Task<Moto> AddAsync(Moto moto);
        Task UpdateAsync(Moto moto);
        Task DeleteAsync(Moto moto);
        Task<int> CountAsync();
        Task SaveAsync();
        Task<IEnumerable<Moto>> GetBySetorAsync(int idSetor);

    }
}