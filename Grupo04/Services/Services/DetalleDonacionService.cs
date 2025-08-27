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
    public class DetalleDonacionService : IDetalleDonacionService
    {
        private readonly comunidadsolidariaContext _context;

        public DetalleDonacionService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DetalleDonacionDtoOut>> GetDetalleDonacions()
        {
            return await _context.DetalleDonacion.Select(m => new DetalleDonacionDtoOut
            {
                Id = m.Id,
                Descripcion = m.Descripcion,
                NombreDonacionIdDonacion = m.DonacionIdDonacionNavigation.Descripcion,
                NombreEnvioIdEnvio = m.DonacionIdDonacionNavigation.Descripcion,
            }).ToArrayAsync();
        }
        public async Task<DetalleDonacion?> GetById(int id)
        {
            return await _context.DetalleDonacion.FindAsync(id);
        }

        public async Task<DetalleDonacionDtoOut?> GetDetalleDonacionDtoById(int id)
        {
            return await _context.DetalleDonacion.Where(m => m.Id == id).Select(m => new DetalleDonacionDtoOut
            {
                Id = m.Id,
                Descripcion = m.Descripcion,
                NombreDonacionIdDonacion = m.DonacionIdDonacionNavigation.Descripcion,
                NombreEnvioIdEnvio = m.DonacionIdDonacionNavigation.Descripcion,

            }).SingleOrDefaultAsync();
        }

        public async Task<DetalleDonacion> Create(DetalleDonacionDtoIn detalleDonacion)
        {
            var newDetalleDonacion = new DetalleDonacion();

            newDetalleDonacion.Descripcion = newDetalleDonacion.Descripcion;
            newDetalleDonacion.DonacionIdDonacion = newDetalleDonacion.DonacionIdDonacion;
            newDetalleDonacion.EnvioIdEnvio = newDetalleDonacion.EnvioIdEnvio;

            _context.DetalleDonacion.Add(newDetalleDonacion);
            await _context.SaveChangesAsync();
            return newDetalleDonacion;
        }

        public async Task Update(int id, DetalleDonacionDtoIn detalleDonacion)
        {
            var existDetalleDonacion = await GetById(id);
            if (existDetalleDonacion != null)
            {
                existDetalleDonacion.Descripcion = detalleDonacion.Descripcion;
                existDetalleDonacion.DonacionIdDonacion = detalleDonacion.DonacionIdDonacion;
                existDetalleDonacion.EnvioIdEnvio = detalleDonacion.EnvioIdEnvio;
                await _context.SaveChangesAsync();
            }

        }
        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);
            if (toDelete != null)
            {
                _context.DetalleDonacion.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
