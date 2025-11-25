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
        public class EnvioService : IEnvioService
        {
            private readonly comunidadsolidariaContext _context;

            public EnvioService(comunidadsolidariaContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<EnvioDtoOut>> GetEnvioAll()
            {
                return await _context.Envio.Select(m => new EnvioDtoOut
                {
                    Id = m.Id,
                    PerfilEmisorIdPerfilEmisor = m.PerfilEmisorIdPerfilEmisorNavigation.RazonSocial,
                    PerfilReceptorIdPerfilReceptor = m.PerfilReceptorIdPerfilReceptorNavigation.RazonSocial,
                    LocalidadEmisorIdLocalidadEmisor = m.LocalidadEmisorIdLocalidadEmisorNavigation.Nombre,
                    LocalidadReceptorIdLocalidadReceptor = m.LocalidadReceptorIdLocalidadReceptorNavigation.Nombre,
                    DonacionIddonacion = m.DonacionIddonacionNavigation.Descripcion,
                    DireccionEmisor = m.DireccionEmisor,
                    DireccionEreceptor = m.DireccionEreceptor,
                    Aclaracion = m.Aclaracion,



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
                    PerfilEmisorIdPerfilEmisor = m.PerfilEmisorIdPerfilEmisorNavigation.RazonSocial,
                    PerfilReceptorIdPerfilReceptor = m.PerfilReceptorIdPerfilReceptorNavigation.RazonSocial,
                    LocalidadEmisorIdLocalidadEmisor = m.LocalidadEmisorIdLocalidadEmisorNavigation.Nombre,
                    LocalidadReceptorIdLocalidadReceptor = m.LocalidadReceptorIdLocalidadReceptorNavigation.Nombre,
                    DonacionIddonacion = m.DonacionIddonacionNavigation.Descripcion,
                    DireccionEmisor = m.DireccionEmisor,
                    DireccionEreceptor = m.DireccionEreceptor,
                    Aclaracion = m.Aclaracion,



                }).SingleOrDefaultAsync();
            }

            public async Task<Envio> Create(EnvioDtoIn Envio)
            {
                var newEnvio = new Envio();

                newEnvio.PerfilEmisorIdPerfilEmisor = Envio.PerfilEmisorIdPerfilEmisor;
                newEnvio.PerfilReceptorIdPerfilReceptor = Envio.PerfilReceptorIdPerfilReceptor;
                newEnvio.LocalidadEmisorIdLocalidadEmisor = Envio.LocalidadEmisorIdLocalidadEmisor;
                newEnvio.LocalidadReceptorIdLocalidadReceptor = Envio.LocalidadReceptorIdLocalidadReceptor;
                newEnvio.DonacionIddonacion = Envio.DonacionIddonacion;
                newEnvio.DireccionEmisor = Envio.DireccionEmisor;
                newEnvio.DireccionEreceptor = Envio.DireccionEreceptor;
                newEnvio.Aclaracion = Envio.Aclaracion;

                _context.Envio.Add(newEnvio);
                await _context.SaveChangesAsync();
                return newEnvio;
            }
        }
    }
