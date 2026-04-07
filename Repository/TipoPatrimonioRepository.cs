using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repository
{
    public class TipoPatrimonioRepository : ITipoPatrimonioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public TipoPatrimonioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<TipoPatrimonio> Listar()
        {
            return _context.TipoPatrimonio.OrderBy(tipo => tipo.NomeTipo).ToList();
        }

        public TipoPatrimonio BuscarPorId(Guid tipoId)
        {
            return _context.TipoPatrimonio.Find(tipoId);
        }

        public TipoPatrimonio BuscarPorNome(string nomeTipo)
        {
            return _context.TipoPatrimonio.FirstOrDefault(tipo => tipo.NomeTipo.ToLower() == nomeTipo.ToLower());
        }

        public void Adicionar(TipoPatrimonio tipo)
        {
            _context.TipoPatrimonio.Add(tipo);
            _context.SaveChanges();
        }

        public void Atualizar(TipoPatrimonio tipo)
        {
            if (tipo == null)
            {
                return;
            }

            TipoPatrimonio tipoBanco = _context.TipoPatrimonio.Find(tipo.TipoPatrimonioID);

            if (tipoBanco == null)
            {
                return;
            }

            tipoBanco.NomeTipo = tipo.NomeTipo;
            _context.SaveChanges();
        }
    }
}
