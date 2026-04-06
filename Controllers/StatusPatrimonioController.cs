using GerenciamentoPatrimonio.Applications.Service;
using GerenciamentoPatrimonio.DTO.StatusPatrimonioDto;
using GerenciamentoPatrimonio.DTO.TipoUsuarioDto;
using GerenciamentoPatrimonio.Execeptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusPatrimonioController : ControllerBase
    {
        private readonly StatusPatrimonioService _service;

        public StatusPatrimonioController(StatusPatrimonioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarStatusPatrimonioDto>> Listar()
        {
            List<ListarStatusPatrimonioDto> status = _service.Listar();
            return status;
        }

        [HttpGet("{id}")]
        public ActionResult<ListarStatusPatrimonioDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarStatusPatrimonioDto status = _service.BuscarPorId(id);
                return Ok(status);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarStatusPatrimonioDto dto)
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
        public ActionResult Atualizar(Guid id, CriarStatusPatrimonioDto dto)
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
