using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public UsuarioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.OrderBy(usuario =>  usuario.NOME).ToList();
        }

        public Usuario BuscarPorId(Guid usuarioId)
        {
            return _context.Usuario.Find(usuarioId);
        }

        public Usuario BuscarDuplicado(string nif, string cpf, string email, Guid? usuarioId = null)
        {
            var consulta = _context.Usuario.AsQueryable();

            if (usuarioId.HasValue)
            {
                consulta = consulta.Where(usuario => usuario.UsuarioID != usuarioId.Value);
            }

            return consulta.FirstOrDefault(usuario =>)
        }
    }
}
