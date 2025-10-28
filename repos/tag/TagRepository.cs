using Microsoft.EntityFrameworkCore;
using TrackingCodeApi.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingCodeApi.repos.tag
{
    public class TagRepository : ITagRepository
    {
        private readonly TrackingCodeDb _db;

        public TagRepository(TrackingCodeDb db)
        {
            _db = db;
        }

        // 🔹 Retorna uma lista paginada de tags
        public async Task<IEnumerable<Tag>> GetPagedAsync(int page, int pageSize)
        {
            return await _db.Tags
                .OrderBy(t => t.CodigoTag)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        // 🔹 Conta total de registros
        public async Task<int> CountAsync()
        {
            return await _db.Tags.CountAsync();
        }

        // 🔹 Busca uma tag pelo código (usado no MotoHandler)
        public async Task<Tag?> GetByCodigoAsync(int codigo)
        {
            return await _db.Tags
                .FirstOrDefaultAsync(t => t.CodigoTag == codigo);
        }

        //  Busca uma tag pelo ID
        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _db.Tags
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.CodigoTag == id);
        }

        //  Cria uma nova tag (não salva imediatamente)
        public async Task AddAsync(Tag tag)
        {
            await _db.Tags.AddAsync(tag);
        }

        //  Atualiza uma tag existente
        public async Task UpdateAsync(Tag tag)
        {
            _db.Tags.Update(tag);
            await _db.SaveChangesAsync();
        }

        //  Remove uma tag
        public async Task DeleteAsync(Tag tag)
        {
            _db.Tags.Remove(tag);
            await _db.SaveChangesAsync();
        }

        //  Persiste as alterações pendentes
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
