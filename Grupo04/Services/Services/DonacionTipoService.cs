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
    public class DonacionTipoService : IDonacionTipoService
    {
        private readonly comunidadsolidariaContext _context;

        public DonacionTipoService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DonacionTipoDtoOut>> GetDonacionTipoAll()
        {
            return await _context.DonacionTipo.Select(m => new DonacionTipoDtoOut
            {
                Id = m.Id,
                Descripcion = m.Descripcion,


            }).ToArrayAsync();
        }
        public async Task<DonacionTipo?> GetById(int id)
        {
            return await _context.DonacionTipo.FindAsync(id);
        }

        public async Task<DonacionTipoDtoOut?> GetDonacionTipoDtoById(int id)
        {
            return await _context.DonacionTipo.Where(m => m.Id == id).Select(m => new DonacionTipoDtoOut
            {
                Id = m.Id,
                Descripcion = m.Descripcion,


            }).SingleOrDefaultAsync();
        }

        public async Task<DonacionTipo> Create(DonacionTipoDtoIn donacionTipo)
        {
            var newDonacionTipo = new DonacionTipo();

            newDonacionTipo.Descripcion = donacionTipo.Descripcion;


            _context.DonacionTipo.Add(newDonacionTipo);
            await _context.SaveChangesAsync();
            return newDonacionTipo;
        }
    }
}
