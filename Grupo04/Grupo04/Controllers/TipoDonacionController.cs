using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDonacionController : ControllerBase
    {
        private readonly ITipoDonacionService _service;
        public TipoDonacionController(ITipoDonacionService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/tipoDonacions")]
        public async Task<IEnumerable<TipoDonacionDtoOut>> TipoDonacions()
        {
            return await _service.GetTipoDonacions();
        }

        [HttpGet("api/v1/tipoDonacion/id/{id}")]
        public async Task<ActionResult<TipoDonacionDtoOut>> GetTipoDonacionById(int id)
        {
            var tipoDonacion = await _service.GetTipoDonacionDtoById(id);
            if (tipoDonacion is null)
            {
                return NotFound(id);
            }
            else
            {
                return tipoDonacion;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/tipoDonacion")]
        public async Task<IActionResult> Create(TipoDonacionDtoIn tipoDonacionDtoIn)
        {
            var newTipoDonacion = await _service.Create(tipoDonacionDtoIn);

            return CreatedAtAction(nameof(GetTipoDonacionById), new { id = newTipoDonacion.Id },newTipoDonacion);
        }

        //Editar
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, TipoDonacionDtoIn tipoDonacionDtoIn)
        {
            if (id != tipoDonacionDtoIn.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({tipoDonacionDtoIn.Id}) del cuerpo de la solicitud" });

            var tipoDonacionToUpdate = await _service.GetTipoDonacionDtoById(id);

            if (tipoDonacionToUpdate is not null)
            {
                await _service.Update(id, tipoDonacionDtoIn);
                return NoContent();
            }
            else
            {
                return NotFound(id);
            }

        }

        //ELIMINAr
        [HttpDelete("api/v1/delete/tipoDonacion/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _service.GetTipoDonacionDtoById(id);

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
