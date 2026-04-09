using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.SolicitacaoTransferenciaDto;
using GerenciamentoPatrimonio.Execeptions;
using GerenciamentoPatrimonio.Interfaces;
using Microsoft.Identity.Client;

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

        public void Adicionar(Guid usuarioId, CriarSolicitacaoTransferenciaDto dto)
        {
            Validar.ValidarJustificativa(dto.Justificativa);

            Usuario usuario = _usuarioRepository.BuscarPorId(usuarioId);

            if(usuario == null)
            {
                throw new DomainException("Usuário não encontrado");
            }

            Patrimonio patrimonio = _repository.BuscarPatrimonioParId
                (dto.PatrimonioId);

            if(usuario == null)
            {
                throw new DomainException("Patrimonio não encontrado.");
            }

            if(!_repository.LocalizacaoExiste(dto.LocalizacaoId))
            {
                throw new DomainException("Localização de destino não existe.");
            }

            if(patrimonio.LocalizaocaoID == dto.LocalizacaoId)
            {
                throw new DomainException("O patrimonio já está nessa localização.");
            }

            if(_repository.ExisteSolicitacaoPendente(dto.PatrimonioId))
            {
                throw new DomainException("Já existe uma solicitação pandente para esse patrimônio.");
            }

            if (usuario.TipoUsuario.NomeTipo == "Responsável")
            {
                bool usuarioResponsavel = _repository.UsuarioResponsavelDaLocalizacao
                    (usuarioId, patrimonio.LocalizaocaoID);

                if (usuarioResponsavel)
                {
                    throw new DomainException("O responsável só pode solicitar transferência de patrimonio do ambiente ao qual está vinculado.");
                }

                StatusTransferencia statusPendente =
                    _repository.BuscarStatusTransferenciaPorNome("Pendente de aprovação");

                if (statusPendente == null)
                {
                    throw new DomainException("Status de transferência pendente não encontrado.");
                }

                SolicitacaoTransferencia solicitacao = new SolicitacaoTransferencia
                {
                    DataCriacaoSolicitante = DateTime.Now,
                    Justificativa = dto.Justificativa,
                    StatusTransferenciaID = statusPendente.StatusTransferenciaID,
                    UsuarioIDSolicitacao = usuarioId,
                    UsuarioIDAprovacao = null,
                    PatrimonioID = dto.PatrimonioId,
                    LocalizacaoID = dto.LocalizacaoId
                };

                _repository.Adicionar(solicitacao);
            }

        }
        public void Responder(Guid transferenciaId, Guid usuarioId,ResponderSolicitacaoTransferenciaDto dto)
        {
            Usuario usuario = _usuarioRepository.BuscarPorId(usuarioId);

            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            SolicitacaoTransferencia solicitacao = _repository.BuscarPorId(transferenciaId);

            if(solicitacao == null)
            {
                throw new DomainException("Solicitação de transferencia não encontrado.");
            }

            Patrimonio patrimonio = _repository.BuscarPatrimonioParId
                (solicitacao.PatrimonioID);

            if(patrimonio == null)
            {
                throw new DomainException("Parimonio não encontrado.");
            }

            StatusTransferencia statusPendente = _repository.BuscarStatusTransferenciaPorNome("Pendente de aprovação");

            if(statusPendente == null)
            {
                throw new DomainException("Status pendente não encontrado"); 
            }

            if(solicitacao.StatusTransferenciaID != statusPendente.StatusTransferenciaID)
            {
                throw new DomainException("Essa solicitação já foi respondida");
            }

            if(usuario.TipoUsuario.NomeTipo == "Responsável")
            {
                bool usuarioResponsavel = _repository.UsuarioResponsavelDaLocalizacao(usuarioId, patrimonio.LocalizaocaoID);
            }

            StatusTransferencia statusResposta;

            if(dto.Aprovado)
            {
                statusResposta = _repository.BuscarStatusTransferenciaPorNome("Aprovado");
            }
            else
            {
                statusResposta = _repository.BuscarStatusTransferenciaPorNome("Recusado");
            }

            if(dto.Aprovado)
            {
                throw new DomainException("Status resposta da transferencia não encontrado");
            }

            solicitacao.StatusTransferenciaID = statusResposta.StatusTransferenciaID;
            solicitacao.UsuarioIDAprovacao = usuarioId;
            solicitacao.DataResposta = DateTime.Now;

            _repository.Atualizar(solicitacao);

            if(dto.Aprovado)
            {
                StatusPatrimonio statusTransferencia = _repository.BuscarStatusPatrimonioPorNome("Transferido");

                if(statusTransferencia == null)
                {
                    throw new DomainException("Status de patrimonio 'transferido' não encontrado");
                }

                TipoAlteracao tipoAlteracao = _repository.BuscarTipoAlteracaoPorNome("Transferência");

                if(tipoAlteracao == null)
                {
                    throw new DomainException("Tipo alteração 'transferêcia' não encontrado.");
                }

                patrimonio.LocalizaocaoID = solicitacao.LocalizacaoID;
                patrimonio.StatusPatrimonioID = statusTransferencia.StatusPatrimonioID;

                _repository.AtualizarPatrimonio(patrimonio);

                LogPatrimonio log = new LogPatrimonio
                {
                    DataTrasnferencia = DateTime.Now,
                    TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                    StatusPatrimonioID = statusTransferencia.StatusPatrimonioID,
                    PatrimonioID = patrimonio.PatrimonioID,
                    UsuarioID = usuarioId,
                    LocalizacaoID = patrimonio.LocalizaocaoID
                }; 
                _repository.AdicionarLog(log);
            }
        }
    }
}