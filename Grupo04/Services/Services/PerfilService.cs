using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
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
    public class PerfilService : IPerfilService
    {
        private readonly comunidadsolidariaContext _context;

        public PerfilService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PerfilDtoOut>> GetPerfils()
        {
            return await _context.Perfil.Select(m => new PerfilDtoOut
            {
                Id = m.Id,
                CuitCuil = m.CuitCuil,
                RazonSocial = m.RazonSocial,
                Descripcion = m.Descripcion,
                Cbu = m.Cbu,
                Alias = m.Alias,
                NombreUsuarioIdUsuario = m.UsuarioIdUsuarioNavigation.Email,
                NombreLocalidadIdLocalidad = m.LocalidadIdLocalidadNavigation.Nombre,
                Imagen = m.Imagen,

            }).ToArrayAsync();
        }
        public async Task<Perfil?> GetById(int id)
        {
            return await _context.Perfil.FindAsync(id);
        }

        public async Task<Perfil?> GetPerfilIdUsuario(int id)
        {
            return await _context.Perfil.Where(m => m.UsuarioIdUsuario == id).Select(m => new Perfil
            {
                Id = m.Id,
                CuitCuil = m.CuitCuil,
                RazonSocial = m.RazonSocial,
                Descripcion = m.Descripcion,
                Cbu = m.Cbu,
                Alias = m.Alias,
                UsuarioIdUsuario = m.UsuarioIdUsuario,
                LocalidadIdLocalidad = m.LocalidadIdLocalidad,
                Imagen = m.Imagen,

            }).SingleOrDefaultAsync();
        }


        public async Task<Perfil?> GetPerfilIdNombre(string nombre)
        {
            return await _context.Perfil.Where(m => m.RazonSocial == nombre).Select(m => new Perfil
            {
                Id = m.Id,
                CuitCuil = m.CuitCuil,
                RazonSocial = m.RazonSocial,
                Descripcion = m.Descripcion,
                Cbu = m.Cbu,
                Alias = m.Alias,
                UsuarioIdUsuario = m.UsuarioIdUsuario,
                LocalidadIdLocalidad = m.LocalidadIdLocalidad,
                Imagen = m.Imagen,

            }).SingleOrDefaultAsync();
        }


        public async Task<PerfilDtoOut?> GetPerfilDtoById(int id)
        {
            return await _context.Perfil.Where(m => m.Id == id).Select(m => new PerfilDtoOut
            {
                Id = m.Id,
                CuitCuil = m.CuitCuil,
                RazonSocial = m.RazonSocial,
                Descripcion = m.Descripcion,
                Cbu = m.Cbu,
                Alias = m.Alias,
                NombreUsuarioIdUsuario = m.UsuarioIdUsuarioNavigation.Email,
                NombreLocalidadIdLocalidad = m.LocalidadIdLocalidadNavigation.Nombre,
                Imagen = m.Imagen,

            }).SingleOrDefaultAsync();
        }

        public async Task<Perfil> Create(PerfilDtoIn perfil, IFormFile files)
        {
            if (files == null || files.Length == 0)
            {
                throw new ArgumentException("No se han proporcionado imágenes.");
            }
            if (string.IsNullOrWhiteSpace(perfil.Descripcion))
            {
                throw new ArgumentException("El Nombre no puede estar vacío.");
            }

            // Convierte el archivo de imagen a un array de bytes
            byte[] imageBytes;
            using (var memoryStream = new MemoryStream())
            {
                await files.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();
            }

            var newPerfil = new Perfil();

            newPerfil.CuitCuil = perfil.CuitCuil;
            newPerfil.RazonSocial = perfil.RazonSocial;
            newPerfil.Descripcion = perfil.Descripcion;
            newPerfil.Cbu = perfil.Cbu;
            newPerfil.Alias = perfil.Alias;
            newPerfil.UsuarioIdUsuario = perfil.UsuarioIdUsuario;
            newPerfil.LocalidadIdLocalidad = perfil.LocalidadIdLocalidad;
            newPerfil.Imagen = imageBytes;

            _context.Perfil.Add(newPerfil);
            await _context.SaveChangesAsync();
            return newPerfil;
        }

        public async Task Update(int id, PerfilDtoIn perfil)
        {
            var existPerfil = await GetById(id);
            if (existPerfil != null)
            {


                existPerfil.CuitCuil = perfil.CuitCuil;
                existPerfil.RazonSocial = perfil.RazonSocial;
                existPerfil.Descripcion = perfil.Descripcion;
                existPerfil.Cbu = perfil.Cbu;
                existPerfil.Alias = perfil.Alias;
                existPerfil.UsuarioIdUsuario = perfil.UsuarioIdUsuario;
                existPerfil.LocalidadIdLocalidad = perfil.LocalidadIdLocalidad;
                existPerfil.Imagen = perfil.Imagen;


                await _context.SaveChangesAsync();
            }

        }
        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);
            if (toDelete != null)
            {
                _context.Perfil.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
