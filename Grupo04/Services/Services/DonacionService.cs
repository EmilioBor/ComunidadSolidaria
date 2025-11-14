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
    public class DonacionService : IDonacionService
    {
        private readonly comunidadsolidariaContext _context;

        public DonacionService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DonacionDtoOut>> GetDonacions()
        {
            return await _context.Donacion.Select(m => new DonacionDtoOut
            {
                Id = m.Id,
                Descripcion = m.Descripcion,
                FechaHora = m.FechaHora,
                NombreDonacionTipoIdDonacionTipo = m.DonacionTipoIdDonacionTipoNavigation.Descripcion,
                NombrePerfilIdPerfil = m.PerfilIdPerfilNavigation.RazonSocial,
                NombrePerfilDonanteIdPerfilDonante = m.PerfilDonanteIdPerfilDonanteNavigation.RazonSocial,
                
            }).ToArrayAsync();
        }
        public async Task<Donacion?> GetById(int id)
        {
            return await _context.Donacion.FindAsync(id);
        }

        public async Task<DonacionDtoOut?> GetDonacionDtoById(int id)
        {
            return await _context.Donacion.Where(m => m.Id == id).Select(m => new DonacionDtoOut
            {
                Id = m.Id,
                Descripcion = m.Descripcion,
                FechaHora = m.FechaHora,
                NombreDonacionTipoIdDonacionTipo = m.DonacionTipoIdDonacionTipoNavigation.Descripcion,
                NombrePerfilIdPerfil = m.PerfilIdPerfilNavigation.RazonSocial,
                NombrePerfilDonanteIdPerfilDonante = m.PerfilDonanteIdPerfilDonanteNavigation.RazonSocial,

            }).SingleOrDefaultAsync();
        }

        public async Task<Donacion> Create(DonacionDtoIn donacion)
        {
            var newDonacion = new Donacion();

            newDonacion.Descripcion = donacion.Descripcion;
            newDonacion.FechaHora = donacion.FechaHora;
            newDonacion.DonacionTipoIdDonacionTipo = donacion.DonacionTipoIdDonacionTipo;
            newDonacion.PerfilIdPerfil = donacion.PerfilIdPerfil;
            newDonacion.PerfilDonanteIdPerfilDonante= donacion.PerfilDonanteIdPerfilDonante;

            _context.Donacion.Add(newDonacion);
            await _context.SaveChangesAsync();
            return newDonacion;
        }

        public async Task Update(int id, DonacionDtoIn donacion)
        {
            var existDonacion = await GetById(id);
            if (existDonacion != null)
            {
                existDonacion.Descripcion = donacion.Descripcion;
                existDonacion.FechaHora = donacion.FechaHora;
                existDonacion.DonacionTipoIdDonacionTipo = donacion.DonacionTipoIdDonacionTipo;
                existDonacion.PerfilIdPerfil = donacion.PerfilIdPerfil;
                existDonacion.PerfilDonanteIdPerfilDonante = donacion.PerfilDonanteIdPerfilDonante;

                await _context.SaveChangesAsync();
            }

        }
        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);
            if (toDelete != null)
            {
                _context.Donacion.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
