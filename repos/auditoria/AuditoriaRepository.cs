
using Microsoft.EntityFrameworkCore;
using TrackingCodeApi.models;
using System.Threading.Tasks;

namespace TrackingCodeApi.repos.auditoria
{
    public class AuditoriaRepository : IAuditoriaRepository
    {
        private readonly TrackingCodeDb _db;

        public AuditoriaRepository(TrackingCodeDb db)
        {
            _db = db;
        }

        public async Task<Auditoria?> FindAsyncById(int id)
        {
            return await _db.Auditorias.FirstOrDefaultAsync(a => a.IdAudit == id);
        }

        public async Task<Auditoria> CreateAsync(Auditoria auditoria)
        {
            _db.Auditorias.Add(auditoria);
            await _db.SaveChangesAsync();
            return auditoria;
        }
    }
}
