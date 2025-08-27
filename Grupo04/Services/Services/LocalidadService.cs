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
    public class LocalidadService : ILocalidadService
    {
        private readonly comunidadsolidariaContext _context;

        public LocalidadService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<LocalidadDtoOut>> GetLocalidads()
        {
            return await _context.Localidad.Select(m => new LocalidadDtoOut
            {
                Id = m.Id,
                Nombre = m.Nombre,
                CodigoPostal = m.CodigoPostal,
                NombreProvinciaIdProvincia = m.ProvinciaIdProvinciaNavigation.Nombre,
            }).ToArrayAsync();
        }
        public async Task<Localidad?> GetById(int id)
        {
            return await _context.Localidad.FindAsync(id);
        }

        public async Task<LocalidadDtoOut?> GetLocalidadDtoById(int id)
        {
            return await _context.Localidad.Where(m => m.Id == id).Select(m => new LocalidadDtoOut
            {
                Id = m.Id,
                Nombre = m.Nombre,
                CodigoPostal = m.CodigoPostal,
                NombreProvinciaIdProvincia = m.ProvinciaIdProvinciaNavigation.Nombre,
            }).SingleOrDefaultAsync();
        }

        public async Task<Localidad> Create(LocalidadDtoIn localidad)
        {
            var newLocalidad = new Localidad();
            newLocalidad.Nombre = localidad.Nombre;
            newLocalidad.CodigoPostal = localidad.CodigoPostal;
            newLocalidad.ProvinciaIdProvincia = newLocalidad.ProvinciaIdProvincia;

            _context.Localidad.Add(newLocalidad);
            await _context.SaveChangesAsync();
            return newLocalidad;
        }

        public async Task Update(int id, LocalidadDtoIn localidad)
        {
            var existLocalidad = await GetById(id);
            if (existLocalidad != null)
            {
                existLocalidad.Nombre = localidad.Nombre;
                existLocalidad.CodigoPostal = localidad.CodigoPostal;
                existLocalidad.ProvinciaIdProvincia = localidad.ProvinciaIdProvincia;

                await _context.SaveChangesAsync();
            }

        }
        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);
            if (toDelete != null)
            {
                _context.Localidad.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
