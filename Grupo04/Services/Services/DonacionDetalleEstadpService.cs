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
    public class DonacionDetalleEstadoService : IDonacionDetalleEstadoService
    {
        private readonly comunidadsolidariaContext _context;

        public DonacionDetalleEstadoService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DonacionDetalleEstadoDtoOut>> GetDonacionDetalleEstados()
        {
            return await _context.DonacionDetalleEstado.Select(m => new DonacionDetalleEstadoDtoOut
            {
                Id = m.Id,
                Descripcion = m.Descripcion,
                NombreDonacionIdDonacion = m.DonacionIdDonacionNavigation.Descripcion,
                Cantidad = m.Cantidad,
                NombreDonacionEstadoIdDonacionEstado = m.DonacionEstadoIdDonacionEstadoNavigation.Nombre,

            }).ToArrayAsync();
        }
        public async Task<DonacionDetalleEstado?> GetById(int id)
        {
            return await _context.DonacionDetalleEstado.FindAsync(id);
        }

        public async Task<DonacionDetalleEstadoDtoOut?> GetDonacionDetalleEstadoDtoById(int id)
        {
            return await _context.DonacionDetalleEstado.Where(m => m.Id == id).Select(m => new DonacionDetalleEstadoDtoOut
            {
                Id = m.Id,
                Descripcion = m.Descripcion,
                NombreDonacionIdDonacion = m.DonacionIdDonacionNavigation.Descripcion,
                Cantidad = m.Cantidad,
                NombreDonacionEstadoIdDonacionEstado = m.DonacionEstadoIdDonacionEstadoNavigation.Nombre,

            }).SingleOrDefaultAsync();
        }

        public async Task<DonacionDetalleEstado> Create(DonacionDetalleEstadoDtoIn donacionDetalleEstado)
        {
            var newDonacionDetalleEstado = new DonacionDetalleEstado();

            newDonacionDetalleEstado.Descripcion = donacionDetalleEstado.Descripcion;
            newDonacionDetalleEstado.DonacionIdDonacion = donacionDetalleEstado.DonacionIdDonacion;
            newDonacionDetalleEstado.Cantidad = donacionDetalleEstado.Cantidad;
            newDonacionDetalleEstado.DonacionEstadoIdDonacionEstado = donacionDetalleEstado.DonacionEstadoIdDonacionEstado;

            _context.DonacionDetalleEstado.Add(newDonacionDetalleEstado);
            await _context.SaveChangesAsync();
            return newDonacionDetalleEstado;
        }

        public async Task Update(int id, DonacionDetalleEstadoDtoIn DonacionDetalleEstado)
        {
            var existDonacionDetalleEstado = await GetById(id);
            if (existDonacionDetalleEstado != null)
            {
                existDonacionDetalleEstado.Descripcion = DonacionDetalleEstado.Descripcion;
                existDonacionDetalleEstado.DonacionIdDonacion = DonacionDetalleEstado.DonacionIdDonacion;
                existDonacionDetalleEstado.Cantidad = DonacionDetalleEstado.Cantidad;
                existDonacionDetalleEstado.DonacionEstadoIdDonacionEstado = DonacionDetalleEstado.DonacionEstadoIdDonacionEstado;

                await _context.SaveChangesAsync();
            }

        }
        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);
            if (toDelete != null)
            {
                _context.DonacionDetalleEstado.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
