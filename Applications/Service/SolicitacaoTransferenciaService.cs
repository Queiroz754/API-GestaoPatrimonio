using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.SolicitacaoTransferenciaDto;
using GerenciamentoPatrimonio.Execeptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class SolicitacaoTransferenciaService
    {
        private readonly ISolicitacaoTransferenciaRepository _repository;

        private readonly IUsuarioRepository _usuarioRepository;

        public SolicitacaoTransferenciaService(ISolicitacaoTransferenciaRepository repository, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        public List<ListarSolicitacaoTransferenciaDto> Listar()
        {
            List<SolicitacaoTransferencia> solicitacoes = _repository.Listar();
            List<ListarSolicitacaoTransferenciaDto> solicitacoesDTO = solicitacoes.Select(solicitacao => new ListarSolicitacaoTransferenciaDto
            {
                TransferenciaID = solicitacao.TransferenciaID,
                DateCriacaoSolicitante = solicitacao.DataCriacaoSolicitante,
                DataResposta = solicitacao.DataResposta,
                Justificativa = solicitacao.Justificativa,
                StatusTransferenciaId = solicitacao.StatusTransferenciaID,
                UsuarioIdSolicitacao = solicitacao.UsuarioIDSolicitacao,
                UsuarioIdAprovacao = solicitacao.UsuarioIDAprovacao,
                PatrimonioId = solicitacao.PatrimonioID,
                LocalizacaoId = solicitacao.LocalizacaoID
            }).ToList();

            return solicitacoesDTO;
        }

        public ListarSolicitacaoTransferenciaDto BuscarPorId(Guid transferenciaId)
        {
            SolicitacaoTransferencia solicitacao = _repository.BuscarPorId(transferenciaId);

            if (solicitacao == null)
            {
                throw new DomainException("Solicitação de transferência não encontrada.");
            }

            ListarSolicitacaoTransferenciaDto solicitacaoDto = new ListarSolicitacaoTransferenciaDto
            {
                TransferenciaID = solicitacao.TransferenciaID,
                DateCriacaoSolicitante = solicitacao.DataCriacaoSolicitante,
                DataResposta = solicitacao.DataResposta,
                Justificativa = solicitacao.Justificativa,
                StatusTransferenciaId = solicitacao.StatusTransferenciaID,
                UsuarioIdSolicitacao = solicitacao.UsuarioIDSolicitacao,
                UsuarioIdAprovacao = solicitacao.UsuarioIDAprovacao,
                PatrimonioId = solicitacao.PatrimonioID,
                LocalizacaoId = solicitacao.LocalizacaoID
            };

            return solicitacaoDto;
        }
    }
}