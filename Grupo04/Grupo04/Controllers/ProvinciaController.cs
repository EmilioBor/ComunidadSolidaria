using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly IProvinciaService _service;
        public ProvinciaController(IProvinciaService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/provincias")]
        public async Task<IEnumerable<ProvinciaDtoOut>> Provincias()
        {
            return await _service.GetProvincias();
        }

        [HttpGet("api/v1/provincia/id/{id}")]
        public async Task<ActionResult<ProvinciaDtoOut>> GetProvinciaById(int id)
        {
            var provincia = await _service.GetProvinciaDtoById(id);
            if (provincia is null)
            {
                return NotFound(id);
            }
            else
            {
                return provincia;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/provincia")]
        public async Task<IActionResult> Create(ProvinciaDtoIn provinciaDtoIn)
        {
            var newProvincia = await _service.Create(provinciaDtoIn);

            return CreatedAtAction(nameof(GetProvinciaById), new { id = newProvincia.Id }, newProvincia);
        }

        //Editar
        [HttpPut("api/v1/editar/{id}")]
        public async Task<IActionResult> Update(int id, ProvinciaDtoIn provinciaDtoIn)
        {
            if (id != provinciaDtoIn.Id)
                return BadRequest(new { message = $"El ID = {id} de la URL no coincide con el ID({provinciaDtoIn.Id}) del cuerpo de la solicitud" });

            var provinciaToUpdate = await _service.GetProvinciaDtoById(id);

            if (provinciaToUpdate is not null)
            {
                await _service.Update(id, provinciaDtoIn);
                return NoContent();
            }
            else
            {
                return NotFound(id);
            }

        }

        //ELIMINAr
        [HttpDelete("api/v1/delete/provincia/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDelete = await _service.GetProvinciaDtoById(id);

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
