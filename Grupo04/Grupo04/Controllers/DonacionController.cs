using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonacionController : ControllerBase
    {
        private readonly IDonacionService _service;
        public DonacionController(IDonacionService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/donacions")]
        public async Task<IEnumerable<DonacionDtoOut>> Donacions()
        {
            return await _service.GetDonacions();
        }

        [HttpGet("api/v1/donacion/id/{id}")]
        public async Task<ActionResult<DonacionDtoOut>> GetDonacionById(int id)
        {
            var donacion = await _service.GetDonacionDtoById(id);
            if (donacion is null)
            {
                return NotFound(id);
            }
            else
            {
                return donacion;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/donacion")]
        public async Task<IActionResult> Create(DonacionDtoIn donacionDtoIn)
        {
            var newDonacion = await _service.Create(donacionDtoIn);

            return CreatedAtAction(nameof(GetDonacionById), new { id = newDonacion.Id });
        }

        //Editar
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, DonacionDtoIn donacionDtoIn)
        {
            if (id != donacionDtoIn.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({donacionDtoIn.Id}) del cuerpo de la solicitud" });

            var donacionToUpdate = await _service.GetDonacionDtoById(id);

            if (donacionToUpdate is not null)
            {
                await _service.Update(id, donacionDtoIn);
                return NoContent();
            }
            else
            {
                return NotFound(id);
            }

        }

        //ELIMINAr
        [HttpDelete("api/v1/delete/donacion/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _service.GetDonacionDtoById(id);

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
