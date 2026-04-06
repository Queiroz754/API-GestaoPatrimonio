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

        public void Atualizar(Patrimonio dto, Guid id)
        {
            Validar.ValidarNome(dto.Denominacao);

            Patrimonio patrimonioBanco = _repository.BuscarPorId(id);

            if (patrimonioBanco == null)
            {
               
            }
        }

    }
}
