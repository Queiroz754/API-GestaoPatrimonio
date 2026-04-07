using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.AreaDto;
using GerenciamentoPatrimonio.DTO.PatrimonioDto;
using GerenciamentoPatrimonio.DTO.TipoPatrimonioDto;
using GerenciamentoPatrimonio.DTO.TipoUsuarioDto;
using GerenciamentoPatrimonio.Execeptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Service
{
    public class TipoPatrimonioService
    {
        private readonly ITipoPatrimonioRepository _repository;

        public TipoPatrimonioService(ITipoPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoPatrimonioDto> Listar()
        {
            List<TipoPatrimonio> tipos = _repository.Listar();

            List<ListarTipoPatrimonioDto> tipoDto = tipos.Select(tipo => new
            ListarTipoPatrimonioDto
            {
                TipoPatrimonioID = tipo.TipoPatrimonioID,
                NomeTipo = tipo.NomeTipo
            }).ToList();
            return tipoDto;
        }

        public ListarTipoPatrimonioDto BuscarPorId(Guid tipoPatrimonioId)
        {
            TipoPatrimonio tipoPatrimonio = _repository.BuscarPorId(tipoPatrimonioId);

            if (tipoPatrimonio == null)
            {
                throw new DomainException("Tipo de Patrimonio não encontrada.");
            }

            ListarTipoPatrimonioDto tipoPatrimonioDto = new ListarTipoPatrimonioDto
            {
                TipoPatrimonioID = tipoPatrimonio.TipoPatrimonioID,
                NomeTipo = tipoPatrimonio.NomeTipo
            };

            return tipoPatrimonioDto;
        }

        public ListarTipoPatrimonioDto BuscarPorNome(string nomeTipo)
        {
            TipoPatrimonio tipoPatrimonio = _repository.BuscarPorNome(nomeTipo);

            if (tipoPatrimonio == null)
            {
                throw new DomainException("Tipo de patrimonio não encontrado.");
            }

            ListarTipoPatrimonioDto tipoPatrimonioDto = new ListarTipoPatrimonioDto
            {
                TipoPatrimonioID = tipoPatrimonio.TipoPatrimonioID,
                NomeTipo = tipoPatrimonio.NomeTipo
            };

            return tipoPatrimonioDto;
        }

        public void Adicionar(CriarTipoPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoPatrimonio TipoExistente = _repository.BuscarPorNome(dto.NomeTipo);

            if (TipoExistente != null)
            {
                throw new DomainException("Já existe esse tipo patrimonio.");
            }

            TipoPatrimonio tipo = new TipoPatrimonio
            {
                NomeTipo = dto.NomeTipo
            };

            _repository.Adicionar(tipo);
        }

        public void Atualizar(Guid tipoUsuarioId, CriarTipoPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoPatrimonio tipoBanco = _repository.BuscarPorId(tipoUsuarioId);

            if (tipoBanco == null)
            {
                throw new DomainException("Tipo de patrimonio não encontrada.");
            }

            TipoPatrimonio tipoExistente = _repository.BuscarPorNome(dto.NomeTipo);

            if (tipoExistente != null)
            {
                throw new DomainException("Já existe um tipo de patrimonio cadastrado com esse nome.");
            }

            tipoBanco.NomeTipo = dto.NomeTipo;

            _repository.Atualizar(tipoBanco);
        }
    }
}
