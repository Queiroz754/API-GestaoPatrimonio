using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.AreaDto;
using GerenciamentoPatrimonio.DTO.CidadeDto;
using GerenciamentoPatrimonio.Execeptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Service
{
    public class CidadeService
    {
        private readonly ICidadeRepository _repository;

        public CidadeService(ICidadeRepository repository)
        {
            _repository = repository;
        }

        public List<ListarCidadeDto> Listar()
        {
            List<Cidade> cidades = _repository.Listar();

            List<ListarCidadeDto> cidadeDto = cidades.Select(cidades => new 
            ListarCidadeDto
            {
                CidadeID = cidades.CidadeID,
                NomeCidade = cidades.NomeCidade,
                Estado = cidades.Estado
            }).ToList();
                return cidadeDto;
        }

        public ListarCidadeDto BuscarPorId(Guid cidadeId)
        {
            Cidade cidade = _repository.BuscarPorId(cidadeId);

            if (cidade == null)
            {
                throw new DomainException("Cidade não encontrada.");
            }

            ListarCidadeDto cidadeDto = new ListarCidadeDto
            {
                CidadeID = cidade.CidadeID,
                NomeCidade = cidade.NomeCidade,
                Estado = cidade.Estado
            };

            return cidadeDto;
        }

        public void Adicionar(CriarCidadeDto dto)
        {
            Validar.ValidarNome(dto.NomeCidade);
            Cidade cidadeExistente = _repository.BuscarPorNomeEstado(dto.NomeCidade, dto.Estado);

            if(cidadeExistente != null)
            {
                throw new DomainException("Já existe essa cidade cadastrada neste estado.");
            }

            Cidade cidade = new Cidade
            {
                NomeCidade = dto.NomeCidade,
                Estado = dto.Estado,
            };

            _repository.Adicionar(cidade);

        
        }

        public void Atualizar(Guid cidadeId,CriarCidadeDto dto)
        {
            Validar.ValidarNome(dto.NomeCidade);

            Cidade cidadeBanco = _repository.BuscarPorId(cidadeId);

            if (cidadeBanco == null)
            {
                throw new DomainException("Cidade não encontrada.");
            }

            Cidade cidadeExistente = _repository.BuscarPorNomeEstado(dto.NomeCidade, dto.Estado);

            if (cidadeExistente != null)
            {
                throw new DomainException("Já existe uma cidade cadastrada com esse nome neste bairro.");
            }

            cidadeBanco.NomeCidade = dto.NomeCidade;
            cidadeBanco.Estado = dto.Estado;

            _repository.Atualizar(cidadeBanco);


        }

    }
}
