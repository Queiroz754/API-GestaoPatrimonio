using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.AreaDto;
using GerenciamentoPatrimonio.DTO.StatusPatrimonioDto;
using GerenciamentoPatrimonio.DTO.TipoUsuarioDto;
using GerenciamentoPatrimonio.Execeptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Service
{
    public class StatusPatrimonioService
    {
        private readonly IStatusPatrimonioRepository _repository;

        public StatusPatrimonioService(IStatusPatrimonioRepository repository)
        {
            _repository = repository;
        }
        public List<ListarStatusPatrimonioDto> Listar()
        {
            List<StatusPatrimonio> status = _repository.Listar();

            List<ListarStatusPatrimonioDto> statusDto = status.Select(s => new
            ListarStatusPatrimonioDto
            {
                StatusPatrimonioID = s.StatusPatrimonioID,
                NomeStatus = s.NomeStatus
            }).ToList();
            return statusDto;
        }

        public ListarStatusPatrimonioDto BuscarPorId(Guid statusId)
        {
            StatusPatrimonio status = _repository.BuscarPorId(statusId);

            if (status == null)
            {
                throw new DomainException("Status de patrimonio não encontrado.");
            }

            ListarStatusPatrimonioDto statusDto = new ListarStatusPatrimonioDto
            {
                NomeStatus = status.NomeStatus,
                StatusPatrimonioID = status.StatusPatrimonioID
            };

            return statusDto;
        }

        public ListarStatusPatrimonioDto BuscarPorNome(string nomeStatus)
        {
            StatusPatrimonio status = _repository.BuscarPorNome(nomeStatus);

            if (nomeStatus == null)
            {
                throw new DomainException("Status de patrimonio não encontrado.");
            }

            ListarStatusPatrimonioDto statusDto = new ListarStatusPatrimonioDto
            {
                NomeStatus = status.NomeStatus   ,
                StatusPatrimonioID = status.StatusPatrimonioID
            };

            return statusDto;
        }

        public void Adicionar(CriarStatusPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusPatrimonio statusExistente = _repository.BuscarPorNome(dto.NomeStatus);

            if (statusExistente != null)
            {
                throw new DomainException("Já existe um status de patrimonio cadastrada com esse nome.");
            }

            StatusPatrimonio status = new StatusPatrimonio
            {
                NomeStatus = dto.NomeStatus
            };

            _repository.Adicionar(status);
        }

        public void Atualizar(Guid areaId, CriarStatusPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusPatrimonio statusBanco = _repository.BuscarPorId(areaId);

            if (statusBanco == null)
            {
                throw new DomainException("Status de patrimonio não encontrado.");
            }

            StatusPatrimonio statusExistente = _repository.BuscarPorNome(dto.NomeStatus);

            if (statusExistente != null)
            {
                throw new DomainException("Já existe um tatus de patrimonio cadastrada com esse nome.");
            }

            statusBanco.NomeStatus = dto.NomeStatus;

            _repository.Atualizar(statusBanco);
        }
    }
}
