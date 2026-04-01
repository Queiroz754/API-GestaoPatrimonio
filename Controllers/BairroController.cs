using GerenciamentoPatrimonio.Applications.Service;
using GerenciamentoPatrimonio.DTO.BairroDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BairroController : ControllerBase
    {
        private readonly BairroService _service;

        public BairroController(BairroService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarBairroDto>> Listar()
        {
            List<ListarBairroDto> bairro = _service.Listar();
            return bairro;
        }

        [HttpGet("{id}")]
        public ActionResult<ListarBairroDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarBairroDto bairro = _service.BuscarPorId(id);
                return Ok(bairro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public ActionResult Adicionar(CriarBairroDto dto)
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
        public ActionResult Atualizar(Guid id, CriarBairroDto dto)
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
