namespace CaseChatGPT.Web.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> Login(string username, string password);
        Task<bool> ObterProdutos();
    }
}
