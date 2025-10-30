using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleDonacionTipoController : ControllerBase
    {
        private readonly IDetalleDonacionTipoService _service;
        public DetalleDonacionTipoController(IDetalleDonacionTipoService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/detalleDonacionTipos")]
        public async Task<IEnumerable<DetalleDonacionTipoDtoOut>> DetalleDonacionTipos()
        {
            return await _service.GetDetalleDonacionTipoAll();
        }

        [HttpGet("api/v1/detalleDonacionTipo/id/{id}")]
        public async Task<ActionResult<DetalleDonacionTipoDtoOut>> GetDetalleDonacionTipoById(int id)
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
        public async Task<IActionResult> Create(DetalleDonacionTipoDtoIn detalleDonacionTipoDtoIn)
        {
            var newDetalleDonacionTipo = await _service.Create(detalleDonacionTipoDtoIn);

            return CreatedAtAction(nameof(GetDetalleDonacionTipoById), new { id = newDetalleDonacionTipo.Id }, newDetalleDonacionTipo);
        }
    }
}
