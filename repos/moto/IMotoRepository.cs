using TrackingCodeApi.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrackingCodeApi.repos.moto
{
    public interface IMotoRepository
    {
        Task<IEnumerable<Moto>> GetPagedAsync(int page, int pageSize);
        Task<int> CountAsync();
        Task<Moto?> GetByChassiAsync(string chassi);
        Task<Moto?> GetByIdAsync(string id);
        Task AddAsync(Moto moto);      
        Task SaveAsync();              
        Task<Moto> UpdateAsync(Moto moto);
        Task DeleteAsync(Moto moto);
    }
}