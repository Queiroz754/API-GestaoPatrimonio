using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;
using Microsoft.Identity.Client;

namespace GerenciamentoPatrimonio.Repository
{
    public class PatrimonioRepository : IPatrimonioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public PatrimonioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Patrimonio> Listar()
        {
            return _context.Patrimonio
                    .OrderBy(patrimonio => patrimonio.Denominacao).ToList();
        }

        public Patrimonio BuscarPorId(Guid patrimonioID)
        {
            return _context.Patrimonio.Find(patrimonioID);
        }

        public Patrimonio BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioId = null)
        {
            var consulta = _context.Patrimonio.AsQueryable();

            if(patrimonioId.HasValue)
            {
                consulta = consulta.Where(patrimonio => patrimonio.PatrimonioID != patrimonioId);
            }

            return consulta.FirstOrDefault(patrimonio => patrimonio.NumeroPatrimonio == numeroPatrimonio);
        }

        public bool LocalizacaoExiste(Guid localizacaoid)
        {
            return _context.Localizacao.Any(local => local.LocalizacaoID == localizacaoid);
        }

        public bool StatusPatrimonioExiste(Guid statusPatrimonioId)
        {
            return _context.StatusPatrimonio.Any(status => status.StatusPatrimonioID == statusPatrimonioId);
        }

        public bool TipoPatrimonioExiste(Guid tipoPatrimonioId)
        {
            return _context.TipoPatrimonio.Any(tipo => tipo.TipoPatrimonioID == tipoPatrimonioId);
        }

        public void Adicionar(Patrimonio patrimonio)
        {
            _context.Patrimonio.Add(patrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(Patrimonio patrimonio)
        {
            if (patrimonio == null)
            {
                return;
            }

            Patrimonio patrimonioBanco = _context.Patrimonio.Find(patrimonio.PatrimonioID);

            if (patrimonioBanco == null)
            {
                return;
            }

            patrimonioBanco.Denominacao = patrimonio.Denominacao;
            patrimonioBanco.NumeroPatrimonio = patrimonio.NumeroPatrimonio;
            patrimonioBanco.Valor = patrimonio.Valor;
            patrimonioBanco.Imagem = patrimonio.Imagem;
            patrimonioBanco.LocalizaocaoID = patrimonio.LocalizaocaoID;
            patrimonioBanco.TipoPatrimonioID = patrimonio.TipoPatrimonioID;

            _context.SaveChanges();
        }

        public void AtualizarStatus(Patrimonio patrimonio)
        {
            if (patrimonio == null)
            {
                return;
            }

            Patrimonio patrimonioBanco = _context.Patrimonio.Find(patrimonio.PatrimonioID);

            if (patrimonioBanco == null)
            {
                return;
            }

            patrimonioBanco.StatusPatrimonioID = patrimonio.StatusPatrimonioID;

            _context.SaveChanges();
        }

    }
}
