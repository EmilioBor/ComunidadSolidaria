using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvioController : ControllerBase
    {
        private readonly IEnvioService _service;
        public EnvioController(IEnvioService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/Envios")]
        public async Task<IEnumerable<EnvioDtoOut>> Envios()
        {
            return await _service.GetEnvioAll();
        }

        [HttpGet("api/v1/Envio/id/{id}")]
        public async Task<ActionResult<EnvioDtoOut>> GetEnvioById(int id)
        {
            var Envio = await _service.GetEnvioDtoById(id);
            if (Envio is null)
            {
                return NotFound(id);
            }
            else
            {
                return Envio;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/Envio")]
        public async Task<IActionResult> Create(EnvioDtoIn EnvioDtoIn)
        {
            var newEnvio = await _service.Create(EnvioDtoIn);

            return CreatedAtAction(nameof(GetEnvioById), new { id = newEnvio.Id }, newEnvio);
        }
    }
}
