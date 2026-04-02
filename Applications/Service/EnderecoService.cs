using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.EnderecoDto;
using GerenciamentoPatrimonio.Execeptions;
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

        public ListarEnderecoDto BuscarPorId(Guid enderecoId) 
        {
            Endereco endereco = _repository.BuscarPorId(enderecoId);

            if (endereco == null)
            {
                throw new DomainException("Endereço não encontrada.");
            }

            ListarEnderecoDto enderecoDto = new ListarEnderecoDto
            {
                EnderecoID = endereco.EnderecoID,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                CEP = endereco.CEP,
                BairroID = endereco.BairroID
            };

            return enderecoDto;
        }

        public void Adicionar(CriarEnderecoDto dto)
        {
            if(!_repository.BairroExiste(dto.BairroID))
            {
                throw new DomainException("Bairro não cadastrado.");
            }

            Endereco enderecoExiste = _repository.BuscarPorLogradouroENumero(dto.Logradouro, dto.Numero, dto.BairroID);

            if (enderecoExiste != null)
            {
                throw new DomainException("Esse endereço já existe");
            }

            Endereco endereco = new Endereco
            {
                Logradouro = dto.Logradouro,
                Numero = dto.Numero,
                Complemento = dto.Complemento,
                CEP = dto.CEP,
                BairroID = dto.BairroID
            };

            _repository.Adicionar(endereco);
        }

        public void Atualizar(Guid enderecoId, CriarEnderecoDto dto)
        {
            if (!_repository.BairroExiste(dto.BairroID))
            {
                throw new DomainException("Bairro não cadastrado.");
            }

            Endereco enderecoBanco = _repository.BuscarPorId(enderecoId);

            if (enderecoBanco == null)
            {
                throw new DomainException("Endereço não encontrado.");
            }

            Endereco enderecoExiste = _repository.BuscarPorLogradouroENumero(dto.Logradouro, dto.Numero, dto.BairroID);


            if (enderecoExiste != null)
            {
                throw new DomainException("Esse endereço já está cadastrado.");
            }

            enderecoBanco.Logradouro = dto.Logradouro;
            enderecoBanco.BairroID = dto.BairroID;
            enderecoBanco.Numero = dto.Numero;
            enderecoBanco.Complemento = dto.Complemento;
            enderecoBanco.CEP = dto.CEP;

            _repository.Atualizar(enderecoBanco);
                
        }
    }
}
