using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface ICargoRepository
    {
        List<Cargo> Listar();
        Cargo BuscarPorId(Guid cargoID);
        Cargo BuscarPorNome(string nomeCargo);
        void Adicionar(Cargo cargo);
        void Atualizar(Cargo cargo);
    }
}