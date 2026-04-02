using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.AreaDto;
using GerenciamentoPatrimonio.DTO.TipoUsuarioDto;
using GerenciamentoPatrimonio.Execeptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Service
{
    public class TipoUsuarioService
    {
        private readonly ITipoUsuarioRepository _repository;

        public TipoUsuarioService(ITipoUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarTipoUsuarioDto> Listar()
        {
            List<TipoUsuario> tipoUsuarios = _repository.Listar();

            List<ListarTipoUsuarioDto> tipoUsuarioDto = tipoUsuarios.Select(tipoUsuario => new
            ListarTipoUsuarioDto
            {
                TipoUsuarioID = tipoUsuario.TipoUsuarioID,
                NomeTipo = tipoUsuario.NomeTipo
            }).ToList();
            return tipoUsuarioDto;
        }

        public ListarTipoUsuarioDto BuscarPorId(Guid tipoUsuarioId)
        {
            TipoUsuario tipoUsuario = _repository.BuscarPorId(tipoUsuarioId);

            if (tipoUsuario == null)
            {
                throw new DomainException("Tipo de usuário não encontrada.");
            }

            ListarTipoUsuarioDto tipoUsuarioDto = new ListarTipoUsuarioDto
            {
                TipoUsuarioID = tipoUsuario.TipoUsuarioID,
                NomeTipo = tipoUsuario.NomeTipo
            };

            return tipoUsuarioDto;
        }

        public ListarTipoUsuarioDto BuscarPorNome(string nomeTipo)
        {
            TipoUsuario tipoUsuario = _repository.BuscarPorNome(nomeTipo);

            if (tipoUsuario == null)
            {
                throw new DomainException("Tipo de usuário não encontrada.");
            }

            ListarTipoUsuarioDto tipoUsuarioDto = new ListarTipoUsuarioDto
            {
                TipoUsuarioID = tipoUsuario.TipoUsuarioID,
                NomeTipo = tipoUsuario.NomeTipo
            };

            return tipoUsuarioDto;
        }

        public void Adicionar(CriarTipoUsuarioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoUsuario TipoExistente = _repository.BuscarPorNome(dto.NomeTipo);

            if (TipoExistente != null)
            {
                throw new DomainException("Já existe esse tipo usuário.");
            }

            TipoUsuario tipo = new TipoUsuario
            {
                NomeTipo = dto.NomeTipo
            };

            _repository.Adicionar(tipo);
        }

        public void Atualizar(Guid tipoUsuarioId, CriarTipoUsuarioDto dto)
        {
            Validar.ValidarNome(dto.NomeTipo);

            TipoUsuario tipoBanco = _repository.BuscarPorId(tipoUsuarioId);

            if (tipoBanco == null)
            {
                throw new DomainException("Tipo de usuario não encontrada.");
            }

            TipoUsuario tipoExistente = _repository.BuscarPorNome(dto.NomeTipo);

            if (tipoExistente != null)
            {
                throw new DomainException("Já existe um tipo de usuário cadastrado com esse nome.");
            }

            tipoBanco.NomeTipo = dto.NomeTipo;

            _repository.Atualizar(tipoBanco);
        }
    }
}
