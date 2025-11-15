using Core.Request;
using Core.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UsuarioReporteService : IUsuarioReporte
    {
        private readonly comunidadsolidariaContext _context;

        public UsuarioReporteService(comunidadsolidariaContext context)
        {
            _context = context;
        }

        // ✅ Obtener todos los reportes
        public async Task<IEnumerable<UsuarioReporteDtoOut>> GetUsuarioReportes()
        {
            return await _context.UsuarioReporte
                .Include(r => r.PerfilIdPerfilNavigation)
                .Include(r => r.PublicacionIdPublicacionNavigation)
                .Select(m => new UsuarioReporteDtoOut
                {
                    Id = m.Id,
                    Motivo = m.Motivo,
                    Descripcion = m.Descripcion,
                    FechaHora = m.FechaHora,
                    NombrePerfilIdPerfil = m.PerfilIdPerfilNavigation.RazonSocial,
                    NombrePublicacionIdPublicacion = m.PublicacionIdPublicacionNavigation.Titulo
                })
                .ToArrayAsync();
        }

        // ✅ Obtener por id
        public async Task<UsuarioReporte?> GetById(int id)
        {
            return await _context.UsuarioReporte.FindAsync(id);
        }

        public async Task<UsuarioReporteDtoOut?> GetUsuarioReporteDtoById(int id)
        {
            return await _context.UsuarioReporte
                .Include(r => r.PerfilIdPerfilNavigation)
                .Include(r => r.PublicacionIdPublicacionNavigation)
                .Where(m => m.Id == id)
                .Select(m => new UsuarioReporteDtoOut
                {
                    Id = m.Id,
                    Motivo = m.Motivo,
                    Descripcion = m.Descripcion,
                    FechaHora = m.FechaHora,
                    NombrePerfilIdPerfil = m.PerfilIdPerfilNavigation.RazonSocial,
                    NombrePublicacionIdPublicacion = m.PublicacionIdPublicacionNavigation.Titulo
                })
                .SingleOrDefaultAsync();
        }

        // ✅ Contar reportes por perfil
        public async Task<int> CountReportesPorPerfil(int perfilId)
        {
            return await _context.UsuarioReporte
                .Where(r => r.PerfilIdPerfil == perfilId)
                .CountAsync();
        }

        // ✅ Contar reportes por publicación
        public async Task<int> CountReportesPorPublicacion(int publicacionId)
        {
            return await _context.UsuarioReporte
                .Where(r => r.PublicacionIdPublicacion == publicacionId)
                .CountAsync();
        }

        // ✅ Crear reporte con contador automático
        public async Task<(UsuarioReporte reporte, int totalReportesPerfil)> CreateConContador(UsuarioReporteDtoIn dto)
        {
            // Validar que perfil y publicación existan
            var perfilExist = await _context.Perfil.FindAsync(dto.PerfilIdPerfil);
            var publicacionExist = await _context.Publicacion.FindAsync(dto.PublicacionIdPublicacion);
            if (perfilExist == null) throw new ArgumentException("Perfil no existe");
            if (publicacionExist == null) throw new ArgumentException("Publicación no existe");

            var newReporte = new UsuarioReporte
            {
                Motivo = dto.Motivo,
                Descripcion = dto.Descripcion,
                FechaHora = dto.FechaHora.ToUniversalTime(), // 🔹 Convertir a UTC
                PerfilIdPerfil = dto.PerfilIdPerfil,
                PublicacionIdPublicacion = dto.PublicacionIdPublicacion
            };

            _context.UsuarioReporte.Add(newReporte);
            await _context.SaveChangesAsync();

            var totalReportes = await CountReportesPorPerfil(dto.PerfilIdPerfil);

            return (newReporte, totalReportes);
        }

        // ✅ Actualizar reporte
        public async Task Update(int id, UsuarioReporteDtoIn dto)
        {
            var existUsuarioReporte = await GetById(id);
            if (existUsuarioReporte != null)
            {
                existUsuarioReporte.Motivo = dto.Motivo;
                existUsuarioReporte.Descripcion = dto.Descripcion;
                existUsuarioReporte.FechaHora = dto.FechaHora.ToUniversalTime(); // 🔹 UTC
                existUsuarioReporte.PerfilIdPerfil = dto.PerfilIdPerfil;
                existUsuarioReporte.PublicacionIdPublicacion = dto.PublicacionIdPublicacion;

                await _context.SaveChangesAsync();
            }
        }

        // ✅ Eliminar reporte
        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);
            if (toDelete != null)
            {
                _context.UsuarioReporte.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
