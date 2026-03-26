using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.LocalizacaoDto;
using GerenciamentoPatrimonio.Execeptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Service
{
    public class LocalizacaoService
    {
        private readonly ILocalizacaoRepository _repository;

        public LocalizacaoService(ILocalizacaoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarLocalizaDto> Listar()
        {
            List<Localizacao> localizacaos = _repository.Listar();

            List<ListarLocalizaDto> localizaDtos = localizacaos.Select(localizacaos => new ListarLocalizaDto
            {
                LocalizacaoID = localizacaos.LocalizacaoID,
                NomeLocal = localizacaos.NomeLocal,
                LocalSAP = localizacaos.LocalSAP,
                DescricaoSAP = localizacaos.DescricaoSAP,
                AreaID = localizacaos.AreaID
            }).ToList();

            return localizaDtos;
        }

        public ListarLocalizaDto BuscarPorId(Guid localizacaoID)
        {
            Localizacao localizacao = _repository.BuscarPorID(localizacaoID);

            if(localizacao == null)
            {
                throw new DomainException("Localização não encontrada.");
            }

            return new ListarLocalizaDto
            {
                LocalizacaoID = localizacao.LocalizacaoID,
                NomeLocal = localizacao.NomeLocal,
                LocalSAP = localizacao.LocalSAP,
                DescricaoSAP = localizacao.DescricaoSAP,
                AreaID = localizacao.AreaID
            };
        }

        public void Adicionar(CriarLocalizacaoDto dto)
        {
            Validar.ValidarNome(dto.NomeLocal);

            if(!_repository.AreaExiste(dto.AreaID))
            {
                throw new DomainException("Área informada nãoo existe.");
            }


            Localizacao localizacao = new Localizacao
            {
                NomeLocal = dto.NomeLocal,
                LocalSAP = dto.LocalSAP,
                DescricaoSAP = dto.DescricaoSAP,
                AreaID = dto.AreaID
            };

            _repository.Adicionar(localizacao);
        }

        public void Atualizar(Guid localizacaoId,CriarLocalizacaoDto dto)
        {
            Validar.ValidarNome(dto.NomeLocal);

            Localizacao localizacaoBanco = _repository.BuscarPorID(localizacaoId);

            if(localizacaoBanco == null)
            {
                throw new DomainException("Localização não encontrada.");
            }

            if(!_repository.AreaExiste(dto.AreaID))
            {
                throw new DomainException("Área informada não existe.");
            }

            localizacaoBanco.NomeLocal = dto.NomeLocal;
            localizacaoBanco.LocalSAP = dto.LocalSAP;
            localizacaoBanco.DescricaoSAP = dto.DescricaoSAP;
            localizacaoBanco.AreaID = dto.AreaID;

            _repository.Atualizar(localizacaoBanco);
        }
    }
}
