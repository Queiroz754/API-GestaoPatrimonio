namespace GerenciamentoPatrimonio.DTO.SolicitacaoTransferenciaDto
{
    public class CriarSolicitacaoTransferenciaDto
    {
        public string Justificativa { get; set; }
        Guid PatrimonioId { get; set; }
        Guid LocalId { get; set; }
    }
}
