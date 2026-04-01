using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repository
{
    public class BairroRepository : IBairroRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public BairroRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Bairro> Listar()
        {
            return _context.Bairro.OrderBy(bairro => bairro.NomeBairro).ToList();
        }
        public Bairro BuscarPorId(Guid bairroId)
        {
            return _context.Bairro.Find(bairroId);
        }

        public Bairro BuscarPorNome(string nomeBairro, Guid cidadeId)
        {
            return _context.Bairro.FirstOrDefault(bairro => bairro.NomeBairro.ToLower() == nomeBairro.ToLower() && bairro.CidadeID == cidadeId);
        }

        public void Adicionar(Bairro bairro)
        {
            _context.Bairro.Add(bairro);
            _context.SaveChanges();
        }

        public void Atualizar(Bairro bairro)
        {
            if (bairro == null) return;

            Bairro bairroBanco = _context.Bairro.Find(bairro.BairroID);

            if (bairroBanco == null)
            {
                return;
            }

            bairroBanco.NomeBairro = bairro.NomeBairro;
            _context.SaveChanges();
        }

        public bool CidadeExiste(Guid cidadeId)
        {
            return _context.Bairro.Any(cidade => cidade.CidadeID  == cidadeId);
        }
    }
}
