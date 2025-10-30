using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonacionTipoController : ControllerBase
    {
        private readonly IDonacionTipoService _service;
        public DonacionTipoController(IDonacionTipoService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/donacionTipos")]
        public async Task<IEnumerable<DonacionTipoDtoOut>> DonacionTipos()
        {
            return await _service.GetDonacionTipoAll();
        }

        [HttpGet("api/v1/donacionTipo/id/{id}")]
        public async Task<ActionResult<DonacionTipoDtoOut>> GetDonacionTipoById(int id)
        {
            var donacionTipo = await _service.GetDonacionTipoDtoById(id);
            if (donacionTipo is null)
            {
                return NotFound(id);
            }
            else
            {
                return donacionTipo;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/donacionTipo")]
        public async Task<IActionResult> Create(DonacionTipoDtoIn donacionTipoDtoIn)
        {
            var newDonacionTipo = await _service.Create(donacionTipoDtoIn);

            return CreatedAtAction(nameof(GetDonacionTipoById), new { id = newDonacionTipo.Id }, newDonacionTipo);
        }
    }
}
