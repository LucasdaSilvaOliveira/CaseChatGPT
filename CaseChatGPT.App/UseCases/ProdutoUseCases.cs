using AutoMapper;
using CaseChatGPT.App.DTOs.Produto;
using CaseChatGPT.Domain.Interfaces;

namespace CaseChatGPT.App.UseCases
{
    public class ProdutoUseCases
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        public ProdutoUseCases(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ObterProdutosDTO>> GetProdutos()
        {
            var produtos = await _produtoRepository.GetProdutos();

            var produtosDTO = _mapper.Map<IEnumerable<ObterProdutosDTO>>(produtos);

            return produtosDTO;
        }
    }
}
