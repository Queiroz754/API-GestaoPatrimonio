using Azure.Core.Pipeline;
using GerenciamentoPatrimonio.Applications.Service;
using GerenciamentoPatrimonio.DTO.AreaDto;
using GerenciamentoPatrimonio.DTO.CidadeDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly CidadeService _service;

        public CidadeController(CidadeService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarCidadeDto>> Listar()
        {
            List<ListarCidadeDto> cidade = _service.Listar();
            return cidade;
        }

        [HttpGet("{id}")]
        public ActionResult<ListarCidadeDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarCidadeDto cidade = _service.BuscarPorId(id);
                return Ok(cidade);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarCidadeDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id,CriarCidadeDto dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
