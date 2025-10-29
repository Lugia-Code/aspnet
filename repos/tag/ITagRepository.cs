using TrackingCodeApi.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrackingCodeApi.repos.tag
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetPagedAsync(int page, int pageSize);
        Task<int> CountAsync();
        Task<Tag?> GetByCodigoAsync(string codigo);
        Task<Tag?> GetByIdAsync(string id);
        Task<Tag> CreateAsync(Tag tag);
        Task UpdateAsync(Tag tag);
        Task DeleteAsync(Tag tag);
        Task SaveAsync();
        
        Task<bool> AnyWithChassiAsync(string chassi);
    }
}



