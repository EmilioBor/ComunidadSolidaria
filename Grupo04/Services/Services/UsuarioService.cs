using Core.Request;
using Core.Response;
using Grupo04.Custom;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly comunidadsolidariaContext _context;
        private readonly Utilidades _utilidades;
        public UsuarioService(comunidadsolidariaContext context, Utilidades utilidades)
        {
            _context = context;
            _utilidades = utilidades;
        }
        public async Task<IEnumerable<UsuarioDtoOut>> GetUsuarios()
        {
            return await _context.Usuario.Select(m => new UsuarioDtoOut
            {
                Id = m.Id,
                Email = m.Email,
                Password = m.Password,
                Rol = m.Rol,

            }).ToArrayAsync();
        }
        public async Task<Usuario?> GetById(int id)
        {
            return await _context.Usuario.FindAsync(id);
        }

        public async Task<UsuarioDtoOut?> GetUsuarioDtoById(int id)
        {
            return await _context.Usuario.Where(m => m.Id == id).Select(m => new UsuarioDtoOut
            {
                Id = m.Id,
                Email = m.Email,
                Password = m.Password,
                Rol = m.Rol,

            }).SingleOrDefaultAsync();
        }

        public async Task<UsuarioAuthResponse> Create(UsuarioDtoIn usuario)
        {
            // Evitar duplicados
            var existe = await _context.Usuario.AnyAsync(u => u.Email == usuario.Email);
            if (existe)
            {
                return new UsuarioAuthResponse
                {
                    Email = usuario.Email,
                    EsCorrecto = false
                };
            }

            var nuevo = new Usuario
            {
                Email = usuario.Email,
                Password = _utilidades.EncriptarSHA256(usuario.Password),
                Rol = usuario.Rol
            };

            _context.Usuario.Add(nuevo);
            await _context.SaveChangesAsync();

            // Generar token
            var token = _utilidades.GenerarJWT(nuevo);

            return new UsuarioAuthResponse
            {
                Id = nuevo.Id,
                Email = nuevo.Email,
                Token = token,
                EsCorrecto = true
            };
            //var newUsuario = new Usuario();

            //newUsuario.Email = usuario.Email;
            //newUsuario.Password = usuario.Password;
            //newUsuario.Rol = usuario.Rol;

            //_context.Usuario.Add(newUsuario);
            //await _context.SaveChangesAsync();
            //return newUsuario;
        }
        public async Task<UsuarioAuthResponse> Login(string email, string password)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null)
            {
                return new UsuarioAuthResponse
                {
                    Email = email,
                    EsCorrecto = false
                };
            }

            string passwordEncriptada = _utilidades.EncriptarSHA256(password);
            if (usuario.Password != passwordEncriptada)
            {
                return new UsuarioAuthResponse
                {
                    Email = email,
                    EsCorrecto = false
                };
            }

            var token = _utilidades.GenerarJWT(usuario);

            return new UsuarioAuthResponse
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Token = token,
                EsCorrecto = true
            };
        }

        public async Task Update(int id, UsuarioDtoIn usuario)
        {
            var existUsuario = await GetById(id);
            if (existUsuario != null)
            {
                existUsuario.Email = usuario.Email;
                existUsuario.Password = usuario.Password;
                existUsuario.Rol= usuario.Rol;

                await _context.SaveChangesAsync();
            }

        }
        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);
            if (toDelete != null)
            {
                _context.Usuario.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
