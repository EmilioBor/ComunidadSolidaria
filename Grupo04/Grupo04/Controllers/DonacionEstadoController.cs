using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonacionEstadoController : ControllerBase
    {
        private readonly IDonacionEstadoService _service;
        public DonacionEstadoController(IDonacionEstadoService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/detalleDonacionTipos")]
        public async Task<IEnumerable<DonacionEstadoDtoOut>> DetalleDonacionTipos()
        {
            return await _service.GetDonacionEstadoAll();
        }

        [HttpGet("api/v1/detalleDonacionTipo/id/{id}")]
        public async Task<ActionResult<DonacionEstadoDtoOut>> GetDetalleDonacionTipoById(int id)
        {
            var detalleDonacionTipo = await _service.GetetalleDonacionTipoDtoById(id);
            if (detalleDonacionTipo is null)
            {
                return NotFound(id);
            }
            else
            {
                return detalleDonacionTipo;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/detalleDonacionTipo")]
        public async Task<IActionResult> Create(DonacionEstadoDtoIn detalleDonacionTipoDtoIn)
        {
            var newDetalleDonacionTipo = await _service.Create(detalleDonacionTipoDtoIn);

            return CreatedAtAction(nameof(GetDetalleDonacionTipoById), new { id = newDetalleDonacionTipo.Id }, newDetalleDonacionTipo);
        }
    }
}
