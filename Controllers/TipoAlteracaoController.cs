using GerenciamentoPatrimonio.Applications.Service;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTO.TipoAlteracaoDto;
using GerenciamentoPatrimonio.DTO.TipoPatrimonioDto;
using GerenciamentoPatrimonio.Execeptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAlteracaoController : ControllerBase
    {
        private readonly TipoAlteracaoService _service;

        public TipoAlteracaoController(TipoAlteracaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarTipoAlteracaoDto>> Listar()
        {
            List<ListarTipoAlteracaoDto> tipos = _service.Listar();
            return tipos;
        }

        [HttpGet("{id}")]
        public ActionResult<ListarTipoAlteracaoDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarTipoAlteracaoDto tipo = _service.BuscarPorId(id);
                return Ok(tipo);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarTipoAlteracaoDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarTipoAlteracaoDto dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
