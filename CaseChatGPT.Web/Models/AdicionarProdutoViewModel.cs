namespace CaseChatGPT.Web.Models
{
    public class AdicionarProdutoViewModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string UsuarioId { get; set; }
    }
}
