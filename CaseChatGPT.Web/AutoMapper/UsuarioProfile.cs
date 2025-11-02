using AutoMapper;
using CaseChatGPT.Web.Areas.Usuario.Models;
using CaseChatGPT.Web.DTOs.Usuario;

namespace CaseChatGPT.Web.AutoMapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<ObterUsuarioDTO, UsuarioViewModel>().ReverseMap();
        }
    }
}
