using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.BairroDto;
using GerenciamentoPatrimonio.Execeptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Service
{
    public class BairroService
    {
        private readonly IBairroRepository _repository;

        public BairroService(IBairroRepository repository)
        {
            _repository = repository;
        }

        public List<ListarBairroDto> Listar()
        {
            List<Bairro> bairros = _repository.Listar();

            List<ListarBairroDto> bairroDto = bairros.Select(bairro =>
            new ListarBairroDto
            {
                BairroID = bairro.BairroID,
                NomeBairro = bairro.NomeBairro,
                CidadeID = bairro.CidadeID,
            }).ToList();
                return bairroDto;
        }

        public ListarBairroDto BuscarPorId(Guid bairroId)
        {
            Bairro bairro = _repository.BuscarPorId(bairroId);

            if (bairro == null)
            {
                throw new DomainException("Bairro não encontrado.");
            }

            ListarBairroDto bairroDto = new ListarBairroDto
            {
                BairroID = bairro.BairroID,
                NomeBairro = bairro.NomeBairro,
                CidadeID = bairro.CidadeID
            };
            return bairroDto;
        }

        public void Adicionar(CriarBairroDto dto)
        {
            Validar.ValidarNome(dto.NomeBairro);

            if (!_repository.CidadeExiste(dto.CidadeID))
            {
                throw new DomainException("Cidade não cadastrada.");
            }

            Bairro bairroExiste = _repository.BuscarPorNome(dto.NomeBairro, dto.CidadeID);

            if (bairroExiste != null)
            {
                throw new DomainException("Já existe esse bairro cadastrado nesta cidade.");
            }


            Bairro bairro = new Bairro
            {
                NomeBairro = dto.NomeBairro,
                CidadeID = dto.CidadeID
            };

            _repository.Adicionar(bairro);
        }

        public void Atualizar(Guid bairroId, CriarBairroDto dto)
        {
            Validar.ValidarNome(dto.NomeBairro);

            if (!_repository.CidadeExiste(dto.CidadeID))
            {
                throw new DomainException("Cidade não cadastrada.");
            }

            Bairro bairroBanco = _repository.BuscarPorId(bairroId);

            if (bairroBanco == null)
            {
                throw new DomainException("Bairro não encontrado.");
            }

            Bairro bairroExistente = _repository.BuscarPorNome(dto.NomeBairro, dto.CidadeID);

            if(bairroExistente != null)
            {
                throw new DomainException("Já existe um bairro cadastrado com esse nome nesta cidade.");
            }

            bairroBanco.NomeBairro = dto.NomeBairro;
            bairroBanco.CidadeID = dto.CidadeID;

            _repository.Atualizar(bairroBanco);
        }

        

    }
}
