using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Grupo04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionTipoController : ControllerBase
    {
        private readonly IPublicacionTipoService _service;
        public PublicacionTipoController(IPublicacionTipoService service)
        {
            _service = service;
        }

        [HttpGet("api/v1/publicacionTipos")]
        public async Task<IEnumerable<PublicacionTipoDtoOut>> PublicacionTipos()
        {
            return await _service.GetPublicacionTipoAll();
        }

        [HttpGet("api/v1/publicacionTipo/id/{id}")]
        public async Task<ActionResult<PublicacionTipoDtoOut>> GetPublicacionTipoById(int id)
        {
            var publicacionTipo = await _service.GetetalleDonacionTipoDtoById(id);
            if (publicacionTipo is null)
            {
                return NotFound(id);
            }
            else
            {
                return publicacionTipo;
            }
        }

        //agregar
        [HttpPost("api/v1/agrega/publicacionTipo")]
        public async Task<IActionResult> Create(PublicacionTipoDtoIn publicacionTipoDtoIn)
        {
            var newPublicacionTipo = await _service.Create(publicacionTipoDtoIn);

            return CreatedAtAction(nameof(GetPublicacionTipoById), new { id = newPublicacionTipo.Id }, newPublicacionTipo);
        }
    }
}
