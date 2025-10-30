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
    public class MensajeService : IMensajeService
    {
        private readonly comunidadsolidariaContext _context;

        public MensajeService(comunidadsolidariaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MensajeDtoOut>> GetMensajes()
        {
            return await _context.Mensaje.Select(m => new MensajeDtoOut
            {
                Id = m.Id,
                Contenido = m.Contenido,
                FechaHora = m.FechaHora,
                NombreChatIdChat = m.ChatIdChatNavigation.PublicacionIdPublicacionNavigation.Descripcion,
                NombrePerfilIdPerfil = m.PerfilIdPerfilNavigation.RazonSocial,
            }).ToArrayAsync();
        }
        public async Task<Mensaje?> GetById(int id)
        {
            return await _context.Mensaje.FindAsync(id);
        }

        public async Task<MensajeDtoOut?> GetMensajeDtoById(int id)
        {
            return await _context.Mensaje.Where(m => m.Id == id).Select(m => new MensajeDtoOut
            {
                Id = m.Id,
                Contenido = m.Contenido,
                FechaHora = m.FechaHora,
                NombreChatIdChat = m.ChatIdChatNavigation.PublicacionIdPublicacionNavigation.Descripcion,
                NombrePerfilIdPerfil = m.PerfilIdPerfilNavigation.RazonSocial,

            }).SingleOrDefaultAsync();
        }

        public async Task<Mensaje> Create(MensajeDtoIn mensaje)
        {
            var newMensaje = new Mensaje();

            newMensaje.Contenido = mensaje.Contenido;
            newMensaje.FechaHora= mensaje.FechaHora;
            newMensaje.ChatIdChat = mensaje.ChatIdChat;
            newMensaje.PerfilIdPerfil= mensaje.PerfilIdPerfil;

            _context.Mensaje.Add(newMensaje);
            await _context.SaveChangesAsync();
            return newMensaje;
        }

        public async Task Update(int id, MensajeDtoIn mensaje)
        {
            var existMensaje = await GetById(id);
            if (existMensaje != null)
            {
                existMensaje.Contenido = mensaje.Contenido;
                existMensaje.FechaHora = mensaje.FechaHora;
                existMensaje.ChatIdChat = mensaje.ChatIdChat;
                existMensaje.PerfilIdPerfil = mensaje.PerfilIdPerfil;

                await _context.SaveChangesAsync();
            }

        }
        public async Task Delete(int id)
        {
            var toDelete = await GetById(id);
            if (toDelete != null)
            {
                _context.Mensaje.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
