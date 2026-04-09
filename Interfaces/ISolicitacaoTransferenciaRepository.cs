using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface ISolicitacaoTransferenciaRepository
    {
        List<SolicitacaoTransferencia> Listar();
        SolicitacaoTransferencia BuscarPorId(Guid id);
        bool ExisteSolicitacaoPendente(Guid patrimonioID);
        bool UsuarioResponsavelDaLocalizacao(Guid usuarioID, Guid localizacaoID);
        StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus);
        void Adicionar(SolicitacaoTransferencia solicitacaoTransferencia);
        bool LocalizacaoExiste(Guid localizacaoId);
        Patrimonio BuscarPatrimonioParId(Guid patrimonioId);
        TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo);
        StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus);
        void Atualizar(SolicitacaoTransferencia solicitacaoTransferencia);
        void AtualizarPatrimonio(Patrimonio patrimonio);
        void AdicionarLog(LogPatrimonio logPatrimonio);


    }
}
