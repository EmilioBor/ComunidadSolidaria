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
    public class PublicacionTipoService : IPublicacionTipoService
    {
        private readonly comunidadsolidariaContext _context;

        public PublicacionTipoService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PublicacionTipoDtoOut>> GetPublicacionTipoAll()
        {
            return await _context.PublicacionTipo.Select(m => new PublicacionTipoDtoOut
            {
                Id = m.Id,
                Nombre = m.Nombre,


            }).ToArrayAsync();
        }
        public async Task<PublicacionTipo?> GetById(int id)
        {
            return await _context.PublicacionTipo.FindAsync(id);
        }

        public async Task<PublicacionTipoDtoOut?> GetetalleDonacionTipoDtoById(int id)
        {
            return await _context.PublicacionTipo.Where(m => m.Id == id).Select(m => new PublicacionTipoDtoOut
            {
                Id = m.Id,
                Nombre = m.Nombre,

            }).SingleOrDefaultAsync();
        }

        public async Task<PublicacionTipo> Create(PublicacionTipoDtoIn publicacionTipo)
        {
            var newPublicacionTipo = new PublicacionTipo();

            newPublicacionTipo.Nombre = publicacionTipo.Nombre;


            _context.PublicacionTipo.Add(newPublicacionTipo);
            await _context.SaveChangesAsync();
            return newPublicacionTipo;
        }
    }
}
