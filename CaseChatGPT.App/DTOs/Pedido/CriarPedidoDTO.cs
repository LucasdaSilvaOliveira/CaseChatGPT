using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.App.DTOs.Pedido
{
    public class CriarPedidoDTO
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Status { get; set; }
        public string UsuarioId { get; set; }
        public int ProdutoId { get; set; }
    }
}
