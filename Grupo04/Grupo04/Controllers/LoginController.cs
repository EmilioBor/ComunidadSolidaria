using Core.Response;
using Grupo04.Custom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Grupo04.Controllers
{
    [ApiController] // ✅ Agregá esto
    [Route("[controller]")] // ✅ Hace que las rutas sean /Login/Login o /Login/Registrarse
    public class LoginController : ControllerBase // ✅ Usar ControllerBase en APIs
    {
        private readonly comunidadsolidariaContext _context;
        private readonly Utilidades _utilidades;

        public LoginController(comunidadsolidariaContext context, Utilidades utilidades)
        {
            _context = context;
            _utilidades = utilidades;
        }

        // 🔹 REGISTRARSE
        [HttpPost("Registrarse")]
        public async Task<IActionResult> Registrarse([FromBody] UsuarioDtoOut usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Password))
                return BadRequest(new { message = "Datos inválidos." });

            var newRegistro = new Usuario
            {
                Email = usuario.Email,
                Password = _utilidades.EncriptarSHA256(usuario.Password!)
            };

            await _context.Usuario.AddAsync(newRegistro);
            await _context.SaveChangesAsync();

            return Ok(new { isSuccess = newRegistro.Id != 0 });
        }

        // 🔹 LOGIN
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDtoOut login) // ✅ Importante: [FromBody]
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                return BadRequest(new { message = "Email o contraseña inválidos." });

            var passwordEncriptada = _utilidades.EncriptarSHA256(login.Password!);

            var usuarioEncontrado = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == passwordEncriptada);

            if (usuarioEncontrado == null)
            {
                return Ok(new { isSuccess = false, token = "" });
            }

            return Ok(new { isSuccess = true, token = _utilidades.GenerarJWT(usuarioEncontrado) });
        }
    }
}
