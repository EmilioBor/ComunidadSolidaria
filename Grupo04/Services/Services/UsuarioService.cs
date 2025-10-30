using Core.Request;
using Core.Response;
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

        public UsuarioService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UsuarioDtoOut>> GetUsuarios()
        {
            return await _context.Usuario.Select(m => new UsuarioDtoOut
            {
                Id = m.Id,
                Email = m.Email,
                Password = m.Password,

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

            }).SingleOrDefaultAsync();
        }

        public async Task<Usuario> Create(UsuarioDtoIn usuario)
        {
            var newUsuario = new Usuario();

            newUsuario.Email = usuario.Email;
            newUsuario.Password = usuario.Password;

            _context.Usuario.Add(newUsuario);
            await _context.SaveChangesAsync();
            return newUsuario;
        }

        public async Task Update(int id, UsuarioDtoIn usuario)
        {
            var existUsuario = await GetById(id);
            if (existUsuario != null)
            {
                existUsuario.Email = usuario.Email;
                existUsuario.Password = usuario.Password;

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
