using TrackingCodeApi.models;
using System.Threading.Tasks;

namespace TrackingCodeApi.repos.localizacao
{
    public interface ILocalizacaoRepository
    {
        Task<Localizacao?> FindAsyncById(int id);  // Buscar uma localização por ID
        Task<IEnumerable<Localizacao>> GetBySetorIdAsync(int setorId);  // Buscar localizações associadas a um setor
        Task<Localizacao> CreateAsync(Localizacao localizacao);  // Criar nova localização
        Task UpdateAsync(Localizacao localizacao);  // Atualizar localização
    }
}