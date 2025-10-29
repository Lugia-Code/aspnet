using Microsoft.EntityFrameworkCore;
using TrackingCodeApi.models;

namespace TrackingCodeApi.repos.usuario
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly TrackingCodeDb _db;

        public UsuarioRepository(TrackingCodeDb db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _db.Usuario.AsNoTracking().ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _db.Usuario.FirstOrDefaultAsync(u => u.IdFuncionario == id);
        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            await _db.Usuario.AddAsync(usuario);
            await _db.SaveChangesAsync();
            return usuario;
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _db.Usuario.Update(usuario);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Usuario usuario)
        {
            _db.Usuario.Remove(usuario);
            await _db.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}