using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repository
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public CidadeRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Cidade> Listar()
        {
            return _context.Cidade.OrderBy(Cidade => Cidade.NomeCidade).ToList();
        }

        public Cidade BuscarPorId(Guid cidadeID)
        {
            return _context.Cidade.Find(cidadeID);
        }

        public Cidade BuscarPorNomeEstado(string nomeCidade, string nomeEstado)
        {
            return _context.Cidade.FirstOrDefault(cidadeBanco => cidadeBanco.Estado.ToLower() == nomeEstado.ToLower() 
                    && cidadeBanco.NomeCidade.ToLower() == nomeCidade.ToLower());
        }

        public void Adicionar(Cidade cidade)
        {
            _context.Cidade.Add(cidade);
            _context.SaveChanges();
        }

        public void Atualizar(Cidade cidade)
        {
            if (cidade == null)
            {
                return;
            }

            Cidade cidadeBanco = _context.Cidade.Find(cidade.CidadeID);

            if (cidadeBanco == null)
            {
                return;
            }

            cidadeBanco.NomeCidade = cidadeBanco.NomeCidade;
            _context.SaveChanges();
        }

    }
}
