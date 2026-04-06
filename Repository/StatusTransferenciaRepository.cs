using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repository
{
    public class StatusTransferenciaRepository : IStatusTransferenciaRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public StatusTransferenciaRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<StatusTransferencia> Listar()
        {
            return _context.StatusTransferencia
                    .OrderBy(t => t.NomeStatus).ToList();
        }
        public StatusTransferencia BuscarPorId(Guid transferenciaID)
        {
            return _context.StatusTransferencia.Find(transferenciaID);
        }

        public StatusTransferencia BuscarPorNome(string nomeStatus)
        {
            return _context.StatusTransferencia.FirstOrDefault(nome => nome.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

        public void Adicionar(StatusTransferencia statusTransferencia)
        {
            _context.StatusTransferencia.Add(statusTransferencia);
            _context.SaveChanges();
        }

        public void Atualizar(StatusTransferencia status)
        {
            if (status == null)
            {
                return;
            }

            StatusTransferencia StatusBanco = _context.StatusTransferencia.Find(status.StatusTransferenciaID);

            if (StatusBanco == null)
            {
                return;
            }

            StatusBanco.NomeStatus = status.NomeStatus;

            _context.SaveChanges();
        }
    }
}
