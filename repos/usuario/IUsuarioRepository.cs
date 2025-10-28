
using TrackingCodeApi.models;
using System.Threading.Tasks;

namespace TrackingCodeApi.repos.usuario
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> FindAsyncById(int id);
        Task<Usuario> CreateAsync(Usuario usuario);
    }
}