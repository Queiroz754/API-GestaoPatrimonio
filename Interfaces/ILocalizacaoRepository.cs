using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface ILocalizacaoRepository
    {
        List<Localizacao> Listar();
        Localizacao BuscarPorID(Guid localizacaoID);
        void Adicionar(Localizacao localizacao);
        bool AreaExiste(Guid areaid);
        void Atualizar(Localizacao localizacao);
    }
}
