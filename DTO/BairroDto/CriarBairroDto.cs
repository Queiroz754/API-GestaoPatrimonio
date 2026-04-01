using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.DTO.BairroDto
{
    public class CriarBairroDto
    {
        public string NomeBairro { get; set; } = null!;
        public Guid  CidadeID { get; set; }
    }
}
