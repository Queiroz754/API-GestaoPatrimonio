using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.PatrimonioDto;
using GerenciamentoPatrimonio.Execeptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Service
{
    public class PatrimonioService
    {
        private readonly IPatrimonioRepository _repository;

        public PatrimonioService(IPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarPatrimonioDto> Listar()
        {
            List<Patrimonio> patrimonios = _repository.Listar();
            List<ListarPatrimonioDto> patrimoniosDto = patrimonios.Select(p => new ListarPatrimonioDto
            {
                PatrimonioId = p.PatrimonioID,
                Denominacao = p.Denominacao,
                NumeroPatrimonio = p.NumeroPatrimonio,
                Valor = p.Valor,
                Imagem = p.Imagem,
                LocalID = p.LocalizaocaoID,
                TipoPatrimonioID = p.TipoPatrimonioID,
                StatusPatrimonioID = p.StatusPatrimonioID
            }).ToList();

            return patrimoniosDto;
        }

        public ListarPatrimonioDto BuscarPorId(Guid id)
        {
            Patrimonio patrimonio = _repository.BuscarPorId(id);

            return new ListarPatrimonioDto
            {
                PatrimonioId = patrimonio.PatrimonioID,
                Denominacao = patrimonio.Denominacao,
                NumeroPatrimonio = patrimonio.NumeroPatrimonio,
                Valor = patrimonio.Valor,
                Imagem = patrimonio.Imagem,
                LocalID = patrimonio.LocalizaocaoID,
                TipoPatrimonioID = patrimonio.TipoPatrimonioID,
                StatusPatrimonioID = patrimonio.StatusPatrimonioID
            };
        }

        public void Adicionar (CriarPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.Denominacao);

            Patrimonio patrimonioExistente = _repository.BuscarPorNumeroPatrimonio(dto.NumeroPatrimonio);

            
            if (patrimonioExistente != null)
            {
                throw new DomainException("Já existe um patrimonio cadastrada com esse nome.");
            }

            if(!_repository.LocalizacaoExiste(dto.LocalizacaoID))
            {
                throw new DomainException("Localização não cadastrada no sistema");
            }

            if (!_repository.TipoPatrimonioExiste(dto.TipoPatrimonioID))
            {
                throw new DomainException("Tipo patrimonio não cadastrada no sistema");
            }

            if (!_repository.StatusPatrimonioExiste(dto.StatusPatrimonioID))
            {
                throw new DomainException("Status de patrimonio não cadastrada no sistema");
            }

            Patrimonio patrimonio  = new Patrimonio
            {
                Denominacao = dto.Denominacao,
                Imagem = dto.Imagem,
                NumeroPatrimonio = dto.NumeroPatrimonio,
                Valor = dto.Valor,
                LocalizaocaoID = dto.LocalizacaoID,
                TipoPatrimonioID = dto.TipoPatrimonioID,
                StatusPatrimonioID = dto.StatusPatrimonioID
            };

            _repository.Adicionar(patrimonio);
        }

        public void Atualizar(Guid id, CriarPatrimonioDto dto)
        {
            Validar.ValidarNome(dto.Denominacao);

            Patrimonio patrimonioBanco = _repository.BuscarPorId(id);

            if (patrimonioBanco == null)
            {
                throw new DomainException("Patrimonio não encontrado.");
            }

            Patrimonio patrimonioExiste = _repository.BuscarPorNumeroPatrimonio(dto.NumeroPatrimonio);

            if (patrimonioExiste != null)
            {
                throw new DomainException("Esse Patrimonio já está cadastrado.");
            }

            if (!_repository.LocalizacaoExiste(dto.LocalizacaoID))
            {
                throw new DomainException("Localização não cadastrada no sistema");
            }

            if (!_repository.TipoPatrimonioExiste(dto.TipoPatrimonioID))
            {
                throw new DomainException("Tipo patrimonio não cadastrada no sistema");
            }

            if (!_repository.StatusPatrimonioExiste(dto.StatusPatrimonioID))
            {
                throw new DomainException("Status de patrimonio não cadastrada no sistema");
            }

            patrimonioBanco.Denominacao = dto.Denominacao;
            patrimonioBanco.NumeroPatrimonio = dto.NumeroPatrimonio;
            patrimonioBanco.Valor = dto.Valor;
            patrimonioBanco.Imagem = dto.Imagem;

            _repository.Atualizar(patrimonioBanco);
        }

    }
}
