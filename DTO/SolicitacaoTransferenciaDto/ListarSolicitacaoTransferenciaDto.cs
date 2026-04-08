namespace GerenciamentoPatrimonio.DTO.SolicitacaoTransferenciaDto
{
    public class ListarSolicitacaoTransferenciaDto
    {
        public Guid TransferenciaID { get; set; }
        public DateTime DateCriacaoSolicitante { get; set; }
        public DateTime? DataResposta {  get; set; }
        public string Justificativa { get; set; } = string.Empty;
        public Guid StatusTransferenciaId { get; set; }
        public Guid? UsuarioIdSolicitacao { get; set; }
        public Guid UsuarioIdAprovacao { get; set; }
        public Guid PatrimonioId { get; set; }
        public Guid LocalizacaoId { get; set; }
    }
}
