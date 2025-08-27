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
        private readonly IEnvioServer _service;
        public EnvioController(IEnvioServer service)
        {
            _service = service;
        }

        [HttpGet("api/v1/envios")]
        public async Task<IEnumerable<EnvioDtoOut>> Envios()
        {
            return await _service.GetEnvios();
        }

        [HttpGet("api/v1/envio/id/{id}")]
        public async Task<ActionResult<EnvioDtoOut>> GetEnvioById(int id)
        {
            var envio = await _service.GetEnvioDtoById(id);
            if (envio is null)
            {
                return NotFound(id);
            }
            else
            {
                return envio;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/envio")]
        public async Task<IActionResult> Create(EnvioDtoIn envioDtoIn)
        {
            var newEnvio = await _service.Create(envioDtoIn);

            return CreatedAtAction(nameof(GetEnvioById), new { id = newEnvio.Id });
        }

        //Editar
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, EnvioDtoIn envioDtoIn)
        {
            if (id != envioDtoIn.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({envioDtoIn.Id}) del cuerpo de la solicitud" });

            var envioToUpdate = await _service.GetEnvioDtoById(id);

            if (envioToUpdate is not null)
            {
                await _service.Update(id, envioDtoIn);
                return NoContent();
            }
            else
            {
                return NotFound(id);
            }

        }

        //ELIMINAr
        [HttpDelete("api/v1/delete/envio/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _service.GetEnvioDtoById(id);

            if (toDelete is not null)
            {
                await _service.Delete(id);
                return Ok();
            }
            else
            {
                return NotFound(id);
            }
        }
    }
}
