using GerenciamentoPatrimonio.Applications.Service;
using GerenciamentoPatrimonio.DTO.LocalizacaoDto;
using GerenciamentoPatrimonio.Execeptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        private readonly LocalizacaoService _service;

        public LocalizacaoController(LocalizacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarLocalizaDto>> Listar()
        {
            List<ListarLocalizaDto> localizacoes = _service.Listar();
            return Ok(localizacoes);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarLocalizaDto> BuscarPorID(Guid id)
        {
            try
            {
                ListarLocalizaDto localizacao = _service.BuscarPorId(id);
                return Ok(localizacao);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarLocalizacaoDto dto)
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
        public ActionResult Atualizar(Guid id, CriarLocalizacaoDto dto)
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
