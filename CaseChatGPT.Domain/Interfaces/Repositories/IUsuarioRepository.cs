using CaseChatGPT.Domain.Entities;

namespace CaseChatGPT.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetUsuarios();
        Task<Usuario> GetUsuarioById(string id);
        Task AddUsuario(Usuario usuario, string passwordDTO);
        Task UpdateUsuario(Usuario usuario);
        Task DeleteUsuario(Usuario usuario);
    }
}
