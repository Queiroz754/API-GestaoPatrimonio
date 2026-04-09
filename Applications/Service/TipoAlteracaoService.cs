using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.TipoAlteracaoDto;
using GerenciamentoPatrimonio.Execeptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Service
{
    public class TipoAlteracaoService
    {
        private readonly ITipoAlteracaoRepository _repository;

        public TipoAlteracaoService(ITipoAlteracaoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoAlteracaoDto> Listar()
        {
            List<TipoAlteracao> tipos = _repository.Listar();

            List<ListarTipoAlteracaoDto> tipoDto = tipos.Select(tipo => new
            ListarTipoAlteracaoDto
            {
                TipoAlteracaoID = tipo.TipoAlteracaoID,
                NomeTipo = tipo.NomeTipo
            }).ToList();
            return tipoDto;
        }

        public ListarTipoAlteracaoDto BuscarPorId(Guid tipoAlteracaoId)
        {
            TipoAlteracao tipoAlteracao = _repository.BuscarPorId(tipoAlteracaoId);

            if (tipoAlteracao == null)
            {
                throw new DomainException("Tipo de Alteração não encontrada.");
            }

            ListarTipoAlteracaoDto tipoAlteracaoDto = new ListarTipoAlteracaoDto
            {
                TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                NomeTipo = tipoAlteracao.NomeTipo
            };

            return tipoAlteracaoDto;
        }

        public ListarTipoAlteracaoDto BuscarPorNome(string nomeTipo)
        {
            TipoAlteracao tipoAlteracao = _repository.BuscarPorNome(nomeTipo);

            if (tipoAlteracao == null)
            {
                throw new DomainException("Tipo de Alteração não encontrado.");
            }

            ListarTipoAlteracaoDto tipoAlteracaoDto = new ListarTipoAlteracaoDto
            {
                TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                NomeTipo = tipoAlteracao.NomeTipo
            };

            return tipoAlteracaoDto;
        }

        public void Adicionar(CriarTipoAlteracaoDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoAlteracao TipoExistente = _repository.BuscarPorNome(dto.NomeTipo);

            if (TipoExistente != null)
            {
                throw new DomainException("Já existe esse tipo Alteração.");
            }

            TipoAlteracao tipo = new TipoAlteracao
            {
                NomeTipo = dto.NomeTipo
            };

            _repository.Adicionar(tipo);
        }

        public void Atualizar(Guid tipoAlteracaoId, CriarTipoAlteracaoDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoAlteracao tipoBanco = _repository.BuscarPorId(tipoAlteracaoId);

            if (tipoBanco == null)
            {
                throw new DomainException("Tipo de alteração não encontrada.");
            }

            TipoAlteracao tipoExistente = _repository.BuscarPorNome(dto.NomeTipo);

            if (tipoExistente != null)
            {
                throw new DomainException("Já existe um tipo de alteração cadastrado com esse nome.");
            }

            tipoBanco.NomeTipo = dto.NomeTipo;

            _repository.Atualizar(tipoBanco);
        }
    }
}
