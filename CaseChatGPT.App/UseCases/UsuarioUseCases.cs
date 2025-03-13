using CaseChatGPT.Domain.Entities;
using CaseChatGPT.Domain.Interfaces.Repositories;
using CaseChatGPT.Domain.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseChatGPT.App.UseCases
{
    public class UsuarioUseCases : IUsuarioUseCases
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioUseCases(IUsuarioRepository usuarioUseCases)
        {
            _usuarioRepository = usuarioUseCases;
        }
        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.GetUsuarios();
            return usuarios;
        }

        public async Task<Usuario> GetUsuarioById(string id)
        {
            if(string.IsNullOrEmpty(id)) throw new Exception("Id obrigatório");
            var usuario = await _usuarioRepository.GetUsuarioById(id);
            if(usuario == null) throw new Exception("Usuário não encontrado");
            return usuario;
        }

        public async Task AddUsuario(Usuario usuario, string passwordDTO)
        {
            await _usuarioRepository.AddUsuario(usuario, passwordDTO);
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
          await _usuarioRepository.UpdateUsuario(usuario);
        }

        public async Task DeleteUsuario(Usuario usuario)
        {
            await _usuarioRepository.DeleteUsuario(usuario);
        }

    }
}
