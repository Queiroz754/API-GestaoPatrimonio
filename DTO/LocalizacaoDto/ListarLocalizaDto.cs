namespace GerenciamentoPatrimonio.DTO.LocalizacaoDto
{
    public class ListarLocalizaDto
    {
        public Guid LocalizacaoID { get; set; }
        public string NomeLocal {  get; set; } = string.Empty;
        public int? LocalSAP { get; set; }
        public string DescricaoSAP { get; set; }
        public Guid AreaID { get; set; }
    }
}
