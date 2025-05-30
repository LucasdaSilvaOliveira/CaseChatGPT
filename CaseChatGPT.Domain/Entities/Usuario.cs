﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.Domain.Entities
{
    public class Usuario : IdentityUser
    {
        public string NomeCompleto { get; set; }
        public ICollection<Pedido>? Pedidos { get; set; }
        public ICollection<Produto>? Produtos { get; set; }
    }
}
