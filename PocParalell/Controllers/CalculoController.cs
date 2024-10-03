using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PocParalell.Services.Interfaces;

namespace PocParalell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculoController : ControllerBase
    {
        private readonly ICalculoService _service;
        public CalculoController(ICalculoService service)
        {
            _service = service;
        }
        [HttpGet("1")]
        public async Task<ActionResult> Calculo1()
        {
            await _service.InsereUsandoForeach();
            return Ok();
        }

        [HttpGet("2")]
        public async Task<ActionResult> Calculo2()
        {
            await _service.InsereUsandoPaginacaoBulk();
            return Ok();
        }

        [HttpGet("3")]
        public async Task<ActionResult> Calculo3()
        {
            await _service.InsereUsandoTasks();
            return Ok();
        }
    }
}
