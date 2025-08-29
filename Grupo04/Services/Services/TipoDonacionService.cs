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
    public class TipoDonacionService : ITipoDonacionService
    {
        private readonly comunidadsolidariaContext _context;

        public TipoDonacionService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TipoDonacionDtoOut>> GetTipoDonacions()
        {
            return await _context.TipoDonacion.Select(m => new TipoDonacionDtoOut
            {
                Id = m.Id,
                Descripcion = m.Descripcion,
                

            }).ToArrayAsync();
        }
        public async Task<TipoDonacion?> GetById(int id)
        {
            return await _context.TipoDonacion.FindAsync(id);
        }

        public async Task<TipoDonacionDtoOut?> GetTipoDonacionDtoById(int id)
        {
            return await _context.TipoDonacion.Where(m => m.Id == id).Select(m => new TipoDonacionDtoOut
            {
                Id = m.Id,
                Descripcion = m.Descripcion,
                

            }).SingleOrDefaultAsync();
        }

        public async Task<TipoDonacion> Create(TipoDonacionDtoIn tipoDonacion)
        {
            var newTipoDonacion = new TipoDonacion();

            newTipoDonacion.Descripcion = tipoDonacion.Descripcion;
            

            _context.TipoDonacion.Add(newTipoDonacion);
            await _context.SaveChangesAsync();
            return newTipoDonacion;
        }

        public async Task Update(int id, TipoDonacionDtoIn tipoDonacion)
        {
            var existTipoDonacion = await GetById(id);
            if (existTipoDonacion != null)
            {
                existTipoDonacion.Descripcion = tipoDonacion.Descripcion;
                
                await _context.SaveChangesAsync();
            }

        }
        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);
            if (toDelete != null)
            {
                _context.TipoDonacion.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
