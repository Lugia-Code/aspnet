using Microsoft.EntityFrameworkCore;
using TrackingCodeApi.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingCodeApi.repos.moto
{
    public class MotoRepository : IMotoRepository
    {
        private readonly TrackingCodeDb _db;

        public MotoRepository(TrackingCodeDb db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Moto>> GetPagedAsync(int page, int pageSize)
        {
            return await _db.Motos
                .OrderBy(m => m.Chassi)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _db.Motos.CountAsync();
        }

        public async Task<Moto?> GetByChassiAsync(string chassi)
        {
            return await _db.Motos
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Chassi == chassi);
        }

        public async Task<Moto?> GetByIdAsync(string id)
        {
            return await _db.Motos
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Chassi == id);
        }

        public async Task AddAsync(Moto moto)
        {
            await _db.Motos.AddAsync(moto);
            // NOTE: não chama SaveChanges aqui por padrão (chamado por SaveAsync)
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<Moto> UpdateAsync(Moto moto)
        {
            _db.Motos.Update(moto);
            await _db.SaveChangesAsync();
            return moto;
        }

        public async Task DeleteAsync(Moto moto)
        {
            _db.Motos.Remove(moto);
            await _db.SaveChangesAsync();
        }
    }
}