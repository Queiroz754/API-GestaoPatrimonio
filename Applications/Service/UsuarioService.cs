using GerenciamentoPatrimonio.Applications.Autenticacao;
using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.UsuarioDto;
using GerenciamentoPatrimonio.Execeptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Service
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<ListarUsuarioDto> usuariosDto = usuarios.Select(usuario => new ListarUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                NIF = usuario.NIF,
                Nome = usuario.NOME,
                RG = usuario.RG,
                CPF = usuario.CPF,
                CarteiraTrabalho = usuario.CarteiraTrabalho,
                Email = usuario.Email,
                Ativo = usuario.Ativo,
                PrimeiroAcesso = usuario.PrimeiroAcesso,
                EnderecoID = usuario.EnderecoID,
                CargoID = usuario.CargoID,
                TipoUsuarioID = usuario.TipoUsuarioId
            }).ToList();

            return usuariosDto;
        }

        public ListarUsuarioDto BuscarPorId(Guid usuarioId)
        {
            Usuario usuario = _repository.BuscarPorId(usuarioId);

            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            ListarUsuarioDto usuarioDto = new ListarUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                NIF = usuario.NIF,
                Nome = usuario.NOME,
                RG = usuario.RG,
                CPF = usuario.CPF,
                CarteiraTrabalho = usuario.CarteiraTrabalho,
                Email = usuario.Email,
                Ativo = usuario.Ativo,
                PrimeiroAcesso = usuario.PrimeiroAcesso,
                EnderecoID = usuario.EnderecoID,
                CargoID = usuario.CargoID,
                TipoUsuarioID = usuario.TipoUsuarioId
            };

            return usuarioDto;
        }

        public void Adicionar(CriarUsuarioDto dto)
        {
            Validar.ValidarNome(dto.Nome);
            Validar.ValidarNIF(dto.NIF);
            Validar.ValidarCPF(dto.CPF);
            Validar.ValidarEmail(dto.Email);

            Usuario usuarioDuplicado = _repository.BuscarDuplicado(dto.NIF, dto.CPF, dto.Email);

            if (usuarioDuplicado != null)
            {
                if (usuarioDuplicado.NIF == dto.NIF)
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse NIF.");
                }

                if (usuarioDuplicado.CPF == dto.CPF)
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse CPF.");
                }

                if (usuarioDuplicado.Email.ToLower() == dto.Email.ToLower())
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse e-mail.");
                }
            }

            if (!_repository.EnderecoExiste(dto.EnderecoID))
            {
                throw new DomainException("Endereço informado não existe.");
            }

            if (!_repository.CargoExiste(dto.CargoID))
            {
                throw new DomainException("Cargo informado não existe.");
            }

            if (!_repository.TipoUsuarioExiste(dto.TipoUsuarioID))
            {
                throw new DomainException("Tipo de usuário informado não existe.");
            }

            Usuario usuario = new Usuario
            {
                NIF = dto.NIF,
                NOME = dto.Nome,
                RG = dto.RG,
                CPF = dto.CPF,
                CarteiraTrabalho = dto.CarteiraTrabalho,
                Senha = CriptografiaUsuario.CriptografarSenha(dto.NIF),
                Email = dto.Email,
                Ativo = true,
                PrimeiroAcesso = true,
                EnderecoID = dto.EnderecoID,
                CargoID = dto.CargoID,
                TipoUsuarioId = dto.TipoUsuarioID
            };

            _repository.Adicionar(usuario);
        }

        public void Atualizar(Guid usuarioId, CriarUsuarioDto dto)
        {
            Validar.ValidarNome(dto.Nome);
            Validar.ValidarNIF(dto.NIF);
            Validar.ValidarCPF(dto.CPF);
            Validar.ValidarEmail(dto.Email);

            Usuario usuarioBanco = _repository.BuscarPorId(usuarioId);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            Usuario usuarioDuplicado = _repository.BuscarDuplicado(dto.NIF, dto.CPF, dto.Email, usuarioId);

            if (usuarioDuplicado != null)
            {
                if (usuarioDuplicado.NIF == dto.NIF)
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse NIF.");
                }

                if (usuarioDuplicado.CPF == dto.CPF)
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse CPF.");
                }

                if (usuarioDuplicado.Email.ToLower() == dto.Email.ToLower())
                {
                    throw new DomainException("Já existe um usuário cadastrado com esse e-mail.");
                }
            }

            if (!_repository.EnderecoExiste(dto.EnderecoID))
            {
                throw new DomainException("Endereço informado não existe.");
            }

            if (!_repository.CargoExiste(dto.CargoID))
            {
                throw new DomainException("Cargo informado não existe.");
            }

            if (!_repository.TipoUsuarioExiste(dto.TipoUsuarioID))
            {
                throw new DomainException("Tipo de usuário informado não existe.");
            }

            usuarioBanco.NIF = dto.NIF;
            usuarioBanco.NOME = dto.Nome;
            usuarioBanco.RG = dto.RG;
            usuarioBanco.CPF = dto.CPF;
            usuarioBanco.CarteiraTrabalho = dto.CarteiraTrabalho;
            usuarioBanco.Email = dto.Email;
            usuarioBanco.EnderecoID = dto.EnderecoID;
            usuarioBanco.CargoID = dto.CargoID;
            usuarioBanco.TipoUsuarioId = dto.TipoUsuarioID;

            _repository.Atualizar(usuarioBanco);
        }

        public void AtualizarStatus(Guid usuarioId, AtualizarStatusUsuarioDto dto)
        {
            Usuario usuarioBanco = _repository.BuscarPorId(usuarioId);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            usuarioBanco.Ativo = dto.Ativo;
            _repository.AtualizarStatus(usuarioBanco);
        }
    }
}
