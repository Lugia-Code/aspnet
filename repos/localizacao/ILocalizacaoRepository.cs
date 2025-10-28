using TrackingCodeApi.models;
using System.Threading.Tasks;

namespace TrackingCodeApi.repos.localizacao
{
    public interface ILocalizacaoRepository
    {
        Task<Localizacao?> FindAsyncById(int id);
        Task<Localizacao> CreateAsync(Localizacao localizacao);
    }
}