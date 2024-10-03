using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PocParalell.Services.Interfaces;

namespace PocParalell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsereController : ControllerBase
    {
        private readonly IInsereService _service;
        public InsereController(IInsereService service) 
        { 
            _service = service;
        }
        [HttpGet("cdi")]
        public async Task<ActionResult> InsereCdi()
        {
            await _service.InsereCdis();
            return Ok();
        }

        [HttpGet("posicao")]
        public async Task<ActionResult> InserePosicao()
        {
            await _service.InserePosicoes();
            return Ok();
        }

    }
}
