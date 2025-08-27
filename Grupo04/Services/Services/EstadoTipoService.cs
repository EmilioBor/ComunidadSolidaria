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
    public class EstadoTipoService : IEstadoTipoServer
    {
        private readonly comunidadsolidariaContext _context;

        public EstadoTipoService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EstadoTipoDtoOut>> GetEstadoTipos()
        {
            return await _context.EstadoTipo.Select(m => new EstadoTipoDtoOut
            {
                Id = m.Id,
                Nombre = m.Nombre,

            }).ToArrayAsync();
        }
        public async Task<EstadoTipo?> GetById(int id)
        {
            return await _context.EstadoTipo.FindAsync(id);
        }

        public async Task<EstadoTipoDtoOut?> GetEstadoTipoDtoById(int id)
        {
            return await _context.EstadoTipo.Where(m => m.Id == id).Select(m => new EstadoTipoDtoOut
            {
                Id = m.Id,
                Nombre = m.Nombre,

            }).SingleOrDefaultAsync();
        }

        public async Task<EstadoTipo> Create(EstadoTipoDtoIn estadoTipo)
        {
            var newEstadoTipo = new EstadoTipo();

            newEstadoTipo.Nombre = estadoTipo.Nombre;

            _context.EstadoTipo.Add(newEstadoTipo);
            await _context.SaveChangesAsync();
            return newEstadoTipo;
        }

        public async Task Update(int id, EstadoTipoDtoIn estadoTipo)
        {
            var existEstadoTipo = await GetById(id);
            if (existEstadoTipo != null)
            {
                existEstadoTipo.Nombre = estadoTipo.Nombre;

                await _context.SaveChangesAsync();
            }

        }
        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);
            if (toDelete != null)
            {
                _context.EstadoTipo.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
