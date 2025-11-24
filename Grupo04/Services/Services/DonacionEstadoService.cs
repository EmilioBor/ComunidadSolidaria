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
    public class DonacionEstadoService : IDonacionEstadoService
    {
        private readonly comunidadsolidariaContext _context;

        public DonacionEstadoService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DonacionEstadoDtoOut>> GetDonacionEstadoAll()
        {
            return await _context.DonacionEstado.Select(m => new DonacionEstadoDtoOut
            {
                Id = m.Id,
                Nombre = m.Nombre,
                NombreDonacionIdDonacion = m.DonacionIdDonacionNavigation.Descripcion,


            }).ToArrayAsync();
        }
        public async Task<DonacionEstado?> GetById(int id)
        {
            return await _context.DonacionEstado.FindAsync(id);
        }

        public async Task<DonacionEstadoDtoOut?> GetetalleDonacionTipoDtoById(int id)
        {
            return await _context.DonacionEstado.Where(m => m.Id == id).Select(m => new DonacionEstadoDtoOut
            {
                Id = m.Id,
                Nombre = m.Nombre,
                NombreDonacionIdDonacion = m.DonacionIdDonacionNavigation.Descripcion,

            }).SingleOrDefaultAsync();
        }

        public async Task<DonacionEstadoDtoOut?> GetDetalleDonacionTipoDtoByNombre(string estado)
        {
            return await _context.DonacionEstado
                .Where(m => m.DonacionIdDonacionNavigation.Descripcion == estado)
                .OrderByDescending(m => m.Id)   // 👈 Ordenar del más nuevo al más viejo
                .Select(m => new DonacionEstadoDtoOut
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    NombreDonacionIdDonacion = m.DonacionIdDonacionNavigation.Descripcion,
                })
                .FirstOrDefaultAsync();   // 👈 Tomar el último creado
        }



        public async Task<DonacionEstado> Create(DonacionEstadoDtoIn DonacionEstado)
        {
            var newDonacionEstado = new DonacionEstado();

            newDonacionEstado.Nombre = DonacionEstado.Nombre;
            newDonacionEstado.DonacionIdDonacion = DonacionEstado.DonacionIdDonacion;
            

            _context.DonacionEstado.Add(newDonacionEstado);
            await _context.SaveChangesAsync();
            return newDonacionEstado;
        }
    }
}
