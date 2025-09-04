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
    public class EnvioService : IEnvioServer
    {
        private readonly comunidadsolidariaContext _context;

        public EnvioService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EnvioDtoOut>> GetEnvios()
        {
            return await _context.Envio.Select(m => new EnvioDtoOut
            {
                Id = m.Id,
                Direccion = m.Direccion,
                FechaEnvio = m.FechaEnvio,
                NombreEstadoIdEstado = m.EstadoIdEstadoNavigation.Nombre,
                NombreLocalidadIdLocalidad = m.LocalidadIdLocalidadNavigation.Nombre,

            }).ToArrayAsync();
        }
        public async Task<Envio?> GetById(int id)
        {
            return await _context.Envio.FindAsync(id);
        }

        public async Task<EnvioDtoOut?> GetEnvioDtoById(int id)
        {
            return await _context.Envio.Where(m => m.Id == id).Select(m => new EnvioDtoOut
            {
                Id = m.Id,
                Direccion = m.Direccion,
                FechaEnvio = m.FechaEnvio,
                NombreEstadoIdEstado = m.EstadoIdEstadoNavigation.Nombre,
                NombreLocalidadIdLocalidad = m.LocalidadIdLocalidadNavigation.Nombre,

            }).SingleOrDefaultAsync();
        }

        public async Task<Envio> Create(EnvioDtoIn envio)
        {
            var newEnvio = new Envio();

            newEnvio.Direccion = envio.Direccion;
            newEnvio.FechaEnvio = envio.FechaEnvio;
            newEnvio.EstadoIdEstado = envio.EstadoIdEstado;
            newEnvio.LocalidadIdLocalidad = envio.LocalidadIdLocalidad;

            _context.Envio.Add(newEnvio);
            await _context.SaveChangesAsync();
            return newEnvio;
        }

        public async Task Update(int id, EnvioDtoIn envio)
        {
            var existEnvio = await GetById(id);
            if (existEnvio != null)
            {
                existEnvio.Direccion = envio.Direccion;
                existEnvio.FechaEnvio = envio.FechaEnvio;
                existEnvio.EstadoIdEstado = envio.EstadoIdEstado;
                existEnvio.LocalidadIdLocalidad = envio.LocalidadIdLocalidad;

                await _context.SaveChangesAsync();
            }

        }
        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);
            if (toDelete != null)
            {
                _context.Envio.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
