using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.StatusPatrimonioDto;
using GerenciamentoPatrimonio.DTO.StatusTransferenciaDto;
using GerenciamentoPatrimonio.Execeptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Service
{
    public class StatusTransferenciaService
    {
        private readonly IStatusTransferenciaRepository _repository;

        public StatusTransferenciaService(IStatusTransferenciaRepository repository)
        {
            _repository = repository;
        }

        public List<ListarStatusTransferenciaDto> Listar()
        {
            List<StatusTransferencia> status = _repository.Listar();

            List<ListarStatusTransferenciaDto> statusDto = status.Select(s => new
            ListarStatusTransferenciaDto
            {
                StatusTransferenciaID = s.StatusTransferenciaID,
                NomeStatus = s.NomeStatus
            }).ToList();
            return statusDto;
        }

        public ListarStatusTransferenciaDto BuscarPorId(Guid statusId)
        {
            StatusTransferencia status = _repository.BuscarPorId(statusId);

            if (status == null)
            {
                throw new DomainException("Status de transferencia não encontrada.");
            }

            ListarStatusTransferenciaDto statusDto = new ListarStatusTransferenciaDto
            {
                NomeStatus = status.NomeStatus,
                StatusTransferenciaID = status.StatusTransferenciaID
            };

            return statusDto;
        }

        public ListarStatusTransferenciaDto BuscarPorNome(string nomeStatus)
        {
            StatusTransferencia status = _repository.BuscarPorNome(nomeStatus);

            if (nomeStatus == null)
            {
                throw new DomainException("Status de transferencia não encontrada.");
            }

            ListarStatusTransferenciaDto statusDto = new ListarStatusTransferenciaDto
            {
                NomeStatus = status.NomeStatus,
                StatusTransferenciaID = status.StatusTransferenciaID
            };

            return statusDto;
        }

        public void Adicionar(CriarStatusTransferenciaDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusTransferencia statusExistente = _repository.BuscarPorNome(dto.NomeStatus);

            if (statusExistente != null)
            {
                throw new DomainException("Já existe um status de transferencia cadastrada com esse nome.");
            }

            StatusTransferencia status = new StatusTransferencia
            {
                NomeStatus = dto.NomeStatus
            };

            _repository.Adicionar(status);
        }

        public void Atualizar(Guid areaId, CriarStatusTransferenciaDto dto)
        {
            Validar.ValidarNome(dto.NomeStatus);

            StatusTransferencia statusBanco = _repository.BuscarPorId(areaId);

            if (statusBanco == null)
            {
                throw new DomainException("Status de transferencia não encontrada.");
            }

            StatusTransferencia statusExistente = _repository.BuscarPorNome(dto.NomeStatus);

            if (statusExistente != null)
            {
                throw new DomainException("Já existe um tatus de patrimonio cadastrada com esse nome.");
            }

            statusBanco.NomeStatus = dto.NomeStatus;

            _repository.Atualizar(statusBanco);
        }
    }
 
}
