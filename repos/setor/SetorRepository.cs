using Microsoft.EntityFrameworkCore;
using TrackingCodeApi.models;
using System.Threading.Tasks;

namespace TrackingCodeApi.repos.setor
{
    public class SetorRepository : ISetorRepository
    {
        private readonly TrackingCodeDb _db;

        public SetorRepository(TrackingCodeDb db)
        {
            _db = db;
        }

        public async Task<Setor?> GetByIdAsync(int id)
        {
            return await _db.Setores.FirstOrDefaultAsync(s => s.IdSetor == id);
        }

        public async Task<Setor> CreateAsync(Setor setor)
        {
            _db.Setores.Add(setor);
            await _db.SaveChangesAsync();
            return setor;
        }
    }
}