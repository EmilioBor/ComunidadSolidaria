using Core.Response;
using Grupo04.Custom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Grupo04.Controllers
{
    public class LoginController : Controller
    {
        private readonly comunidadsolidariaContext _context;
        private readonly Utilidades _utilidades;

        public LoginController(comunidadsolidariaContext context, Utilidades utilidades)
        {
            _context = context;
            _utilidades = utilidades;
        }
        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse(UsuarioDtoOut usuario)
        {
            var newregistro = new Usuario
            {
                Email = usuario.Email,
                Contraseña = _utilidades.EncriptarSHA256(usuario.Contraseña!)
            };
            await _context.Usuario.AddAsync(newregistro);
            await _context.SaveChangesAsync();

            if (newregistro.Id != 0)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UsuarioDtoOut login)
        {
            var usuarioEncontrado = await _context.Usuario.Where(u =>
                                                                    u.Email == login.Email &&
                                                                    u.Contraseña == _utilidades.EncriptarSHA256(login.Contraseña!)).FirstOrDefaultAsync();
            if (usuarioEncontrado == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilidades.GenerarJWT(usuarioEncontrado) });
            }
        }
    }
}
