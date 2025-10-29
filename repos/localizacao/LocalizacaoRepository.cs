using TrackingCodeApi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrackingCodeApi.repos.localizacao
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private readonly TrackingCodeDb _db;

        public LocalizacaoRepository(TrackingCodeDb db)
        {
            _db = db;
        }

        public async Task<Localizacao?> FindAsyncById(int id)
        {
            return await _db.Localizacao
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.IdLocalizacao == id);  // Buscar localização por ID
        }

        public async Task<IEnumerable<Localizacao>> GetBySetorIdAsync(int setorId)
        {
            return await _db.Localizacao
                .Where(l => l.IdSetor == setorId)  // Localizações associadas a um setor
                .ToListAsync();
        }

        public async Task<Localizacao> CreateAsync(Localizacao localizacao)
        {
            await _db.Localizacao.AddAsync(localizacao);
            await _db.SaveChangesAsync();
            return localizacao;
        }

        public async Task UpdateAsync(Localizacao localizacao)
        {
            _db.Localizacao.Update(localizacao);
            await _db.SaveChangesAsync();
        }
    }
}