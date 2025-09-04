using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoTipoController : ControllerBase
    {
        private readonly IEstadoTipoServer _service;
        public EstadoTipoController(IEstadoTipoServer service)
        {
            _service = service;
        }

        [HttpGet("api/v1/estadoTipos")]
        public async Task<IEnumerable<EstadoTipoDtoOut>> EstadoTipos()
        {
            return await _service.GetEstadoTipos();
        }

        [HttpGet("api/v1/estadoTipo/id/{id}")]
        public async Task<ActionResult<EstadoTipoDtoOut>> GetEstadoTipoById(int id)
        {
            var estadoTipo = await _service.GetEstadoTipoDtoById(id);
            if (estadoTipo is null)
            {
                return NotFound(id);
            }
            else
            {
                return estadoTipo;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/estadoTipo")]
        public async Task<IActionResult> Create(EstadoTipoDtoIn estadoTipoDtoIn)
        {
            var newEstadoTipo = await _service.Create(estadoTipoDtoIn);

            return CreatedAtAction(nameof(GetEstadoTipoById), new { id = newEstadoTipo.Id }, newEstadoTipo);
        }

        //Editar
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, EstadoTipoDtoIn estadoTipoDtoIn)
        {
            if (id != estadoTipoDtoIn.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({estadoTipoDtoIn.Id}) del cuerpo de la solicitud" });

            var estadoTipoToUpdate = await _service.GetEstadoTipoDtoById(id);

            if (estadoTipoToUpdate is not null)
            {
                await _service.Update(id, estadoTipoDtoIn);
                return NoContent();
            }
            else
            {
                return NotFound(id);
            }

        }

        //ELIMINAr
        [HttpDelete("api/v1/delete/estadoTipo/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _service.GetEstadoTipoDtoById(id);

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
