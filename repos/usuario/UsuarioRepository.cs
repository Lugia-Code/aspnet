
using Microsoft.EntityFrameworkCore;
using TrackingCodeApi.models;
using System.Threading.Tasks;

namespace TrackingCodeApi.repos.usuario
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly TrackingCodeDb _db;

        public UsuarioRepository(TrackingCodeDb db)
        {
            _db = db;
        }

        public async Task<Usuario?> FindAsyncById(int id)
        {
            return await _db.Usuarios.FirstOrDefaultAsync(u => u.IdFuncionario == id);
        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();
            return usuario;
        }
    }
}