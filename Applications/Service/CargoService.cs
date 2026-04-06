using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.AreaDto;
using GerenciamentoPatrimonio.DTO.CargoDto;
using GerenciamentoPatrimonio.DTO.TipoUsuarioDto;
using GerenciamentoPatrimonio.Execeptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Service
{
    public class CargoService
    {
        private readonly ICargoRepository _repository;

        public CargoService(ICargoRepository repository)
        {
            _repository = repository; 
        }

        public List<ListarCargoDto> Listar()
        {
            List<Cargo> cargos = _repository.Listar();

            List<ListarCargoDto> cargoDto = cargos.Select(c => new
            ListarCargoDto
            {
                CargoID = c.CargoID,
                NomeCargo = c.NomeCargo
            }).ToList();
            return cargoDto;
        }

        public ListarCargoDto BuscarPorId(Guid cargoId)
        {
            Cargo cargo = _repository.BuscarPorId(cargoId);

            if (cargo == null)
            {
                throw new DomainException("Cargo não encontrada.");
            }

            ListarCargoDto cargoDto = new ListarCargoDto
            {
                CargoID = cargo.CargoID,
                NomeCargo = cargo.NomeCargo
            };

            return cargoDto;
        }

        public ListarCargoDto BuscarPorNome(string nomeCargo)
        {
            Cargo cargo = _repository.BuscarPorNome(nomeCargo);

            if (cargo == null)
            {
                throw new DomainException("Cargo não encontrada.");
            }

            ListarCargoDto cargoDto = new ListarCargoDto
            {
                CargoID = cargo.CargoID,
                NomeCargo = cargo.NomeCargo
            };

            return cargoDto;
        }

        public void Adicionar(CriarCargoDto dto)
        {
            Validar.ValidarNome(dto.NomeCargo);

            Cargo cargoExistente = _repository.BuscarPorNome(dto.NomeCargo);

            if (cargoExistente != null)
            {
                throw new DomainException("Esse cargo já foi cadastrado.");
            }

            Cargo cargo = new Cargo
            {
                NomeCargo = dto.NomeCargo
            };

            _repository.Adicionar(cargo);
        }

        public void Atualizar(Guid cargoId, CriarCargoDto dto)
        {
            Validar.ValidarNome(dto.NomeCargo);

            Cargo cargoBanco = _repository.BuscarPorId(cargoId);

            if (cargoBanco == null)
            {
                throw new DomainException("Cargo não encontrado.");
            }

            Cargo cargoExistente = _repository.BuscarPorNome(dto.NomeCargo);

            if (cargoExistente != null)
            {
                throw new DomainException("Já existe um cargo cadastrada com esse nome.");
            }

            cargoBanco.NomeCargo = dto.NomeCargo;

            _repository.Atualizar(cargoBanco);
        }
    }
}
