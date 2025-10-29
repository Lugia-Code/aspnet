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

        //  Retorna uma lista paginada de tags
        public async Task<IEnumerable<Tag>> GetPagedAsync(int page, int pageSize)
        {
            return await _db.Tag
                .OrderBy(t => t.Chassi )
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        // Conta total de registros
        public async Task<int> CountAsync()
        {
            return await _db.Tag.CountAsync();
        }

    
        public async Task<Tag?> GetByCodigoAsync(string codigo)
        {
            return await _db.Tag
                .FirstOrDefaultAsync(t => t.Chassi == codigo);
        }

        //  Busca por ID
        public async Task<Tag?> GetByIdAsync(string id)
        {
            return await _db.Tag
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Chassi == id);
        }

        // Cria e salva uma nova tag (usado pelo Handler)
        public async Task<Tag> CreateAsync(Tag tag)
        {
            await _db.Tag.AddAsync(tag);
            await _db.SaveChangesAsync();
            return tag;
        }

        // Atualiza uma tag existente
        public async Task UpdateAsync(Tag tag)
        {
            _db.Tag.Update(tag);
            await _db.SaveChangesAsync();
        }

        //  Remove uma tag
        public async Task DeleteAsync(Tag tag)
        {
            _db.Tag.Remove(tag);
            await _db.SaveChangesAsync();
        }

        // Persiste alterações pendentes (opcional)
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
        

        public async Task<bool> AnyWithChassiAsync(string chassi)
        {
            if (string.IsNullOrWhiteSpace(chassi))
                return false;

            // Usa CountAsync > 0, que sempre gera SQL compatível com Oracle
            var exists = await _db.Tag
                .Where(t => t.Chassi == chassi)
                .CountAsync();

            return exists > 0;
        }

    }
}


