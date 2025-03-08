using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.Domain.Entities
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public decimal Preco { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }
        public int Estoque { get; set; }
    }
}
