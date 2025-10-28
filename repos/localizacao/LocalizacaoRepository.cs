using Microsoft.EntityFrameworkCore;
using TrackingCodeApi.models;
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
            return await _db.Localizacoes.FirstOrDefaultAsync(l => l.IdLocalizacao == id);
        }

        public async Task<Localizacao> CreateAsync(Localizacao localizacao)
        {
            _db.Localizacoes.Add(localizacao);
            await _db.SaveChangesAsync();
            return localizacao;
        }
    }
}