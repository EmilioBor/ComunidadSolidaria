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
    public class ProvinciaService : IProvinciaService
    {
        private readonly comunidadsolidariaContext _context;

        public ProvinciaService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProvinciaDtoOut>> GetProvincias()
        {
            return await _context.Provincia.Select(m => new ProvinciaDtoOut
            {
                Id = m.Id,
                Nombre = m.Nombre,

            }).ToArrayAsync();
        }
        public async Task<Provincia?> GetById(int id)
        {
            return await _context.Provincia.FindAsync(id);
        }

        public async Task<ProvinciaDtoOut?> GetProvinciaDtoById(int id)
        {
            return await _context.Provincia.Where(m => m.Id == id).Select(m => new ProvinciaDtoOut
            {

                Id = m.Id,
                Nombre = m.Nombre,

            }).SingleOrDefaultAsync();
        }

        public async Task<Provincia> Create(ProvinciaDtoIn provincia)
        {
            var newProvincia = new Provincia();

            newProvincia.Nombre = provincia.Nombre;

            _context.Provincia.Add(newProvincia);
            await _context.SaveChangesAsync();

            return newProvincia;
        }

        public async Task Update(int id, ProvinciaDtoIn provincia)
        {
            var existProvincia = await GetById(id);
            if (existProvincia != null)
            {

                existProvincia.Nombre = provincia.Nombre;

                await _context.SaveChangesAsync();
            }

        }
        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);
            if (toDelete != null)
            {
                _context.Provincia.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
