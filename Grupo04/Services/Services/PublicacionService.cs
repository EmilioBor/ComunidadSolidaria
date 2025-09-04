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
    public class PublicacionService : IPublicacionService
    {
        private readonly comunidadsolidariaContext _context;

        public PublicacionService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PublicacionDtoOut>> GetPublicacions()
        {
            return await _context.Publicacion.Select(m => new PublicacionDtoOut
            {
                Id = m.Id,
                Descripcion = m.Descripcion,
                FechaCreacion = m.FechaCreacion,
                Titulo = m.Titulo,
                NombreLocalidadIdLocalidad = m.LocalidadIdLocalidadNavigation.Nombre,
                NombreUsuarioIdUsuario = m.UsuarioIdUsuarioNavigation.Email,
                Imagen = m.Imagen,

            }).ToArrayAsync();
        }
        public async Task<Publicacion?> GetById(int id)
        {
            return await _context.Publicacion.FindAsync(id);
        }

        public async Task<PublicacionDtoOut?> GetPublicacionDtoById(int id)
        {
            return await _context.Publicacion.Where(m => m.Id == id).Select(m => new PublicacionDtoOut
            {
                Id = m.Id,
                Descripcion = m.Descripcion,
                FechaCreacion = m.FechaCreacion,
                Titulo = m.Titulo,
                NombreLocalidadIdLocalidad = m.LocalidadIdLocalidadNavigation.Nombre,
                NombreUsuarioIdUsuario = m.UsuarioIdUsuarioNavigation.Email,
                Imagen = m.Imagen,

            }).SingleOrDefaultAsync();
        }

        public async Task<Publicacion> Create(PublicacionDtoIn publicacion, IFormFile files )
        {
            if (files == null || files.Length == 0)
            {
                throw new ArgumentException("No se han proporcionado imágenes.");
            }
            if (string.IsNullOrWhiteSpace(publicacion.Descripcion))
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
            var newPublicacion = new Publicacion();

            newPublicacion.Descripcion = publicacion.Descripcion;
            newPublicacion.FechaCreacion = publicacion.FechaCreacion;
            newPublicacion.Titulo = publicacion.Titulo;
            newPublicacion.UsuarioIdUsuario = publicacion.UsuarioIdUsuario;
            newPublicacion.LocalidadIdLocalidad = publicacion.LocalidadIdLocalidad;
            newPublicacion.Imagen = imageBytes;

            _context.Publicacion.Add(newPublicacion);
            await _context.SaveChangesAsync();
            return newPublicacion;
        }

        public async Task Update(int id, PublicacionDtoIn publicacion)
        {
            var existPublicacion = await GetById(id);
            if (existPublicacion != null)
            {
                existPublicacion.Descripcion = publicacion.Descripcion;
                existPublicacion.FechaCreacion = publicacion.FechaCreacion;
                existPublicacion.Titulo = publicacion.Titulo;
                existPublicacion.UsuarioIdUsuario = publicacion.UsuarioIdUsuario;
                existPublicacion.LocalidadIdLocalidad = publicacion.LocalidadIdLocalidad;
                existPublicacion.Imagen = publicacion.Imagen;

                await _context.SaveChangesAsync();
            }

        }
        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);
            if (toDelete != null)
            {
                _context.Publicacion.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
