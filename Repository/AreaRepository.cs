using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repository
{
    public class AreaRepository : IAreaRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public AreaRepository(GestaoPatrimoniosContext context  )
        {
            _context = context;
        }

        public List<Area> Listar()
        {
            return _context.Area.OrderBy(area => area.NomeArea).ToList();
        }
    }
}
