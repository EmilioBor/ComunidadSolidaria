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
    public class NovedadService : INovedadService
    {
        private readonly comunidadsolidariaContext _context;

        public NovedadService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<NovedadDtoOut>> GetNovedads()
        {
            return await _context.Novedad
                .OrderByDescending(m => m.Fecha)
                .Select(m => new NovedadDtoOut
            {
                Id = m.Id,
                Titulo = m.Titulo,
                Descripcion = m.Descripcion,    
                Fecha = m.Fecha,
                NombrePerfilIdPerfil = m.PerfilIdPerfilNavigation.RazonSocial,
                Imagen = m.Imagen,

            }).ToArrayAsync();
        }
        public async Task<Novedad?> GetById(int id)
        {
            return await _context.Novedad.FindAsync(id);
        }

        public async Task<NovedadDtoOut?> GetNovedadDtoById(int id)
        {
            return await _context.Novedad.Where(m => m.Id == id).Select(m => new NovedadDtoOut
            {
                Id = m.Id,
                Titulo = m.Titulo,
                Descripcion = m.Descripcion,
                Fecha = m.Fecha,
                NombrePerfilIdPerfil = m.PerfilIdPerfilNavigation.RazonSocial,
                Imagen = m.Imagen,

            }).SingleOrDefaultAsync();
        }

        public async Task<Novedad> Create(NovedadDtoIn novedad, IFormFile files)
        {

            if (files == null || files.Length == 0)
            {
                throw new ArgumentException("No se han proporcionado imágenes.");
            }
            if (string.IsNullOrWhiteSpace(novedad.Descripcion))
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
            var newNovedad = new Novedad();

            newNovedad.Titulo = novedad.Titulo;
            newNovedad.Descripcion = novedad.Descripcion;
            newNovedad.Fecha = novedad.Fecha;
            newNovedad.PerfilIdPerfil = novedad.PerfilIdPerfil;
            newNovedad.Imagen = imageBytes;


            _context.Novedad.Add(newNovedad);
            await _context.SaveChangesAsync();
            return newNovedad;
        }

        public async Task Update(int id, NovedadDtoIn novedad)
        {
            var existNovedad = await GetById(id);
            if (existNovedad != null)
            {

                existNovedad.Titulo = novedad.Titulo;
                existNovedad.Descripcion = novedad.Descripcion;
                existNovedad.Fecha = novedad.Fecha;
                existNovedad.Imagen = novedad.Imagen;
                existNovedad.PerfilIdPerfil = novedad.PerfilIdPerfil;


                await _context.SaveChangesAsync();
            }

        }
        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);
            if (toDelete != null)
            {
                _context.Novedad.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
