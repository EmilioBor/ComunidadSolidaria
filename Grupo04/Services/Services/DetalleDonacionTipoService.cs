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
    public class DetalleDonacionTipoService : IDetalleDonacionTipoService
    {
        private readonly comunidadsolidariaContext _context;

        public DetalleDonacionTipoService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DetalleDonacionTipoDtoOut>> GetDetalleDonacionTipoAll()
        {
            return await _context.DetalleDonacionTipo.Select(m => new DetalleDonacionTipoDtoOut
            {
                Id = m.Id,
                Nombre = m.Nombre,
                

            }).ToArrayAsync();
        }
        public async Task<DetalleDonacionTipo?> GetById(int id)
        {
            return await _context.DetalleDonacionTipo.FindAsync(id);
        }

        public async Task<DetalleDonacionTipoDtoOut?> GetetalleDonacionTipoDtoById(int id)
        {
            return await _context.DetalleDonacionTipo.Where(m => m.Id == id).Select(m => new DetalleDonacionTipoDtoOut
            {
                Id = m.Id,
                Nombre = m.Nombre,

            }).SingleOrDefaultAsync();
        }

        public async Task<DetalleDonacionTipo> Create(DetalleDonacionTipoDtoIn detalleDonacionTipo)
        {
            var newDetalleDonacionTipo = new DetalleDonacionTipo();

            newDetalleDonacionTipo.Nombre = detalleDonacionTipo.Nombre;
            

            _context.DetalleDonacionTipo.Add(newDetalleDonacionTipo);
            await _context.SaveChangesAsync();
            return newDetalleDonacionTipo;
        }
    }
}
