using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repository
{
    public class TipoAlteracaoRepository : ITipoAlteracaoRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public TipoAlteracaoRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<TipoAlteracao> Listar()
        {
            return _context.TipoAlteracao.OrderBy(tipo => tipo.NomeTipo).ToList();
        }

        public TipoAlteracao BuscarPorId(Guid tipoId)
        {
            return _context.TipoAlteracao.Find(tipoId);
        }

        public TipoAlteracao BuscarPorNome(string nomeTipo)
        {
            return _context.TipoAlteracao.FirstOrDefault(tipo => tipo.NomeTipo.ToLower() == nomeTipo.ToLower());
        }

        public void Adicionar(TipoAlteracao tipo)
        {
            _context.TipoAlteracao.Add(tipo);
            _context.SaveChanges();
        }

        public void Atualizar(TipoAlteracao tipo)
        {
            if (tipo == null)
            {
                return;
            }

            TipoAlteracao tipoBanco = _context.TipoAlteracao.Find(tipo.TipoAlteracaoID);

            if (tipoBanco == null)
            {
                return;
            }

            tipoBanco.NomeTipo = tipo.NomeTipo;
            _context.SaveChanges();
        }
    }
}
