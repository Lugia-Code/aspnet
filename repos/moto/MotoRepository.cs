using Microsoft.EntityFrameworkCore;
using TrackingCodeApi.models;

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
            return await _db.Moto
                .OrderBy(m => m.Chassi)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Moto?> FindAsyncById(string id)
        {
            return await _db.Moto
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Chassi == id);
        }

        public async Task<Moto> AddAsync(Moto moto)
        {
            await _db.Moto.AddAsync(moto);
            await _db.SaveChangesAsync();
            return moto;
        }

        public async Task UpdateAsync(Moto moto)
        {
            _db.Moto.Update(moto);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Moto moto)
        {
            _db.Moto.Remove(moto);
            await _db.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _db.Moto.CountAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}