using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repository
{
    public class StatusPatrimonioRepository : IStatusPatrimonioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public StatusPatrimonioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<StatusPatrimonio> Listar()
        {
            return _context.StatusPatrimonio
                    .OrderBy(patrimonio => patrimonio.NomeStatus).ToList();
        }

        public StatusPatrimonio BuscarPorId(Guid patrimonioID)
        {
            return _context.StatusPatrimonio.Find(patrimonioID);
        }

        public StatusPatrimonio BuscarPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(nome => nome.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

        public void Adicionar(StatusPatrimonio statusPatrimonio)
        {
            _context.StatusPatrimonio.Add(statusPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(StatusPatrimonio status)
        {
            if (status == null)
            {
                return;
            }

            StatusPatrimonio StatusBanco = _context.StatusPatrimonio.Find(status.StatusPatrimonioID);

            if (StatusBanco == null)
            {
                return;
            }

            StatusBanco.NomeStatus = status.NomeStatus;

            _context.SaveChanges();
        }
    }
}
