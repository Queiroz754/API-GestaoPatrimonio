using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.EnderecoDto;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Service
{
    public class EnderecoService
    {
        private readonly IEnderecoRepository _repository;

        public EnderecoService(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarEnderecoDto> Listar()
        {
            List<Endereco> enderecos = _repository.Listar();

            List<ListarEnderecoDto> enderecosDto = enderecos.Select(endereco =>
            new ListarEnderecoDto
            {
                EnderecoID = endereco.EnderecoID,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                BairroID = endereco.BairroID,
            }).ToList();

            return enderecosDto;
        }
    }
}
