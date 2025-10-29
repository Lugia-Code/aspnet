using Microsoft.EntityFrameworkCore;
using TrackingCodeApi.models;

namespace TrackingCodeApi.repos.setor
{
    public class SetorRepository : ISetorRepository
    {
        private readonly TrackingCodeDb _db;

        public SetorRepository(TrackingCodeDb db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Setor>> GetPagedAsync(int page, int pageSize)
        {
            return await _db.Setores
                .OrderBy(s => s.Nome)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Setor?> GetByIdAsync(int id)
        {
            return await _db.Setores.FirstOrDefaultAsync(s => s.IdSetor == id);
        }

        public async Task<Setor> CreateAsync(Setor setor)
        {
            await _db.Setores.AddAsync(setor);
            await _db.SaveChangesAsync();
            return setor;
        }

        public async Task UpdateAsync(Setor setor)
        {
            _db.Setores.Update(setor);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Setor setor)
        {
            _db.Setores.Remove(setor);
            await _db.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _db.Setores.CountAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}