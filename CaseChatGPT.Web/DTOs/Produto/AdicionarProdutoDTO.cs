namespace CaseChatGPT.Web.DTOs.Produto
{
    public class AdicionarProdutoDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string UsuarioId { get; set; }
    }
}
