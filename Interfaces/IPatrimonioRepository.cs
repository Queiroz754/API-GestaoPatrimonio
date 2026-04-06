using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface IPatrimonioRepository
    {
        List<Patrimonio> Listar();
        Patrimonio BuscarPorId(Guid pareimonioId);
        Patrimonio BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioId = null);
        bool LocalizacaoExiste(Guid localizacaoid);
        bool TipoPatrimonioExiste(Guid patrimonioId);
        bool StatusPatrimonioExiste(Guid statusPatrimoioId);

        void Adicionar(Patrimonio patrimonio);
        void Atualizar(Patrimonio patrimonio, Guid id);
        void AtualizarStatus(Patrimonio patrimonio);
    }
}
