using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.DTO.BairroDto
{
    public class ListarBairroDto
    {
        public Guid BairroID { get; set; }
        public string NomeBairro { get; set; } = null!;
        public Guid CidadeID { get; set; }
    }
}
