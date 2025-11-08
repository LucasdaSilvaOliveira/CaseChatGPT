using CaseChatGPT.Web.DTOs.Usuario;

namespace CaseChatGPT.Web.Services.Usuario
{
    public interface IUsuarioService
    {
        Task<List<ObterUsuarioDTO>> ObterUsuarios();
        Task<ObterUsuarioDTO> ObterUsuarioPorId(string id);
        Task<bool> RemoverUsuario(string id);
        Task<ObterRoleUsuarioDTO> ObterRoleUsuarioPorId(string userId);
    }
}
